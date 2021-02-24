using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject[] buildingList;
    [SerializeField] private int gridSize = 50;
    Points[,] array;
    private GameObject[] tiles;
    private int step = 2;
    private void Awake()
    {
        tiles = GameObject.FindGameObjectsWithTag("Grid");
        ArrayCreation();
    }
    /// <summary>
    /// Créer une grille / matrice d'objets "tiles" qui créer une tuile à chaque bond
    /// poistion voulue (step)
    /// </summary>
    /// <returns></returns>
    private void ArrayCreation()
    {
        array = new Points[gridSize * step, gridSize * step];
        for (int y = 0; y < gridSize - 1; y++)
        {
            for (int x = 0; x < gridSize - 1; x++)
            {
                array[y * step, x * step] = new Points(x * step, y * step);
                Instantiate(tiles[0], new Vector3(x * step, (float)(-0.08), y * step) + new Vector3(1, 0, 1) * (-gridSize), Quaternion.identity);
                //ici on créer des tiles jusqu'à la grandeur de la grille voulue, à chaque bon du step et on diminue le vecteur par la taille 
                //de la grille pour qu'ils soient centré lors de leur création.
            }
        }
    }

}

/// <summary>
/// une classe Points ayant un int X et Y
/// </summary>
public class Points
{
    public static int X { get; private set; }
    public static int Y { get; private set; }

    public Points(int x, int y)
    {
        X = x;
        Y = y;
    }

}
