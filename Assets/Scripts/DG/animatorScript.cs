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
    private string currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void ChangedAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
