using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Electricity : MonoBehaviour
{
    public void ElectrictyState(int gridSize, bool[,] tileState, int posX, int posZ, int posXOld, int posZOld, GameObject wire)
    {
        Point2D PositionSource = new Point2D(posX, posZ);
        Point2D PositionDestination = new Point2D(posXOld, posZOld);
        Point2D current = new Point2D(posX, posZ);
        Queue<Point2D> frontier = new Queue<Point2D>();
        Dictionary<Point2D, Point2D> cameFrom = new Dictionary<Point2D, Point2D>();
        List<Point2D> voisin = new List<Point2D>();
        List<Point2D> path = new List<Point2D>();
        Point2D first = new Point2D(1, 1);
        Point2D next;
        frontier.Enqueue(current);
        bool[,] electrictyMap = CreateElectricityMap(gridSize);
        bool[,] newMap = CreateNewMap(tileState, gridSize);
        cameFrom.Add(PositionSource, PositionSource);
        while (frontier != null)
        {
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

            foreach (Point2D i in voisin)
            {
                if (!cameFrom.ContainsValue(i) || (i == PositionSource))
                {
                    frontier.Enqueue(i);
                    cameFrom[i] = current;
                }
            }
            if (PositionDestination.X == current.X && PositionDestination.Z == current.Z) 
            {
                break;
            }
            voisin.Clear();
        }

        
        foreach (var i in cameFrom)
        {
            if (i.Key.X == PositionDestination.X && i.Key.Z == PositionDestination.Z)
            {
                first = cameFrom[i.Key];
            }
        }
        path.Add(first);

        int pt = 0;
        while (!(path[pt].X == PositionSource.X && path[pt].Z == PositionSource.Z)) 
        {
            next = cameFrom[path[pt]];
            path.Add(next);
            if (tileState[next.X, next.Z])
                Instantiate(wire, new Vector3(next.X* 2 - gridSize, 0, next.Z *2-gridSize), Quaternion.identity);
            tileState[next.X, next.Z] = false; //on REND la position de chaque fils électriques non disponible
            electrictyMap[next.X, next.Z] = true; //ici on RETIENT la position des fils électriques disponibles
            pt++;
        }
    }
    private bool[,] CreateNewMap(bool[,] tileState, int gridSize)
    {
        bool[,] newMap = new bool[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                newMap[x, z] = tileState[x, z];
            }
        }
        return newMap;
    }
    public bool[,] CreateElectricityMap(int gridSize)
    {
        bool[,] electricityMap = new bool[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                electricityMap[x, z] = false;
            }
        }
        return electricityMap;
    }
    public class Point2D
    {
        public int X { get; private set; }
        public int Z { get; private set; }
        public Point2D(int x, int z)
        {
            X = x;
            Z = z;
        }
    }
}
