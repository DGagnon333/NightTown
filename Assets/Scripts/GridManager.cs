//Classe qui permet de créer une grille et d'avoir l’état des cases dans la grille pour voir si
//elles sont disponibles.
//fait par Dérick

using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using UnityEditorInternal;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridSize = 100;
    private GameObject[] tiles;
    private int step = 2;
    private bool[,] tileState;
    [SerializeField] private GameObject ground;
    [SerializeField] private int baseSize = 50;


    private void Awake()
    {
        tiles = GameObject.FindGameObjectsWithTag("Grid");
        ArrayCreation();
    }
    /// <summary>
    /// Créer une des cases visibles lorsque le mode de construction est activé
    /// poistion voulue (step)
    /// </summary>
    /// <returns></returns>
    private void ArrayCreation()
    {
        tileState = new bool[gridSize, gridSize];

        for (int z = 0; z < baseSize; z++)
        {

            for (int x = 0; x < baseSize; x++)
            {
                Instantiate(tiles[0], new Vector3(x * step - baseSize, 0, z * step - baseSize) + ground.transform.position, Quaternion.identity);
            }
        }
        for (int z = 0; z < gridSize; z++)
        {

            for (int x = 0; x < gridSize; x++)
            {
                tileState[x, z] = true;
            }
        }

    }

    /// <summary>
    /// Cette fonction permet de vérifier si une case est disponible ou non.Si elle l'est, 
    /// un objet est placé à l'endroit souhaité et les tuiles utilisé par cet objet seront
    /// ensuite indisponible.
    /// </summary>
    /// <param name = "newBuilding" > le bâtiment choisi</param>
    /// <param name = "tf" > la position du "phantôme" de l'objet sélectionné</param>
    /// <param name = "scale" > la taille de l'objet sélectionné</param>
    //public void TileState(GameObject newBuilding, Transform tf, Vector3 scale)
    //{
    //    /////////////////// Version 1 (plus rapide, mais un peu moins précise) ////////////////////////
    //    Vector3 scaleDiff = new Vector3(scale.x, 0, scale.z) - (new Vector3(1, 0, 1) * step);
    //    int tileDispo = 0;
    //    if (scaleDiff != Vector3.zero) //si l'objet n'est pas de la même taille qu'une case, sa position est modifiée pour qoit la bonne, ainsi que son espace utilisé
    //    {
    //        for (int z = 0; z < scaleDiff.z; z++)
    //        {
    //            for (int x = 0; x < scaleDiff.x; x++)
    //            {
    //                if (!tileState[tf.position - Vector3.one + new Vector3(x * step, 0, z * step)])
    //                {
    //                    tileDispo++;
    //                }
    //            }
    //        }
    //        for (int z = 0; z < scaleDiff.z; z++)
    //        {
    //            for (int x = 0; x < scaleDiff.x; x++)
    //            {

    //                if (tileDispo == 0)
    //                {

    //                    Instantiate(newBuilding, tf.position, Quaternion.identity);
    //                    tileState[tf.position - Vector3.one + new Vector3(x * step, 0, z * step)] = false;

    //                }
    //            }
    //        }

    //    }
    //    //if (scaleDiff == Vector3.zero)
    //    //{
    //    //    if (tileState[tf.position])
    //    //    {
    //    //        Instantiate(newBuilding, tf.position, Quaternion.identity);
    //    //        tileState[tf.position] = false;
    //    //    }
    //    //}

    //    //////////////////// VERSION 2 (version qui prend plus de mémoire mais plus précise) ////////////////////////

    //    ////int tileDispo = 0;
    //    ////if (scaleDiff != Vector3.zero)
    //    ////{
    //    ////        if (tileState[tf.position - Vector3.one])
    //    ////        {
    //    ////            Instantiate(newBuilding, tf.position, Quaternion.identity);
    //    ////            for (int z = 1; 0 < scaleDiff.z; z++)
    //    ////            {
    //    ////                for (int x = 1; 0 < scaleDiff.x; x++)
    //    ////                {
    //    ////                    tileState[tf.position - Vector3.one + new Vector3(x, 0, z)] = false;
    //    ////                }
    //    ////            }
    //    ////            tileState[tf.position - Vector3.one] = false;
    //    ////        }

    //    ////}
    //    if (scaleDiff == Vector3.zero)
    //    {
    //        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!correct
    //        for (int z = 0; z < gridSize; z++)
    //        {
    //            for (int x = 0; x < gridSize; x++)
    //            {
    //                if (array[x, z] == tf.position && tileState[array[x, z]])
    //                {
    //                    Instantiate(newBuilding, tf.position, Quaternion.identity);
    //                    tileState[array[x, z]] = false;

    //                }
    //            }
    //        }
    //    }
    //    ////////////////////////////Version3
    //    //Vector3 scaleDiff = new Vector3(scale.x, 0, scale.z) - (new Vector3(1, 0, 1) * step);
    //    //int tileDispo = 0;
    //    //if (scaleDiff == Vector3.zero) //si l'objet n'est pas de la même taille qu'une case, sa position est modifiée pour qoit la bonne, ainsi que son espace utilisé
    //    //{
    //    //    for (int z = 0; z < gridSize; z++)
    //    //    {

    //    //        for (int x = 0; x < gridSize; x++)
    //    //        {
    //    //            tileDispo++;
    //    //            if (array2[tileDispo].State && array2[tileDispo].X == (int)tf.position.x && array2[tileDispo].Z == (int)tf.position.z)
    //    //            {
    //    //                Instantiate(newBuilding, tf.position, Quaternion.identity);
    //    //                array2[tileDispo].State = false;
    //    //            }
    //    //        }
    //    //    }

    //    //}
    //}
    public void TileState(GameObject newBuilding, Transform tf, Vector3 scale)
    {
        int posX = (int)(tf.position.x + gridSize - (int)ground.transform.position.x) / 2 ;
        int posZ = (int)(tf.position.z + gridSize - (int)ground.transform.position.z) / 2;
        int scaleDiffX = (int)(scale.x -2) / 2;
        int scaleDiffZ = (int)(scale.z -2) / 2;
        int tileDispo = 0;
        Debug.Log((int)(tf.position.x + gridSize) / 2); Debug.Log((int)ground.transform.position.x); Debug.Log(posX);
        if (scaleDiffX != 0 && scaleDiffZ != 0)
        {
            for (int z = 0; z <= scaleDiffZ; z++)
            {
                for (int x = 0; x <= scaleDiffX; x++)
                {
                    if (!tileState[posX + z, posZ + x])
                    {
                        tileDispo++;
                        Debug.Log(tileDispo);
                    }
                }
            }
            if (tileDispo == 0)
            {
                for (int z = 0; z <= scaleDiffZ; z++)
                {
                    for (int x = 0; x <= scaleDiffX; x++)
                    {
                        tileState[posX + x, posZ + z] = false;
                    }
                }
                Instantiate(newBuilding, new Vector3(posX * step - gridSize, 0, posZ * step - gridSize) + new Vector3(1,0,1) + ground.transform.position, Quaternion.identity);
            }
        }

        if (tileState[posX, posZ] && scaleDiffX == 0)
        {
            Instantiate(newBuilding, new Vector3(posX * step - gridSize, 0, posZ * step - gridSize) + ground.transform.position, Quaternion.identity);
            tileState[posX, posZ] = false;
        }
    }
}
public class Tile
{
    public int X;
    public int Z;
    public bool State;

    public Tile(int x, int z)
    {
        X = x;
        Z = z;
    }
}
