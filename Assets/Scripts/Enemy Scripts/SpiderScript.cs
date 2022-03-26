using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D myBody;
    private Vector3 moveDirection = Vector3.down;

    void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ChangeMovement");
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpider();
    }

    void MoveSpider()
    {
        transform.Translate(moveDirection * Time.smoothDeltaTime);
    }

    IEnumerator ChangeMovement()
    {
        yield return new WaitForSeconds(Random.Range(2f, 5f));

        if (moveDirection == Vector3.down) {
            moveDirection = Vector3.up;
        } else {
            moveDirection = Vector3.down;
        }

        StartCoroutine("ChangeMovement");
    }

    IEnumerator SpiderDead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.BULLET_TAG) {
            anim.Play("SpiderDead");

            myBody.bodyType = RigidbodyType2D.Dynamic;

            StartCoroutine(SpiderDead(3f));
            StopCoroutine("ChangeMovement");

        }

        if (target.tag == MyTags.PLAYER_TAG) {
            target.GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
