using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorScript : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");

    }
    void Update()
    {
        //début des animations
        bool isWalking = animator.GetBool(isWalkingHash);

        if (!isWalking)
        {
            if (Input.GetKey("w"))
            {
                animator.SetBool(isWalkingHash, true);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetKey("s"))
            {
                animator.SetBool(isWalkingHash, true);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            if (Input.GetKey("a"))
            {
                animator.SetBool(isWalkingHash, true);
                transform.localRotation = Quaternion.Euler(0, -90, 0);
            }
            if (Input.GetKey("d"))
            {
                animator.SetBool(isWalkingHash, true);
                transform.localRotation = Quaternion.Euler(0, 90, 0);
            }
        }
        if (isWalking && !(Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d")))
        {
            animator.SetBool(isWalkingHash, false);
        }
        //if (Input.GetButtonDown("Jump") && playerIsOnGround == true)
        //{
        //    rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
        //    playerIsOnGround = false;
        //    animator.SetBool(isWalkingHash, true);

        //}

        //les scripts d'animations ont été fortement inspiré par Nicky B, 
        //sur youtube, How to Animate Characters in Unity 3D | Animation Transitions With Booleans
        //fin animations
    }
}
