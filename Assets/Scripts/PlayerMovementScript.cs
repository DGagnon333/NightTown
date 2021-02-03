using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 vitesse = new Vector3(400, 0, 150);
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        rb.AddForce(vitesse * Time.deltaTime);
    }
}
