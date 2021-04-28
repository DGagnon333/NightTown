using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthComponent : MonoBehaviour
{
    public float maxHealth = 300f;
    public float currentHealth;
    public HealthBarComponent healthBar;

    private Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        healthBar.SetMaxHealth((int)maxHealth);
    }

    public void TakeDamage(float damageTaken)
    {
        currentHealth -= damageTaken;
        healthBar.SetHealth((int)currentHealth);
        if (currentHealth <= 0)
        {
            animator.SetTrigger("die");
            Destroy(gameObject, 25);
            Debug.Log("Boss death");
        }
    }
}
