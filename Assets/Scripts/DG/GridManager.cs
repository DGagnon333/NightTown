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
using System.Linq;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridSize = 30;
    private int step = 2;
    private bool[,] tileState;
    [SerializeField] private GameObject ground;
    public List<Vector3> wireQueue = new List<Vector3>();
    public bool[,] electrictyMap;
    public int wireLenght;

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
        electrictyMap = new bool[gridSize, gridSize];
        for (int z = 0; z < gridSize; z++)
        {

            for (int x = 0; x < gridSize; x++)
            {
                tileState[x, z] = true;
                electrictyMap[x, z] = false;
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
    public void TileState(GameObject newBuilding, Transform tf, Vector3 scale, Dictionary<Point2D, GameObject> buildingTiles)
    {
        int tileDispo = 0;
        Electricity electricity = new Electricity();

        //on change la position et la taille du transform pour que ce soit proportionel à la grille
        int posX = (int)(tf.position.x + gridSize - (int)ground.transform.position.x) / 2;
        int posZ = (int)(tf.position.z + gridSize - (int)ground.transform.position.z) / 2;
        int scaleDiffX = (int)(scale.x - 2) / 2;
        int scaleDiffZ = (int)(scale.z - 2) / 2;
        Vector3 position = new Vector3(posX * step - gridSize, 0, posZ * step - gridSize);
        bool isEraser = false;
        GameObject buildingClone = newBuilding; //simplement pour l'instancier
        bool isDestroyed = false;

        if (newBuilding.CompareTag("Eraser"))
        {
            isEraser = true;
        }

        //pour un bâtiment qui mesure plus qu'une case
        if (scaleDiffX != 0 && scaleDiffZ != 0 || isEraser)
        {
            for (int z = 0; z <= scaleDiffZ; z++)
            {
                for (int x = 0; x <= scaleDiffX; x++)
                {
                    if (!tileState[posX + z, posZ + x])
                    {
                        if (isEraser)
                        {
                            isDestroyed = Eraser(newBuilding, buildingTiles, posX, posZ, x, z);
                        }
                        else
                        {
                            tileDispo++;
                        }
                    }
                }
            }
            if (tileDispo == 0)
            {
                if (!isEraser)
                    buildingClone = Instantiate(newBuilding, position + new Vector3(1, 0, 1) + ground.transform.position, Quaternion.identity);
                for (int z = 0; z <= scaleDiffZ; z++)
                {
                    for (int x = 0; x <= scaleDiffX; x++)
                    {
                        buildingTiles.Add(new Point2D(posX + x, posZ + z), buildingClone);
                        if (isDestroyed)
                            tileState[posX + x, posZ + z] = false;
                        else
                            tileState[posX + x, posZ + z] = false;
                    }
                }
            }

        }
            //pour un bâtiment qui mesure une cased
            if (tileState[posX, posZ] && scaleDiffX == 0)
            {
                if (!isEraser)
                {
                    Debug.Log("allo");
                    buildingTiles.Add(new Point2D(posX, posZ), Instantiate(newBuilding, position + ground.transform.position, Quaternion.identity));
                    tileState[posX, posZ] = false;
                }
                if (isEraser)
                {
                    isDestroyed = Eraser(newBuilding, buildingTiles, posX, posZ, 0, 0);
                }
                if(isDestroyed)
                    tileState[posX, posZ] = true;
            }

            //si le bâtiment est un "wire"
            if (newBuilding.CompareTag("Wire"))
            {
                Wire(electricity, posX, posZ, newBuilding, position, buildingTiles);
            }

            if (!newBuilding.CompareTag("Wire"))
                wireQueue.Clear();
        
    }
    private bool Eraser(GameObject newBuilding, Dictionary<Point2D, GameObject> buildingTiles, int posX, int posZ, int addX, int addZ)
    {
        int keyX;
        int keyZ;
        bool isDestroyed = false;
        foreach (var i in buildingTiles)
        {
            keyX = i.Key.X + addX;
            keyZ = i.Key.Z + addZ;
            Debug.Log((i.Key.X + addX) + ", " + (i.Key.Z + addZ));
            if (posX == keyX && posZ == keyZ)
            {
                Debug.Log(posX + ", " + posZ);
                Destroy(i.Value);
                isDestroyed = true;
            }
        }
        return isDestroyed;
    }
    private void Wire(Electricity electricity, int posX, int posZ, GameObject newBuilding, Vector3 position, Dictionary<Point2D, GameObject> buildingTiles)
    {
        if (wireQueue.Count != 0)
        {
            int posXOld = (int)(wireQueue[wireQueue.Count - 1].x + gridSize - (int)ground.transform.position.x) / 2;
            int posZOld = (int)(wireQueue[wireQueue.Count - 1].z + gridSize - (int)ground.transform.position.z) / 2;
            wireLenght = electricity.ElectrictyState(gridSize, tileState, posX, posZ, posXOld, posZOld, newBuilding, electrictyMap, buildingTiles);
            wireQueue.Add(position);
        }

        else
            wireQueue.Add(position);
    }
}
