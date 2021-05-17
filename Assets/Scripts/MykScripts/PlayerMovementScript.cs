using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    //Script fait par Mykael et avec une aide de Dérick Gagnon 
    [SerializeField] CharacterController rb;
    [SerializeField] float walkSpeed = 50f;
    [SerializeField] float runSpeed = 100f;
    [SerializeField] float forceJump = 6f;
    [SerializeField] float gravity = -9.81f;
    public bool playerIsOnGround = true;
    private float jumpVelocity;

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        body.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }

        else
        {
            moveSpeed = walkSpeed;
        }

        if (Input.GetButtonDown("Jump") && rb.isGrounded)
        {
            jumpVelocity = forceJump;
            playerIsOnGround = false;
        }

        jumpVelocity += gravity * Time.deltaTime;

        Vector3 movement = (transform.right * x + transform.forward * z).normalized + Vector3.up*jumpVelocity;
        rb.Move( movement * moveSpeed * Time.deltaTime);
    }
}