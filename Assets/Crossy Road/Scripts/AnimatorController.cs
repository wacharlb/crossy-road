using UnityEngine;
using System.Collections;

public class AnimatorController : MonoBehaviour
{
    public PlayerController playerController = null;
    private Animator animator = null;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log("AnimatorController Update: isIdle: " + playerController.isIdle);

        if (playerController.isDead)
        {
            animator.SetBool("dead", true);
        }

        if (playerController.jumpStart)
        {
            animator.SetBool("jumpStart", true);
        }
        else if (playerController.isJumping)
        {
            animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool("jump", false);
            animator.SetBool("jumpStart", false);
        }
    }
}
