﻿//Classe qui permet de créer une grille et d'avoir l’état des cases dans la grille pour voir si
//elles sont disponibles.
//fait par Dérick

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using UnityEditorInternal;
using System.Linq;

public class GridManager : MonoBehaviour
{
    [SerializeField] public int gridSize = 30;
    private int step = 2;
    public bool[,] tileState;
    [SerializeField] public GameObject ground;
    public bool[,] electrictyMap;
    public int wireLenght;
    int posBaseX = 0;
    int posBaseZ = 0;
    [SerializeField] public GameObject baseCopy;

    private void Awake()
    {
        ArrayCreation();
        BaseCreation();
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
        Obstacles();
    }

    private void BaseCreation()
    {
        posBaseX = (int)(baseCopy.transform.position.x - 1 + gridSize - (int)GetComponent<GridManager>().ground.transform.position.x) / 2;
        posBaseZ = (int)(baseCopy.transform.position.z - 1 + gridSize - (int)GetComponent<GridManager>().ground.transform.position.z) / 2;
        int scaleDiffX = (int)(baseCopy.transform.localScale.x - 2) / 2;
        int scaleDiffZ = (int)(baseCopy.transform.localScale.z - 2) / 2;
        for (int z = 0; z <= scaleDiffZ; z++)
        {
            for (int x = 0; x <= scaleDiffX; x++)
            {
                tileState[posBaseX + x, posBaseZ + z] = false;
                electrictyMap[posBaseX + x, posBaseZ + z] = true;
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
    public void TileState(GameObject newBuilding, Transform tf, Vector3 scale, Dictionary<Point2D, GameObject> buildingTiles, List<GameObject> wireList, Dictionary<List<GameObject>, bool> wireDictionary)
    {
        int tileDispo = 0;
        Electricity electricity = new Electricity();
        //on change la position et la taille du transform pour que ce soit proportionel à la grille
        int posX = (int)(tf.position.x + gridSize - (int)ground.transform.position.x) / 2;
        int posZ = (int)(tf.position.z + gridSize - (int)ground.transform.position.z) / 2;
        int scaleDiffX = (int)(scale.x - 2) / 2;
        int scaleDiffZ = (int)(scale.z - 2) / 2;
        Vector3 position = new Vector3(posX * step - gridSize, 0, posZ * step - gridSize);
        bool isAvailable = false;
        GameObject buildingClone = newBuilding; //simplement pour l'instancier
        newBuilding.transform.position = tf.position;

        if (newBuilding.CompareTag("Eraser"))
        {
            Eraser(newBuilding, wireDictionary);
        }
        else
        {
            //pour un bâtiment qui mesure plus qu'une case
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
                    buildingClone = Instantiate(newBuilding, position + new Vector3(1, 0, 1) + ground.transform.position, Quaternion.identity);
                    for (int z = 0; z <= scaleDiffZ; z++)
                    {
                        for (int x = 0; x <= scaleDiffX; x++)
                        {
                            buildingTiles.Add(new Point2D(posX + x, posZ + z), buildingClone);
                            tileState[posX + x, posZ + z] = false;
                        }
                    }
                }

            }
            //pour un bâtiment qui mesure une cased
            if (tileState[posX, posZ] && scaleDiffX == 0 && scaleDiffZ == 0)
            {
                buildingClone = Instantiate(newBuilding, position + ground.transform.position, Quaternion.identity);
                buildingTiles.Add(new Point2D(posX, posZ), buildingClone);
                isAvailable = true;
                tileState[posX, posZ] = false;
            }
        }

        //si le bâtiment est un "wire"
        if (newBuilding.CompareTag("Wire") && isAvailable)
        {
            Wire(electricity, posX, posZ, buildingClone, buildingTiles, wireList, wireDictionary);
        }

        if (!newBuilding.CompareTag("Wire"))
        {
            wireList.Clear();
        }

    }
    public void Eraser(GameObject newBuilding, Dictionary<List<GameObject>, bool> wireDictionary)
    {
        //j'utilise une propriété GetComponent même pour les propriétés de la même classe pour que les autres scripts n'aient pas à le faire aussi.
        //donc la fonction a seulement besoin d'avoir un GameObject en intrants et tout le reste est fait.
        int posX = (int)(newBuilding.transform.position.x + gridSize - (int)GetComponent<GridManager>().ground.transform.position.x) / 2;
        int posZ = (int)(newBuilding.transform.position.z + gridSize - (int)GetComponent<GridManager>().ground.transform.position.z) / 2;
        int keyX;
        int keyZ;
        bool isDestroyed = false;
        int scaleDiffX;
        int scaleDiffZ;
        GameObject destroyedObject = new GameObject();
        Dictionary<Point2D, GameObject> buildingTiles = GetComponent<BuildingManager>().buildingTiles;
        Dictionary<Point2D, GameObject> buildingTilesCopy = new Dictionary<Point2D, GameObject>();
        foreach (var i in buildingTiles)
        {
            buildingTilesCopy.Add(i.Key, i.Value);
        }
        foreach (var i in buildingTiles)
        {
            scaleDiffX = (int)(i.Value.transform.localScale.x - 2) / 2;
            scaleDiffZ = (int)(i.Value.transform.localScale.z - 2) / 2;
            if (scaleDiffX == 0 && scaleDiffZ == 0)
            {
                for (int z = 0; z <= scaleDiffZ; z++)
                {
                    for (int x = 0; x <= scaleDiffX; x++)
                    {
                        keyX = i.Key.X + x;
                        keyZ = i.Key.Z + z;
                        if (posX == keyX && posZ == keyZ)
                        {
                            isDestroyed = true;
                            destroyedObject = i.Value;
                            break;
                        }
                    }
                }
            }

            for (int z = 0; z < scaleDiffZ; z++)
            {
                for (int x = 0; x < scaleDiffX; x++)
                {
                    keyX = i.Key.X + x;
                    keyZ = i.Key.Z + z;
                    if (posX == keyX && posZ == keyZ)
                    {
                        isDestroyed = true;
                        destroyedObject = i.Value;
                        break;
                    }
                }
            }

        }

        if (isDestroyed)
        {
            foreach (var i in buildingTilesCopy)
            {
                if (i.Value == destroyedObject)
                {
                    tileState[i.Key.X, i.Key.Z] = true;
                    electrictyMap[i.Key.X, i.Key.Z] = false;
                    buildingTiles.Remove(i.Key);
                }
            }
            foreach (var i in buildingTilesCopy)
            {
                if (i.Key.X == posX && i.Key.Z == posZ)
                {
                    Destroy(i.Value);
                }
            }
        }
        if (destroyedObject.CompareTag("Wire"))
        {
            foreach (var i in wireDictionary)
            {
                ClearMap(electrictyMap);
                WireList(electrictyMap, i.Key);
            }
        }
    }
    private void Wire(Electricity electricity, int posX, int posZ, GameObject newBuilding, Dictionary<Point2D, GameObject> buildingTiles, List<GameObject> wireList, Dictionary<List<GameObject>, bool> wireDictionary)
    {

        if (wireList.Count != 0)
        {
            wireList.Add(newBuilding);
            int posXOld = (int)(wireList[wireList.Count - 2].transform.position.x + gridSize - (int)ground.transform.position.x) / 2;
            int posZOld = (int)(wireList[wireList.Count - 2].transform.position.z + gridSize - (int)ground.transform.position.z) / 2;
            Point2D PositionSource = new Point2D(posX, posZ);
            Point2D PositionDestination = new Point2D(posXOld, posZOld);

            wireList = electricity.ElectrictyState(gridSize, tileState, PositionSource, PositionDestination, newBuilding, electrictyMap, buildingTiles, wireList);

            wireDictionary.Add(wireList, WireList(electrictyMap, wireList));

        }

        if (wireList.Count == 0)
        {
            wireList.Add(newBuilding);
        }
    }
    private bool WireList(bool[,] electrictyMap, List<GameObject> wireList)
    {
        bool conection = false;
        //int nextX = (gridSize - (int)ground.transform.position.x) / 2;
        //int nextZ = (gridSize - (int)ground.transform.position.x) / 2;
        int nextX = 0;
        int nextZ = 0;
        foreach (GameObject i in wireList)
        {
            nextX = (int)(i.transform.position.x + gridSize - (int)ground.transform.position.x) / 2;
            nextZ = (int)(i.transform.position.z + gridSize - (int)ground.transform.position.z) / 2;
            if (electrictyMap[nextX + 1, nextZ] || electrictyMap[nextX - 1, nextZ] || electrictyMap[nextX, nextZ + 1] || electrictyMap[nextX, nextZ - 1])
            {
                conection = true;
                break;
            }
        }
        foreach (GameObject i in wireList)
        {
            nextX = (int)(i.transform.position.x + gridSize - (int)ground.transform.position.x) / 2;
            nextZ = (int)(i.transform.position.z + gridSize - (int)ground.transform.position.z) / 2;
            electrictyMap[nextX, nextZ] = conection;
            if (conection)
                i.GetComponent<Renderer>().material.color = Color.green;
            else
                i.GetComponent<Renderer>().material.color = Color.grey;
        }
        return conection;
    }
    private void Obstacles()
    {
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Obstacle"))
        {
            int obstacleScaleX = (int)Math.Ceiling((i.transform.localScale.x - 2) / 2);
            int obstacleScaleZ = (int)Math.Ceiling((i.transform.localScale.z - 2) / 2);
            for (int z = 0; z <= obstacleScaleZ; z++)
            {
                for (int x = 0; x <= obstacleScaleX; x++)
                {
                    int posX = (int)(i.transform.position.x + gridSize - (int)ground.transform.position.x) / 2;
                    int posZ = (int)(i.transform.position.z + gridSize - (int)ground.transform.position.z) / 2;
                    tileState[(int)Math.Floor((float)posX) + x, (int)Math.Floor((float)posZ) + z] = false;
                }
            }
        }
    }

    private void ClearMap(bool[,] map)
    {
        for (int z = 0; z < map.GetLength(1) - 1; z++)
        {
            for (int x = 0; x < map.GetLength(0) - 1; x++)
            {
                map[x, z] = false;
            }
        }
        BaseCreation();
    }
}
