using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttack : MonoBehaviour
{
    private Animator animator;
    private BossHealthComponent health;
    private Rigidbody rb;

    public Transform playerPosition;
    public float speed = 115f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<BossHealthComponent>();
        rb = GetComponentInChildren<Rigidbody>();

        animator.SetTrigger("run");
    }

    
    void Charge()
    {
        //float elapsedTime = 0;
        //Vector3 lastKnownPos = playerPosition.position;
        //do
        //{
        //    animator.SetTrigger("run");
        //    transform.position = Vector3.MoveTowards(transform.position, lastKnownPos, speed * Time.deltaTime);

        //    elapsedTime += Time.deltaTime;
        //} while (elapsedTime <= 3);

        //Debug.Log("end of charge");

        Vector3 lastKnownPos = playerPosition.position;
        
        transform.position = Vector3.MoveTowards(transform.position, lastKnownPos, speed * Time.deltaTime);
    }
}





