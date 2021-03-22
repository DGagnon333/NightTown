using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerComponent : MonoBehaviour
{
    private Transform target; // Le transform de l'ennemi à tuer
    private List<GameObject> enemy; // Une liste de tous les ennemis dans la zone d'attaque de cette tour
    private float fireCountdown; // Compteur de tir

    [Header("Settings")]
    [SerializeField]
    private Transform towerHead; // La tête de la tour ( La partie qui doit faire une rotation )

    [SerializeField]
    private GameObject projectile;

    [SerializeField]
    private Transform shootPoint;


    [Header("Attributes")]
    [SerializeField]
    private float headRotationSpeed = 5f; // Vitesse de rotation de la tête de la tour lors d'une transition

    [SerializeField]
    private float fireRate = 1f; // Vitesse de tir ( Combien de tirs par seconde )

    
    

    void Awake()
    {
        enemy = new List<GameObject>(); // On initialise la liste
    }

    void Start()
    {
        InvokeRepeating("SearchTarget", 0f, 0.5f); // cherche un ennemi chaque demi-seconde.
    }

    void SearchTarget()
    {
        if(enemy!=null) // si la liste n'est pas vide
        {
            float shortestDistance = Mathf.Infinity; // La plus courte distance
            GameObject nearestEnemy = null; // l'ennemi le plus proche

            foreach (GameObject i in enemy)
            {
                float distance = Vector3.Distance(transform.position, i.transform.position); // distance entre la tour et un ennnemi
                if (distance < shortestDistance) // si la distance de cet ennemi est plus courte que la plus petite distance courante
                {
                    shortestDistance = distance; // On remplace la plus petite distance par cette distance
                    nearestEnemy = i; // L'ennemi le plus proche devient cet ennemi
                }
            }

            if (nearestEnemy != null)
            {
                target = nearestEnemy.transform; // S'il y a un ennemi, la cible devient l'ennemi le plus proche
            }
            else
            {
                target = null;
            }
        }
        
    }



    void Update()
    {
        if (target == null)
            return;


        // Ceci permet de tourner la tête de la tour vers l'ennemi la cible.
        Vector3 direction = target.position - transform.position; 
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Cette ligne permet une transition fluide de la rotation courante à la rotation voulue.
        Vector3 rotation = Quaternion.Lerp(towerHead.rotation, lookRotation, Time.deltaTime * headRotationSpeed).eulerAngles; 
        
        towerHead.rotation = Quaternion.Euler (0f, rotation.y, 0f); // On tourne seulement vers selon l'axe des y.

        if(fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;

        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        Debug.Log("Shoot" + target.name);
        Instantiate(projectile, shootPoint.position, shootPoint.rotation);
    }

    //private void OnDrawGizmosSelected() // Montre la portée de la tour dans sceneview
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, range);
    //}



    void OnTriggerEnter(Collider other) // Quand un ennemi entre dans la zone trigger, il est ajouté à la liste
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) // Lorsqu'il quitte la zone, il est enlevé.
    {
        enemy.Remove(other.gameObject);
    }
}
