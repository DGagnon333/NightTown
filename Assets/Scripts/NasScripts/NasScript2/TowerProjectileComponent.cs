using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script est écrit par Nassour Nassour 
// (Même inspiration que TowerComponent)
public class TowerProjectileComponent : MonoBehaviour
{
    private Transform targetEnemy;

    [SerializeField]
    private float speed = 50f; // Vitesse du projectile

    [SerializeField]
    private float damage = 2f; // dommages du projectile

    public void Chase(Transform target) // Permet de transférer la cible de TowerComponent
    {
        targetEnemy = target; 
    }

    void Update()
    {
        if (targetEnemy == null) // Si on a pas de cible
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = targetEnemy.position - gameObject.transform.position; // Direction à suivre du projectile
        float distanceThisFrame = speed * Time.deltaTime; // distance à traverser en ce frame


        // Si la distance entre le projectile et l'ennemi est 
        // plus petite que la distance à traverser en ce frame, il y a eu une collision
        if (direction.magnitude <= distanceThisFrame)
        {
            Debug.Log("I hit " + targetEnemy.gameObject.name);
            targetEnemy.gameObject.GetComponent<HealthComponent>().TakeDamage(damage);
            Destroy(gameObject);
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);

    }
}
