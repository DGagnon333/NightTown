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
    [SerializeField] public int gridSize = 30;
    private int step = 2;
    public bool[,] tileState;
    [SerializeField] public GameObject ground;
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
        Obstacles();
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
        newBuilding.transform.position = tf.position;

        if (newBuilding.CompareTag("Eraser"))
        {
            Eraser(newBuilding);
        }
        else
        {

            //pour un bâtiment qui mesure plus qu'une case
            if (scaleDiffX != 0 && scaleDiffZ != 0 || isEraser)
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
                    if (!isEraser)
                        buildingClone = Instantiate(newBuilding, position + new Vector3 (1,0,1) + ground.transform.position, Quaternion.identity);
                    for (int z = 0; z <= scaleDiffZ; z++)
                    {
                        for (int x = 0; x <= scaleDiffX; x++)
                        {
                            buildingTiles.Add(new Point2D(posX + x, posZ + z), buildingClone);
                            Debug.Log(buildingTiles.Count() + "posX : " +( posX + x )+ "posZ : " + (posZ+z));
                            tileState[posX + x, posZ + z] = false;
                        }
                    }
                }

            }
            //pour un bâtiment qui mesure une cased
            if (tileState[posX, posZ] && scaleDiffX == 0)
            {
                buildingTiles.Add(new Point2D(posX, posZ), Instantiate(newBuilding, position + ground.transform.position, Quaternion.identity));
                tileState[posX, posZ] = false;
            }
        }

        //si le bâtiment est un "wire"
        if (newBuilding.CompareTag("Wire"))
        {
            Wire(electricity, posX, posZ, newBuilding, position, buildingTiles);
        }

        if (!newBuilding.CompareTag("Wire"))
            wireQueue.Clear();

    }
    public void Eraser(GameObject newBuilding)
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
    }
    private void Wire(Electricity electricity, int posX, int posZ, GameObject newBuilding, Vector3 position, Dictionary<Point2D, GameObject> buildingTiles)
    {
        if (wireQueue.Count != 0)
        {
            Point2D PositionSource = new Point2D(posX, posZ);
            int posXOld = (int)(wireQueue[wireQueue.Count - 1].x + gridSize - (int)ground.transform.position.x) / 2;
            int posZOld = (int)(wireQueue[wireQueue.Count - 1].z + gridSize - (int)ground.transform.position.z) / 2;
            Point2D PositionDestination = new Point2D(posXOld, posZOld);
            wireLenght = electricity.ElectrictyState(gridSize, tileState, PositionSource, PositionDestination, newBuilding, electrictyMap, buildingTiles);
            wireQueue.Add(position);
        }

        else
            wireQueue.Add(position);
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
}
