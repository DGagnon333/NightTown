using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : MonoBehaviour
{
    public float maxHealth = 300f; // vie maximale
    public float currentHealth; // vie courante
    public HealthBarComponent healthBar;
    public float hps =1f; //vie récupérée par seconde


    private void Awake()
    {
        currentHealth = maxHealth;
    }
    void Start()
    {
        healthBar.SetMaxHealth((int)maxHealth); // La barre de vie est remplie
        InvokeRepeating("RegenPerSecond", 1, 1);
    }
    private void Update()
    {       
        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(20f);
        }
    }


    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken; // il perds de la vie
        healthBar.SetHealth((int)currentHealth); // la barre de vie diminue

        if (currentHealth <= 0) // si la vie est egale a 0, le boss meure et la partie est gagnée
        {
            Debug.Log("Le player est mort bruh");
        }

    }

    public void Heal(float amountHealed)
    {
        if (amountHealed + currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthBar.SetHealth((int)currentHealth);
        }
        else
        {
            currentHealth += amountHealed;
            healthBar.SetHealth((int)currentHealth);
        }
    }

    private void RegenPerSecond()// Prends en intrant le nombre de vie par seconde
    {
        float nextHealth = currentHealth + hps;

        if (nextHealth < maxHealth)
        {
            currentHealth += hps;
            healthBar.SetHealth((int)currentHealth);
        }        
    }
}
