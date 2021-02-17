//fait par Dérick

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    public Vector3 gridSize { get; private set; } = new Vector3(20, 20, 20);
    public Vector3 step { get; private set; } = new Vector3(2, 2, 2);
    private GameObject ground;
    private Vector3 stepDiff;

    private void Awake()
    {
        ground = GameObject.FindGameObjectsWithTag("Ground")[0];
        gridSize = ground.transform.localScale;
        stepDiff = transform.localScale - step;
        transform.position = transform.position + stepDiff;

        
    }
    private void OnDrawGizmos()
    {
        SnapToTiles(transform);
        //la fonction SnapToGrid est inspirée de How to Snap Objects to a 
        //Custom Grid in 3 minutes - Unity Tutorial, par Saeed Prez, Youtube
        //
        //j'ai ajouter un stepDiff et d'autres intrants, comme ça, le jeu prend en compte si on la taille d'un 
        //objet change et prend en compte cette donnée pour la nouvelle position...sinon
        //l'objet se retrouve au milieu de deux cases
    }
    public void SnapToTiles(Transform tf)
    {
        //modifier pour que quand on double la grandeur de l'objet il soit à la bonne place

        //-------------------------------------------------------début inspiration
        
            var position = new Vector3(
            Mathf.Round(tf.position.x / step.x) * step.x,
            Mathf.Round(tf.position.y / step.y) * step.y,
            Mathf.Round(tf.position.z / step.z) * step.z);
            tf.position = new Vector3(position.x, 0, position.z);


        //--------------------------------------------------------fin d'inspiration
        if (stepDiff != Vector3.zero)
        {
            tf.position += Vector3.one;
        }
    }
}
