//les scripts d'animations ont été fortement inspiré par Nicky B, 
//sur youtube, How to Animate Characters in Unity 3D | Animation Transitions With Booleans
//fin animations
//
//modifié par Dérick Gagnon

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorScript : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    bool playerIsOnGround = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
    }
    void Update()
    {
        //début des animations
        bool isWalking = animator.GetBool(isWalkingHash);
        bool state = false;

        if (!isWalking)
        {
            if (Input.GetKey("w"))
            {
                animator.SetBool(isWalkingHash, true);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                state = true;
            }
            if (Input.GetKey("s"))
            {
                animator.SetBool(isWalkingHash, true);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                state = true;
            }
            if (Input.GetKey("a"))
            {
                animator.SetBool(isWalkingHash, true);
                transform.localRotation = Quaternion.Euler(0, -90, 0);
                state = true;
            }
            if (Input.GetKey("d"))
            {
                animator.SetBool(isWalkingHash, true);
                transform.localRotation = Quaternion.Euler(0, 90, 0);
                state = true;
            }
        }
        if (isWalking && !state)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if (Input.GetButtonDown("Jump") && playerIsOnGround == true)
        {
            playerIsOnGround = false;
            animator.SetBool(isWalkingHash, true);

        }
        

        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            playerIsOnGround = true;
    }
}
