using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator anim;
    private bool animationStarted;
    private bool animationFinished;
    private int jumpedTimes;
    private bool jumpLeft = true;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FrogJump");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (animationFinished && animationStarted) {
            animationStarted = false;

            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));

        animationStarted = true;
        animationFinished = false;

        jumpedTimes++;

        if (jumpLeft) {
            anim.Play("FrogJumpLeft");
        } else {
            anim.Play("FrogJumpRight");
        }

        StartCoroutine("FrogJump");
    }

    void AnimationFinished()
    {
        animationFinished = true;

        if (jumpLeft) {
            anim.Play("FrogIdleLeft");
        } else {
            anim.Play("FrogIdleRight");
        }

        if (jumpedTimes == 3) {
            jumpedTimes = 0;

            Vector3 tempScale = transform.localScale;
            tempScale.x *= -1;
            transform.localScale = tempScale;

            jumpLeft = !jumpLeft;
        }
    }
}
