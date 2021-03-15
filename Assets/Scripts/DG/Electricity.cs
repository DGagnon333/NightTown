using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Electricity : MonoBehaviour
{
    public void Awake()
    {

    }
    public void ElectrictyState(int gridSize, bool[,] tileState, Transform newBuilding, Transform oldBuilding, int groundPosX, int groundPosZ)
    {
        //int scaleX = (int)(transform.localScale.x); 
        //int scaleZ = (int)(transform.localScale.z);
        //Debug.Log(scaleX);
        //int nb = 0;
        //for (int x = scaleX; x <= scaleX*2; x++)
        //{
        //    for (int z = scaleZ; z <= scaleZ*2; z++)
        //    {
        //        nb++;
        //        Debug.Log(nb);
        //    }
        //}
        ///////////////////////////////////////////////////////
        int posX = ((int)newBuilding.position.x + gridSize - groundPosX) / 2;
        int posZ = ((int)newBuilding.position.z + gridSize - groundPosZ) / 2;
        int posXOld = ((int)oldBuilding.position.x + gridSize - groundPosX) / 2;
        int posZOld = ((int)oldBuilding.position.z + gridSize - groundPosZ) / 2;
        bool[,] nouvCarte = CréerNouvCarte(tileState, gridSize);
        Point2D PositionSource = new Point2D(posX, posZ);
        Point2D PositionDestination = new Point2D(posXOld, posZOld);
        Point2D current = new Point2D(posX, posZ);
        Queue<Point2D> frontier = new Queue<Point2D>();
        frontier.Enqueue(current);

        Dictionary<Point2D, Point2D> cameFrom = new Dictionary<Point2D, Point2D>();
        cameFrom.Add(PositionSource, PositionSource);
        List<Point2D> voisin = new List<Point2D>();


        while (frontier != null)
        {

            current = frontier.Dequeue();
            //Nord
            if (((current.Z - 1) >= 0) && nouvCarte[current.Z - 1, current.X])
            {
                voisin.Add(new Point2D(current.X, current.Z - 1));
                nouvCarte[current.Z - 1, current.X] = false;
            }

            // Est
            if (((current.X + 1) < gridSize) && nouvCarte[current.Z, current.X + 1])
            {
                voisin.Add(new Point2D(current.X + 1, current.Z));
                nouvCarte[current.Z, current.X + 1] = false;
            }

            // Sud
            if (((current.Z + 1) < gridSize) && nouvCarte[current.Z + 1, current.X])
            {
                voisin.Add(new Point2D(current.X, current.Z + 1));
                nouvCarte[current.Z + 1, current.X] = false;
            }

            // Ouest
            if (((current.X - 1) >= 0) && nouvCarte[current.Z, current.X - 1])
            {
                voisin.Add(new Point2D(current.X - 1, current.Z));
                nouvCarte[current.Z, current.X - 1] = false;
            }

            if (PositionDestination.X == current.X && PositionDestination.Z == current.Z)
            {
                break;
            }


            foreach (Point2D i in voisin)
            {
                //if (i.X == PositionSource.X && i.Z == PositionSource.Z)
                //    Debug.Log(i.X + ", " + i.Z);
                //if (i.Equals(PositionSource))
                //    Debug.Log("2");

                if (!cameFrom.ContainsValue(i) || (i.X == PositionSource.X && i.Z == PositionSource.Z)) //ici i.Equals(PositionSource) ne marchait pas
                {
                    frontier.Enqueue(i);
                    cameFrom[i] = current;
                }
            }
            voisin.Clear();
        }

        Debug.Log(posXOld + ", " + posXOld + "--> Position Destination");

        List<Point2D> chemin = new List<Point2D>();
        Point2D premier = new Point2D(PositionDestination.X, PositionDestination.Z);
        foreach (var i in cameFrom)
        {
            if (i.Key.X == PositionDestination.X && i.Key.Z == PositionDestination.Z)
            {
                premier = cameFrom[i.Key];
            }
                
            //Debug.Log(i.Key.X + ", " + i.Key.Z);
        }
        Point2D suivant = premier;
        chemin.Add(premier);

        int pt = 0;

        while (!(PositionDestination.X == cameFrom[suivant].X && PositionDestination.X == cameFrom[suivant].Z)) //ici !cameFrom[suivant].Equals(PositionDestination) ne marchait pas
        {
            Debug.Log("test4");
            suivant = cameFrom[chemin[pt]];
            chemin.Add(suivant);
            Instantiate(newBuilding, new Vector3((suivant.X)* 2 - gridSize, 0, (suivant.Z *2)-gridSize), Quaternion.identity);
            pt++;
        }
    }
    private bool[,] CréerNouvCarte(bool[,] tileState, int gridSize)
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
