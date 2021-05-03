using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScriptController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isAttackingHash;
    private void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    private void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isAttacking = animator.GetBool(isAttackingHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKey("space");
        bool attackPressed = Input.GetKey("f");



        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if (!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool(isRunningHash, true);
        }

        if(isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }

        if (!isJumping && jumpPressed)
        {
            animator.SetBool(isJumpingHash, true);
        }
        if (isJumping && !jumpPressed)
        {
            animator.SetBool(isJumpingHash, false);
        }

        if (!isAttacking && attackPressed)
        {
            animator.SetBool(isAttackingHash, true);
        }
        if (isAttacking && !attackPressed)
        {
            animator.SetBool(isAttackingHash, false);
        }

    }
}
