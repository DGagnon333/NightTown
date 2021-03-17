using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollider : MonoBehaviour
{
    private BoxCollider boxCollider;
    private void Awake()
    {
        boxCollider.material = new PhysicMaterial() { bounciness = 0, };
    }
    

    [SerializeField] Rigidbody rb;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
            rb.velocity = Vector3.zero;
    }
}
