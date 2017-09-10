﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveDistance = 1;
    public float moveTime = 0.4f;
    public float colliderDistCheck = 1;
    public bool isIdle = true;
    public bool isDead = false;
    public bool isMoving = false;
    public bool isJumping = false;
    public bool jumpStart = false;
    public ParticleSystem particle = null;
    public GameObject chick = null;
    public AudioClip audioIdle1 = null;
    public AudioClip audioIdle2 = null;
    public AudioClip audioHop = null;
    public AudioClip audioHit = null;
    public AudioClip audioSplash = null;
    public ParticleSystem splash = null;
    public bool parentedToObject = false;

    private Renderer renderer = null;
    private bool isVisible = false;

    private void Start()
    {
        renderer = chick.GetComponent<Renderer>();
    }

    private void Update()
    {
        if(!Manager.instance.CanPlay())
        {
            return;
        }

        if(isDead)
        {
            return;
        }

        CanIdle();
        CanMove();
        IsVisible();
    }

    void CanIdle()
    {
        if(isIdle)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                CheckIfCanMove();

                PlayAudioClip(audioIdle1);
            }
        }
    }

    void CheckIfCanMove()
    {
        // Raycast - Find if there is any collider box in front of player
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        Physics.Raycast(this.transform.position, -chick.transform.up, out hit, colliderDistCheck);

        Debug.DrawRay(this.transform.position, -chick.transform.up * colliderDistCheck, Color.red, 2);

        if(hit.collider == null)
        {
            SetMove();
        }
        else
        {
            if(hit.collider.tag == "collider")
            {
                //Debug.Log("Hit something with collider tag.");
            }
            else
            {
                SetMove();
            }
        }
    }

    void SetMove()
    {
        //Debug.Log("Hit noithing. Keep moving.");

        isIdle = false;
        isMoving = true;
        jumpStart = true;
    }

    void CanMove()
    {
        if(isMoving)
        {   
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z + moveDistance));
                SetMoveForwardState();
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                Moving(new Vector3(transform.position.x, transform.position.y, transform.position.z - moveDistance));
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Moving(new Vector3(transform.position.x - moveDistance, transform.position.y, transform.position.z));
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                Moving(new Vector3(transform.position.x + moveDistance, transform.position.y, transform.position.z));
            }
        }
    }

    void Moving(Vector3 pos)
    {
        // Set the appropriate game state for player moving
        isIdle = false;
        isMoving = false;
        isJumping = true;
        jumpStart = false;

        // Play audio clip for player movement
        PlayAudioClip(audioHop);

        // Move player
        LeanTween.move(this.gameObject, pos, moveTime).setOnComplete(MoveComplete);
    }

    void MoveComplete()
    {
        isJumping = false;
        isIdle = true;

        // Play audio clip for player movement complete
        PlayAudioClip(audioIdle2);
    }

    void SetMoveForwardState()
    {
        Manager.instance.UpdateDistanceCount();
    }

    void IsVisible()
    {
        //Debug.Log("renderer.isVisible: " + renderer.isVisible);
        if (renderer.isVisible)
        {
            isVisible = true;
        }

        if(!renderer.isVisible && isVisible)
        {
            //Debug.Log("Player off screen. Apply GotHit()");

            GotHit();
        }
    }

    public void GotHit()
    {
        isDead = true;
        ParticleSystem.EmissionModule em = particle.emission;
        em.enabled = true;

        // Play audio clip for player getting hit
        PlayAudioClip(audioHit);

        // Set game state to Game Over
        Manager.instance.GameOver();
    }

    public void GotSoaked()
    {
        isDead = true;
        ParticleSystem.EmissionModule em = splash.emission;
        em.enabled = true;

        // Play audio clip for player getting soaked
        PlayAudioClip(audioSplash);

        chick.SetActive(false);

        // Set game state to Game Over
        Manager.instance.GameOver();
    }

    void PlayAudioClip(AudioClip clip)
    {
        this.GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
