using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform bottomCollision, leftCollision, rightCollision, topCollision;
    public LayerMask playerLayer;

    private Rigidbody2D myBody;
    private Animator anim;
    private Vector3 leftCollisionPosition, rightCollisionPosition;
    private bool moveLeft;
    private bool canMove;
    private bool stunned;

    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        leftCollisionPosition = leftCollision.position;
        rightCollisionPosition = rightCollision.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            if (moveLeft) {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            } else {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }

        CheckCollision();
    }

    void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftCollision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCollision.position, Vector2.right, 0.1f, playerLayer);
        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, 0.2f, playerLayer);

        if (topHit != null) {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG) {
                if (!stunned) {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = 
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);

                    canMove = false;
                    myBody.velocity = new Vector2(0,0);

                    anim.Play("Stunned");
                    stunned = true;

                    if (tag == MyTags.BEETLE_TAG) {
                        anim.Play("Stunned");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if (leftHit) {
            if (leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG) {
                if (!stunned) {
                    // apply damage to player
                    print("DAMAGE LEFT!");
                } else {
                    if (tag != MyTags.BEETLE_TAG) {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if (rightHit) {
            if (rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG) {
                if (!stunned) {
                    // apply damage to player
                    print("DAMAGE RIGHT!");
                } else {
                    if (tag != MyTags.BEETLE_TAG) {
                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        // if we don't detect collision
        if (!Physics2D.Raycast(bottomCollision.position, Vector2.down, 0.1f)) {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;

        Vector3 tempScale = transform.localScale;

        if (moveLeft) {
            tempScale.x = Mathf.Abs(tempScale.x);
            leftCollision.position = leftCollisionPosition;
            rightCollision.position = rightCollisionPosition;
        } else {
            tempScale.x = -Mathf.Abs(tempScale.x);
            leftCollision.position = rightCollisionPosition;
            rightCollision.position = leftCollisionPosition;
        }

        transform.localScale = tempScale;
    }

    IEnumerator Dead(float timer) 
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.BULLET_TAG) {
            if (tag == MyTags.BEETLE_TAG) {
                anim.Play("Stunned");
                canMove = false;
                myBody.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.6f));
            }

            if (tag == MyTags.SNAIL_TAG) {
                if (!stunned) {
                    anim.Play("Stunned");
                    stunned = true;
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                } else {
                    gameObject.SetActive(false);
                }
                
    
            }
        }
    }
}
