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
            Destroy(gameObject);
        }
    }
}