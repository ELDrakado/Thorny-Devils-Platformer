using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 10;

    private Animator anim;
    private bool canDamage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (canDamage) {
            if (target.tag == MyTags.BULLET_TAG) {
                health--;
                canDamage = false;

                if (health == 0) {
                    GetComponent<BossScript>().DeactivateBossScript();
                    anim.Play("Dead");
                    StartCoroutine(Dead(3f));
                }

                StartCoroutine(WaitForDamage());
            }
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator Dead(float timer) 
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}
