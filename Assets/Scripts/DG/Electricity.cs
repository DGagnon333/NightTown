//Ce script a été inspiré par ma tp3 dans la session 3 avec Nassour Nassour, travail qui portait sur le pathfinding.
// Cette classe permet d'instancié des objets d'une source jusqu'à une destination, et créer un tableau de bool
// qui retient l'emplacement de chacun de ces objets

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.UIElements;

public class Electricity : MonoBehaviour
{
    public int wireLenght = 0;
    //-----------------------------------------------------------------------
    //[SerializeField] GameObject baseCopy;
    //List<GameObject> allo = new List<GameObject>();
    //Dictionary<List<GameObject>, bool> wireDictionary = new Dictionary<List<GameObject>, bool>();
    //private void Start()
    //{
    //    allo.Add(baseCopy);
    //    wireDictionary.Add(allo, true);
    //}
    //-----------------------------------------------------------------------
    public int ElectrictyState(int gridSize, bool[,] tileState, Point2D PositionSource, Point2D PositionDestination, GameObject wire, bool[,] electrictyMap, Dictionary<Point2D, GameObject> buildingTiles)
    {
        Point2D current = new Point2D(PositionSource.X, PositionSource.Z);
        Queue<Point2D> frontier = new Queue<Point2D>();
        Dictionary<Point2D, Point2D> cameFrom = new Dictionary<Point2D, Point2D>();
        List<Point2D> voisin = new List<Point2D>();
        List<Point2D> path = new List<Point2D>();
        Point2D first = new Point2D(1, 1);
        Point2D next;
        bool isEmpty = false;

        bool[,] newMap = CreateNewMap(tileState, gridSize);
        frontier.Enqueue(current);
        cameFrom.Add(PositionSource, PositionSource);

        while (frontier.Count != 0)
        {
            if (frontier.Count == 0)
            {
                isEmpty = true;
                break;
            }
            current = frontier.Dequeue();
            //Sud
            if (((current.Z - 1) >= 0) && newMap[current.Z - 1, current.X])
            {
                voisin.Add(new Point2D(current.X, current.Z - 1));
                newMap[current.Z - 1, current.X] = false;
            }

            // Est
            if (((current.X + 1) < gridSize) && newMap[current.Z, current.X + 1])
            {
                voisin.Add(new Point2D(current.X + 1, current.Z));
                newMap[current.Z, current.X + 1] = false;
            }

            // Nord
            if (((current.Z + 1) < gridSize) && newMap[current.Z + 1, current.X])
            {
                voisin.Add(new Point2D(current.X, current.Z + 1));
                newMap[current.Z + 1, current.X] = false;
            }

            // Ouest
            if (((current.X - 1) >= 0) && newMap[current.Z, current.X - 1])
            {
                voisin.Add(new Point2D(current.X - 1, current.Z));
                newMap[current.Z, current.X - 1] = false;
            }

            if (PositionDestination.X == current.X && PositionDestination.Z == current.Z)
            {
                voisin.Clear();
                break;
            }
            foreach (Point2D i in voisin)
            {
                if (!cameFrom.ContainsValue(i) || i == PositionSource)
                {
                    frontier.Enqueue(i);
                    cameFrom.Add(i, current);
                }
            }
            voisin.Clear();
        }
        if (isEmpty == true)
        {
            return 0;
        }
        foreach (var i in cameFrom)
        {
            //permet de voir toutes les position cherchées--
            //Instantiate(wire, new Vector3(i.Key.X * 2 - gridSize, 0, i.Key.Z * 2 - gridSize), Quaternion.identity);
            //----------------------------------------------
            if (i.Key.X == PositionDestination.X && i.Key.Z == PositionDestination.Z)
            {
                first = i.Key;
            }
        }
        path.Add(first);
        while (!(path[wireLenght].X == PositionSource.X && path[wireLenght].Z == PositionSource.Z))
        {
            next = cameFrom[path[wireLenght]];
            path.Add(next);
            if (tileState[next.X, next.Z])
            {
                Debug.Log("allo");
                GameObject newWire = Instantiate(wire, new Vector3(next.X * 2 - gridSize, 0, next.Z * 2 - gridSize), Quaternion.identity);
                buildingTiles.Add(new Point2D(next.X, next.Z), newWire);
                //-----------------------------------------------------------------------------
                //WireList(newWire, electrictyMap);
                //-----------------------------------------------------------------------------
            }
            wireLenght++;

            tileState[next.X, next.Z] = false; //on REND la position de chaque fils électriques non disponible
            electrictyMap[next.X, next.Z] = true; //ici on RETIENT la position des fils électriques
        }
        return wireLenght + 1;
    }
    //-----------------------------------------------------------------------------

    //private void WireList(GameObject wire, bool[,] electrictyMap)
    //{
    //    List<GameObject> allo2 = new List<GameObject>();
    //    bool conection = false;
    //    allo2.Add(wire);
    //    foreach (GameObject i in allo2)
    //    {
    //        conection = IsConnected(i, electrictyMap);
    //        if (conection)
    //            break;

    //    }
    //    wireDictionary.Add(allo2, conection);
    //    foreach (var i in wireDictionary)
    //    {
    //        Debug.Log(i.Key[0].name + ", " + conection);
    //    }
    //}
    //public bool IsConnected(GameObject wireList, bool[,] electrictyMap)
    //{
    //    bool connected = false;
    //    int posX = (int)(wireList.transform.position.x + 200) / 2;
    //    int posZ = (int)(wireList.transform.position.z + 200) / 2;

    //    if (electrictyMap[posX + 1, posZ] || electrictyMap[posX - 1, posZ] || electrictyMap[posX, posZ + 1] || electrictyMap[posX, posZ - 1])
    //    {
    //        connected = true;
    //    }

    //    return connected;
    //}
    //-----------------------------------------------------------------------------

    private bool[,] CreateNewMap(bool[,] tileState, int gridSize)
    {
        bool[,] newMap = new bool[gridSize, gridSize];
        for (int z = 0; z < gridSize; z++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                //newMap[z, x] = tileState[z, x];
                newMap[x, z] = true;
            }
        }
        return newMap;
    }
}
