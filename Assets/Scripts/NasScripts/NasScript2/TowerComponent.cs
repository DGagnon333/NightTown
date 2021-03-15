using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerComponent : MonoBehaviour
{
    public Transform target;

    public float range;


   
    void Start()
    {
        InvokeRepeating("SearchTarget", 0f, 0.5f); // cherche un ennemi chaque demi-seconde.
    }

    void SearchTarget()
    {

    }

  

    void Update()
    {
        
    }

    private void OnDrawGizmos() // Montre la portée de la tour dans sceneview
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
