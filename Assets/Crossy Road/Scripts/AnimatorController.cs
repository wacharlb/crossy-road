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

        if(playerController.isIdle == false)
        {
            return;
        }

        /*
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(270, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(270, 180, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(270, -90, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(270, 90, 0);
        }*/
    }
}
