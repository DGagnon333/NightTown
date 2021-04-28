using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BossComponent : MonoBehaviour
{
    private Animator animator;
    private BossHealthComponent health;
    private Rigidbody rb;

    public Transform playerPosition;
    public float speed = 15f;

    private float elapsedTime = 0f;
    private bool charged = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<BossHealthComponent>();
        rb = GetComponentInChildren<Rigidbody>();   
    } 

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        ChargeAttack();


    }

    void ChargeAttack()
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

        //Vector3 lastKnownPos = transform.position;
        //if (elapsedTime >= 3)
        //{
        //    animator.SetTrigger("run");
        //    elapsedTime = 0;
        //    lastKnownPos = playerPosition.position;
        //}
        //transform.position = Vector3.MoveTowards(transform.position, lastKnownPos, speed * Time.deltaTime);

        Vector3 direction = transform.position;
        if (elapsedTime >=3)
        {
            direction = playerPosition.position - transform.position;
            
            elapsedTime = 0;
        }
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
}
