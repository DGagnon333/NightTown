using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    //Script fait par Mykael et avec une aide de Dérick Gagnon 
    [SerializeField] Rigidbody rb;
    [SerializeField] float walkSpeed = 50f;
    [SerializeField] float runSpeed = 100f;
    public bool playerIsOnGround = true;

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        body.freezeRotation = true;
    }

    private void Update()
    {
        

            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            float moveSpeed;
            if (Input.GetKey(KeyCode.L))
            {
                moveSpeed = runSpeed;
            }

            else
            {
                moveSpeed = walkSpeed;
            }

            Vector3 movement = (transform.right * x + transform.forward * z).normalized;
            rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

            
            rb.constraints = RigidbodyConstraints.FreezeRotationX;
            rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float moveSpeed;
        if (Input.GetKey(KeyCode.L))
        {
            moveSpeed = runSpeed;
        }

        else
        {
            moveSpeed = walkSpeed;
        }

        Vector3 movement = (transform.right * x + transform.forward * z).normalized;
        rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && playerIsOnGround == true)
        {
            rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            playerIsOnGround = false;
        }
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            playerIsOnGround = true;

        if (collision.gameObject.tag != "Ground")
            rb.velocity = Vector3.zero;
    }
}