using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Ce script est fait par Nassour Nassour
public class BossHealthComponent : MonoBehaviour
{
    public float maxHealth = 300f; // vie maximale
    public float currentHealth; // vie courante
    public HealthBarComponent healthBar;

    private BossComponent boss;
    private Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth; 
        animator = GetComponent<Animator>();
        boss = GetComponent<BossComponent>();
    }

    private void Start()
    {
        healthBar.SetMaxHealth((int)maxHealth); // La barre de vie est remplie
    }

    public void TakeDamage(float damageTaken)
    {
        if(boss.Vulnerable) // si le boss est vulnérable
        {
            currentHealth -= damageTaken; // il perds de la vie
            healthBar.SetHealth((int)currentHealth); // la barre de vie diminue
            if (currentHealth <= 0) // si la vie est egale a 0, le boss meure et la partie est gagnée
            {
                animator.SetTrigger("die");
                Destroy(gameObject, 25);
                Debug.Log("Victoire!!");
            }
        }  
    }
}
