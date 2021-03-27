using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShootTheArrow : MonoBehaviour
{
    //Script fait par Mykaël 
    // Un vidép qui m'a aidé à comprendre est celui-ci https://www.youtube.com/watch?v=YOP49qGzR8Y
    public Rigidbody rb;
    public float shootForce = 3000f;
    private bool hasHitObject = false;
    private float chronometer;
    private float timeForDestruction = 5f;
    private float damageByTheArrow = 100f;
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        ShootTheArrowWithForce();
    }

    // Update is called once per frame
    void Update()
    {
        chronometer += Time.deltaTime;
        MakeTheAngleOfArrowChange();
        if (chronometer >= timeForDestruction && hasHitObject == true)
            Destroy(gameObject);

    }

    void ShootTheArrowWithForce()
    {
        rb.AddRelativeForce(Vector3.left * shootForce);
    }

    void MakeTheAngleOfArrowChange()
    {
        float velocityInX = rb.velocity.x;
        float velocityInY = rb.velocity.y;
        float velocityInZ = rb.velocity.z;
        float velocitySize = (float)Math.Sqrt(velocityInX * velocityInX + velocityInZ * velocityInZ);
        float angleToFall = Mathf.Atan2(velocityInY, velocitySize) * (180 / Mathf.PI) - 45; //Étant donné que la flèche va tomber en y en fonction de la gravité, nous voulons que la pointe de la flèche tombe également et qu'elle ne reste pas seulement droite. Nous voulons donc qu'elle agisse comme le fait normalement une flèche. 
        rb.transform.eulerAngles = new Vector3(angleToFall, rb.transform.eulerAngles.y, rb.transform.eulerAngles.z);  // inspiré de : https://www.youtube.com/watch?v=YOP49qGzR8Y&t=685s
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Arrow" && collision.collider.tag != "Bow" && collision.collider.tag != "Player")
        {
            hasHitObject = true;
            StopMoving();
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            HealthComponent enemyHealth = collision.collider.GetComponent<HealthComponent>();
            enemyHealth.TakeDamage(damageByTheArrow); // L'ennemi perds de la vie egal au dommage de l'arme
        }
    }

    private void StopMoving()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll; // inspiré de : https://www.youtube.com/watch?v=ayiXNHhUhQE&t=646s
    }
}