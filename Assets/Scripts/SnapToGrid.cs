using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    public Vector3 gridSize { get; private set; } = new Vector3(10, 10, 10);
    public Vector3 step { get; private set; } = new Vector3(2, 2, 2);
    private GameObject ground;
    private Vector3 stepDiff;
    private int stepDiffX;
    private int stepDiffY;
    private int stepDiffZ;

    private void Awake()
    {
        ground = GameObject.FindGameObjectsWithTag("Ground")[0];
        gridSize = ground.transform.localScale;
        stepDiff = transform.localScale - step;
        transform.position = transform.position + stepDiff;
        Debug.Log(stepDiff);
    }
    private void OnDrawGizmos()
    {
        var position = new Vector3(
                Mathf.Round(transform.position.x / step.x) * step.x,
                Mathf.Round(transform.position.y / step.y) * step.y,
                Mathf.Round(transform.position.z / step.z) * step.z
                );
        if (stepDiff != Vector3.zero)
        {
        transform.position = new Vector3(position.x, 0, position.z) + Vector3.one;
        }
        else
            transform.position = new Vector3(position.x, 0, position.z);
        //if (stepDiff != Vector3.zero)
        //{
        //    SnapToTilesDiff();
        //}
        //else
        //    SnapToTiles();
        //la fonction SnapToGrid est inspirée de How to Snap Objects to a 
        //Custom Grid in 3 minutes - Unity Tutorial, par Saeed Prez, Youtube
        //
        //j'ai ajouter un stepDiff et d'autres intrants, comme ça, le jeu prend en compte si on la taille d'un 
        //objet change et prend en compte cette donnée pour la nouvelle position...sinon
        //l'objet se retrouve au milieu de deux cases
    }

    private void SnapToTiles()
    {
        var position = new Vector3(
                Mathf.Round(transform.position.x / step.x) * step.x,
                Mathf.Round(transform.position.y / step.y) * step.y,
                Mathf.Round(transform.position.z / step.z) * step.z
                );
        transform.position = new Vector3(position.x, 0, position.z);
    }
    private void SnapToTilesDiff()
    {
        //modifier pour que quand on double la grandeur de l'objet il soit à la bonne place

        //-------------------------------------------------------début inspiration
        
            var position = new Vector3(
            Mathf.Round(transform.position.x / step.x) * step.x,
            Mathf.Round(transform.position.y / step.y) * step.y,
            Mathf.Round(transform.position.z / step.z) * step.z);
            transform.position = new Vector3(position.x, 0, position.z) + Vector3.one;


        //--------------------------------------------------------fin d'inspiration
    }
}
