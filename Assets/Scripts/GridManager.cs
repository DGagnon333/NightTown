//fait par Dérick

using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridSize = 50;
    public Vector3[,] array;
    private GameObject[] tiles;
    private int step = 2;
    public Dictionary<Vector3, bool> tileState;
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
        array = new Vector3[gridSize, gridSize];
        tileState = new Dictionary<Vector3, bool>(gridSize);
        for (int z = 0; z < gridSize; z++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                array[z, x] = (new Vector3(x * step, 0, z * step) + new Vector3(1, 0, 1) * (-gridSize));
                tileState[array[z, x]] = true;
                Instantiate(tiles[0], new Vector3(x * step, (float)(-0.08), z * step) + new Vector3(1, 0, 1) * (-gridSize), Quaternion.identity);
                //ici on créer des tiles jusqu'à la grandeur de la grille voulue, à chaque bon du step et on diminue le vecteur par la taille 
                //de la grille pour qu'ils soient centré lors de leur création.
            }
        }
    }
    public void TileState(GameObject newBuilding, Transform tf)
    {
        if (tileState[tf.position])
        {
            Instantiate(newBuilding, tf.position, Quaternion.identity);
            tileState[tf.position] = false;
        }
    }

}
