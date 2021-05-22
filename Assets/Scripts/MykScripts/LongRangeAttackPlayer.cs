using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LongRangeAttackPlayer : MonoBehaviour
{
    //Script fait par Mykaël 
    // Un vidép qui m'a aidé à comprendre est celui-ci https://www.youtube.com/watch?v=YOP49qGzR8Y
    public float pullSpeed = 1f; //Va affecter la force du tir
    public GameObject arrowPrefab; //Le Prefab de la flèche.
    public GameObject arrow;
    public GameObject ArrowSpawn;//L'endroit où apparaît la flèche (juste à côté de l'arc).
    public int remainingArrows = 30;//Nombre de flèche restante.
    float pull;
    bool arrowReady = false;
    // Start is called before the first frame update
    void Start()
    {
        PlaceArrow();
    }

    // Update is called once per frame
    void Update()
    {
        ShootTheArrow();
    }

    void PlaceArrow() //Permet de placer la flèche sur l'arc
    {
        arrowReady = true;
        arrow = Instantiate(arrowPrefab, ArrowSpawn.transform.position, ArrowSpawn.transform.rotation) as GameObject; //Permet de placer la flèche. 
    }

    void ShootTheArrow()
    {
        if (remainingArrows != 0)//Regarde le nombre de flèches restantes.
        {

            if (Input.GetMouseButton(0))
                pull += Time.deltaTime * pullSpeed; //Permet de tirer plus fort. 
            if (Input.GetMouseButtonUp(0))
            {
                ShootTheArrow theArrow = arrow.transform.GetComponent<ShootTheArrow>();
                theArrow.shootForce = theArrow.shootForce * (pull / 100);
                remainingArrows -= 1; //Enlève une flèche sur le nombre de flèches restantes.
                pull = 0f;
                arrowReady = false;
                theArrow.enabled = true;
            }

            if (Input.GetKey(KeyCode.Mouse0) && arrowReady == false)
                PlaceArrow();
        }
    }
}