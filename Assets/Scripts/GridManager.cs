//Classe qui permet de créer une grille et d'avoir l’état des cases dans la grille pour voir si
//elles sont disponibles.
//fait par Dérick

using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridSize = 50;
    public Vector3[,] array;
    private GameObject[] tiles;
    private int step = 2;
    private Dictionary<Vector3, bool> tileState;

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
        array = new Vector3[gridSize, gridSize];
        tileState = new Dictionary<Vector3, bool>(gridSize);

        for (int z = 0; z < gridSize; z++)
        {

            for (int x = 0; x < gridSize; x++)
            {
                array[x, z] = new Vector3(x * step - gridSize, 0, z * step - gridSize);
                tileState[new Vector3((int)array[x, z].x, 0, (int)array[x, z].z)] = true;
                Instantiate(tiles[0], new Vector3(x * step, (int)(-0.08), z * step) + new Vector3(1, 0, 1) * (-gridSize), Quaternion.identity);
                //ici on créer des tiles jusqu'à la grandeur de la grille voulue, à chaque bon du step et on diminue le vecteur par la taille 
            }
        }
        //test = new int[gridSize];
        //test2 = new int[gridSize];
        //array = new Dictionary<int[,], bool>(gridSize);
        //tileState = new Dictionary<Vector3, bool>(gridSize);

        //for (int z = 0; z < gridSize; z++)
        //{

        //    for (int x = 0; x < gridSize; x++)
        //    {
        //        test[x] = x * step - gridSize;
        //        test2[z] = z * step - gridSize;
        //        array[new int[test[x], test2[z]]] = true;
        //        Instantiate(tiles[0], new Vector3(x * step - gridSize, (int)(-0.08), z * step - gridSize), Quaternion.identity);
        //        //ici on créer des tiles jusqu'à la grandeur de la grille voulue, à chaque bon du step et on diminue le vecteur par la taille 
        //    }
        //}

    }

    /// <summary>
    /// Cette fonction permet de vérifier si une case est disponible ou non. Si elle l'est, 
    /// un objet est placé à l'endroit souhaité et les tuiles utilisé par cet objet seront
    /// ensuite indisponible.
    /// </summary>
    /// <param name="newBuilding">le bâtiment choisi</param>
    /// <param name="tf">la position du "phantôme" de l'objet sélectionné</param>
    /// <param name="scale">la taille de l'objet sélectionné</param>
    public void TileState(GameObject newBuilding, Transform tf, Vector3 scale)
    {
        ///////////////// Version 1 (plus rapide, mais un peu moins précise) ////////////////////////
        Vector3 scaleDiff = new Vector3(scale.x, 0, scale.z) - (new Vector3(1, 0, 1) * step);
        int tileDispo = 0;
        if (scaleDiff != Vector3.zero) //si l'objet n'est pas de la même taille qu'une case, sa position est modifiée pour qoit la bonne, ainsi que son espace utilisé
        {
            for (int z = 0; z < scaleDiff.z; z++)
            {
                for (int x = 0; x < scaleDiff.x; x++)
                {
                    if (!tileState[tf.position - Vector3.one + new Vector3(x * step, 0, z * step)])
                    {
                        tileDispo++;
                    }
                }
            }
            for (int z = 0; z < scaleDiff.z; z++)
            {
                for (int x = 0; x < scaleDiff.x; x++)
                {
                    if (tileDispo == 0)
                    {

                        Instantiate(newBuilding, tf.position, Quaternion.identity);
                        tileState[tf.position - Vector3.one + new Vector3(x * step, 0, z * step)] = false;

                    }
                }
            }

        }
        //if (scaleDiff == Vector3.zero)
        //{
        //    if (tileState[tf.position])
        //    {
        //        Instantiate(newBuilding, tf.position, Quaternion.identity);
        //        tileState[tf.position] = false;
        //    }
        //}

        ////////////////// VERSION 2 (version qui prend plus de mémoire mais plus précise) ////////////////////////

        //int tileDispo = 0;
        //if (scaleDiff != Vector3.zero)
        //{
        //        if (tileState[tf.position - Vector3.one])
        //        {
        //            Instantiate(newBuilding, tf.position, Quaternion.identity);
        //            for (int z = 1; 0 < scaleDiff.z; z++)
        //            {
        //                for (int x = 1; 0 < scaleDiff.x; x++)
        //                {
        //                    tileState[tf.position - Vector3.one + new Vector3(x, 0, z)] = false;
        //                }
        //            }
        //            tileState[tf.position - Vector3.one] = false;
        //        }

        //}
        if (scaleDiff == Vector3.zero)
        {
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!correct
            for (int z = 0; z < gridSize; z++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    if (array[x, z] == tf.position)
                    {
                        Instantiate(newBuilding, tf.position, Quaternion.identity);
                        tileState[array[x, z]] = false;
                    }
                }
            }
        }

    }
}
