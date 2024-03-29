﻿using System.Collections;
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
            // Guillaume: On doit coder ce qui arrive si le joueur meurt...


            if(gameObject.CompareTag("Enemy"))
            {
                gameObject.transform.position = new Vector3(0, -1000, 0); // Pour faire appel au OnTriggerExit
                Destroy(gameObject, 1);         
            }    
            else if (gameObject.CompareTag("Tower"))
            {
                Destroy(transform.parent.gameObject);
            }
            else if (gameObject.CompareTag("Building")) // Guillaume: ajuster pour mes besoins doit cheker avec nass
            {
                Destroy(transform.gameObject);
            }
        }
    }

    public void Heal(float amountHealed, float maxHealth)
    {
        if(amountHealed + health > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += amountHealed;
        }
    }
}
