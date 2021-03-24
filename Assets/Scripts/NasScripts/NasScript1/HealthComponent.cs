using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script est fait par Nassour Nassour
public class HealthComponent : MonoBehaviour
{
    [SerializeField()]
    float health = 35f;

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
        {
            if(gameObject.CompareTag("Enemy"))
            {
                gameObject.transform.position = new Vector3(0, -1000, 0); // Pour faire appel au OnTriggerExit
                Destroy(gameObject, 1);         
            }    
            else if (gameObject.CompareTag("Base"))
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
