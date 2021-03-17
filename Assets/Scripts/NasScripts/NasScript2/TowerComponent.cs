using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerComponent : MonoBehaviour
{
    public Transform target;

    public float range; // utile??

    List<GameObject> enemy = null;



    void Start()
    {
        InvokeRepeating("SearchTarget", 0f, 0.5f); // cherche un ennemi chaque demi-seconde.
    }

    void SearchTarget()
    {
        if(enemy!=null)
        {
            float shortestDistance = Mathf.Infinity;
            GameObject nearestEnemy = null;

            foreach (GameObject i in enemy)
            {
                float distance = Vector3.Distance(transform.position, i.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestEnemy = i;
                }
            }

            if (nearestEnemy != null)
            {
                target = nearestEnemy.transform;
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
    }

    //private void OnDrawGizmosSelected() // Montre la portée de la tour dans sceneview
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, range);
    //}



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemy.Remove(other.gameObject);
    }
}
