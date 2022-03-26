using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject stone;
    public Transform attackInstantiate;

    private Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartAttack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        GameObject obj = Instantiate(stone, attackInstantiate.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700f), 0f));
    }

    void BackToIdle()
    {
        anim.Play("Idle");
    }

    public void DeactivateBossScript()
    {
        StopCoroutine("StartAttack");
        enabled = false;
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));
        anim.Play("Attack");

        StartCoroutine("StartAttack");
    }
}
