using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthComponent : MonoBehaviour // Ce script va sur l'ennemi
{
    [SerializeField()]
    float health = 35f;

    public void TakeDamage(float damageTaken)
    {
        health -= damageTaken;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
