﻿using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float walkSpeed = 150f;
    [SerializeField] float runSpeed =200f;
    public bool playerIsOnGround = true;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float moveSpeed; 
        if (Input.GetKey (KeyCode.L))
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            playerIsOnGround = true;
    }
}