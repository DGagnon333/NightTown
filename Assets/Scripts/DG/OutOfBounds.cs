//Cette classe s'occupe de voir si le joueur est en train de tomber dans le vide. 
//Si C'est le cas, sa position retournera à celle du début de la partie.

//Fait par Dérick

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    float time = 0;
    private Vector3 position;
    private void Awake()
    {
        position = transform.position;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if(time >= 4)
        {
            time = 0;
            OfBounds();
        }
    }

    private void OfBounds()
    {
        if (transform.position.y <= -5)
        {
            transform.position = position;
        }
    }
}
