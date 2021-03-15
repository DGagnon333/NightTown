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
    private int step = 2;
    private bool[,] tileState;
    [SerializeField] private GameObject ground;
    [SerializeField] private int baseSize = 50;
    

    private void Awake()
    {
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
    public void TileState(GameObject newBuilding, Transform tf, Vector3 scale)
    {
        int posX = (int)(tf.position.x + gridSize - (int)ground.transform.position.x) / 2 ;
        int posZ = (int)(tf.position.z + gridSize - (int)ground.transform.position.z) / 2;
        int scaleDiffX = (int)(scale.x -2) / 2;
        int scaleDiffZ = (int)(scale.z -2) / 2;
        int tileDispo = 0;
        if (scaleDiffX != 0 && scaleDiffZ != 0)
        {
            for (int z = 0; z <= scaleDiffZ; z++)
            {
                for (int x = 0; x <= scaleDiffX; x++)
                {
                    if (!tileState[posX + z, posZ + x])
                    {
                        tileDispo++;
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
