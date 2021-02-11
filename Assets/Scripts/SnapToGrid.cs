using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    public Vector3 gridSize { get; private set; } = new Vector3(10, 10, 10);
    public Vector3 step { get; private set; } = new Vector3(2, 1, 2);
    private GameObject ground;
    //private int stepDiff;

    private void Awake()
    {
        ground = GameObject.FindGameObjectsWithTag("Ground")[0];
        gridSize = ground.transform.localScale;
    }
    private void OnDrawGizmos()
    {
        SnapToTiles();
        //la fonction SnapToGrid est inspirée de How to Snap Objects to a 
        //Custom Grid in 3 minutes - Unity Tutorial, par Saeed Prez, Youtube

    }
    private void SnapToTiles()
    {
        //stepDiff = (int)(transform.localScale.x - step.x);
        //modifier pour que quand on double la grandeur de l'objet il soit à la bonne place

        //-------------------------------------------------------début inspiration
        var position = new Vector3(
            Mathf.Round(transform.position.x / step.x) * step.x,
            Mathf.Round(transform.position.y / step.y) * step.y,
            Mathf.Round(transform.position.z / step.z) * step.z
            //Mathf.RoundToInt(this.transform.position.x),
            //Mathf.RoundToInt(this.transform.position.y),
            //Mathf.RoundToInt(this.transform.position.z)
            );
        this.transform.position = position;
        //--------------------------------------------------------fin d'inspiration
    }
}
