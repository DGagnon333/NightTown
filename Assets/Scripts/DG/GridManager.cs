//Classe qui permet de créer une grille et d'avoir l’état des cases dans la grille pour voir si
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

    [SerializeField] public int gridSize = 750; //la taille de la matrice
    private int STEP = 2; //la grandeur de pour chaque case
    public bool[,] tileState; //le tableau qui s'occupe de la disponibilité de chaque case
    public bool[,] electrictyMap; //le tableau qui s'occupe de l'alimentation de chaque case
    int posBaseX = 0; //la position initiale de la base
    int posBaseZ = 0;
    [SerializeField] public GameObject baseCopy; //
    [SerializeField] private Material electricMat;
    [SerializeField] private Material noElectricity;
    [SerializeField] private GameObject connected;
    [SerializeField] private GameObject UnConnected;

    /// <summary>
    /// initialisation de la matrice
    /// </summary>
    private void Awake()
    {
        ArrayCreation();
        BaseCreation();

    }
    /// <summary>
    /// initialise chaque case de la matrice comme étant disponible et non-alimenté
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
    /// S'occupe de rendre la position de la base dans le monde non
    /// disponible et mettre les cases qu'elle occupe alimentées
    /// </summary>
    private void BaseCreation()
    {
        posBaseX = (int)(baseCopy.transform.position.x - 1 + gridSize) / 2;
        posBaseZ = (int)(baseCopy.transform.position.z - 1 + gridSize) / 2;
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
    /// Cette fonction permet de vérifier si une case est disponible ou non. Si elle l'est, 
    /// un objet est placé à l'endroit souhaité et les tuiles utilisé par cet objet seront
    /// ensuite indisponible.
    /// </summary>
    /// <param name = "newBuilding" > le bâtiment choisi</param>
    /// <param name = "tf" > la position du "phantôme" de l'objet sélectionné</param>
    /// <param name = "scale" > la taille de l'objet sélectionné</param>
    public void TileState(GameObject newBuilding, Transform tf, Vector3 scale, Dictionary<Point2D, GameObject> buildingTiles, List<GameObject> wireList, Dictionary<List<GameObject>, bool> wireDictionary)
    {
        //on change la position et la taille du transform pour que ce soit proportionel à la grille
        int posX = (int)(tf.position.x + gridSize) / 2;
        int posZ = (int)(tf.position.z + gridSize) / 2;
        int scaleDiffX = (int)(scale.x - 2) / 2;
        int scaleDiffZ = (int)(scale.z - 2) / 2;


        bool tileDispo = true;
        newBuilding.transform.position = tf.position;
        Vector3 position = new Vector3(posX * STEP - gridSize, 0, posZ * STEP - gridSize);
        GameObject buildingClone = new GameObject(); //copie du bâtiment sélectionné
        Electricity electricity = new Electricity(); //Classe pour l'électricité


        if (newBuilding.CompareTag("Eraser"))
        {
            Eraser(newBuilding, wireDictionary);
        }

        if (!newBuilding.CompareTag("Eraser") && !newBuilding.CompareTag("Wire"))
        {
            for (int z = 0; z <= scaleDiffZ; z++)
            {
                for (int x = 0; x <= scaleDiffX; x++)
                {
                    if (!tileState[posX + z, posZ + x])
                    {
                        tileDispo = false;
                    }
                }
            }
            if (tileDispo)
            {
                buildingClone = Instantiate(newBuilding, position + new Vector3(1, 0, 1), Quaternion.Euler(0, 180, 0));
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

        //si le bâtiment est un fil électrique
        if (newBuilding.CompareTag("Wire"))
        {
            buildingClone = Instantiate(newBuilding, position, Quaternion.identity);
            buildingTiles.Add(new Point2D(posX, posZ), buildingClone);
            tileState[posX, posZ] = false;
            Wire(electricity, posX, posZ, buildingClone, buildingTiles, wireList, wireDictionary);
        }

        //s'occupe de créer les boules en haut des bâtiments pour voir si ils sont fonctionel ou non
        if (!newBuilding.CompareTag("Wire"))
        {
            wireList.Clear();
            if (!newBuilding.CompareTag("Eraser"))
            {
                LookForConnection(buildingTiles, buildingClone);
            }
        }

    }

    /// <summary>
    /// créer des boules en haut des bâtiment pour voir si ils sont fonctionel ou non.
    /// Si oui, la boule est bleue, si non la boule est rouge
    /// </summary>
    /// <param name="buildingTiles">le tableau de disponibilité</param>
    /// <param name="buildingClone">la copie du bâtiment sélectioné</param>
    private void LookForConnection(Dictionary<Point2D, GameObject> buildingTiles, GameObject buildingClone)
    {
        //position et taille du bâtiment
        int posX;
        int posZ;
        int scaleDiffX;
        int scaleDiffZ;

        //alimenté ou non
        bool connection;

        //la "boule"
        GameObject ball = new GameObject();

        //dernier objet regardé
        GameObject lastObject = new GameObject();


        //vérifie pour chaque bâtiment s'il est alimenté.
        foreach (var i in buildingTiles)
        {
            if (!i.Value.CompareTag("Wire") && !i.Value.CompareTag("Torche"))
            {
                connection = false;
                posX = i.Key.X;
                posZ = i.Key.Z;
                scaleDiffX = (int)(i.Value.transform.localScale.x - 2) / 2;
                scaleDiffZ = (int)(i.Value.transform.localScale.z - 2) / 2;
                for (int z = 0; z <= scaleDiffZ; z++)
                {
                    for (int x = 0; x <= scaleDiffX; x++)
                    {

                        if ((electrictyMap[posX + x + 1, posZ] || electrictyMap[posX - z, posZ] || electrictyMap[posX, posZ + z + 1] || electrictyMap[posX, posZ - z]))
                        {
                            connection = true;
                        }
                    }
                }

                //s'il est alimenté, créée une boule bleue
                if (connection && (buildingClone == i.Value) && (lastObject != i.Value))
                {
                    ball = Instantiate(connected, i.Value.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
                }

                //s'il n'est pas alimenté, créée une boule rouge
                if (!connection && (buildingClone == i.Value) && (lastObject != i.Value))
                {
                    ball = Instantiate(UnConnected, i.Value.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
                }
                ball.transform.SetParent(i.Value.transform);//on met la boule un enfant du nouveau bâtiment. De cette façon, 
                //quand on l'efface, la boule s'efface aussi.

                lastObject = i.Value;
            }


        }

    }

    /// <summary>
    /// fonction qui s'occupe d'effacer les bâtiments
    /// </summary>
    /// <param name="newBuilding"></param>
    /// <param name="wireDictionary"></param>
    public void Eraser(GameObject newBuilding, Dictionary<List<GameObject>, bool> wireDictionary)
    {
        //position et taille dans la matrice
        int posX = (int)(newBuilding.transform.position.x + gridSize) / 2;
        int posZ = (int)(newBuilding.transform.position.z + gridSize) / 2;
        int scaleDiffX;
        int scaleDiffZ;

        //position plus la différence de taille
        int keyX;
        int keyZ;

        bool isDestroyed = false;
        GameObject destroyedObject = new GameObject();
        Dictionary<Point2D, GameObject> buildingTiles = GetComponent<BuildingManager>().buildingTiles; //liste des bâtiments et de leur position
        Dictionary<Point2D, GameObject> buildingTilesCopy = new Dictionary<Point2D, GameObject>(); // copie du tableau de disponibilité

        //ici on copie le tableau puisque quand des bâtiment vont commencer à être 
        //effacer on veut garder un tableau intacte pour continuer à chercher
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

        //Si le bâtiment est un fil électrique, l'effacement se fera d'une autre façon
        if (destroyedObject.CompareTag("Wire"))
        {
            WireListArangment(posX, posZ, wireDictionary, buildingTiles);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posZ"></param>
    /// <param name="wireDictionary"></param>
    /// <param name="buildingTiles"></param>
    private void WireListArangment(int posX, int posZ, Dictionary<List<GameObject>, bool> wireDictionary, Dictionary<Point2D, GameObject> buildingTiles)
    {
        ClearMap(electrictyMap); //on réinitialise le tableau d'alimention pour le vérifier dans un nouvel ordre plus tard
        List<GameObject> listFound = new List<GameObject>();
        int nextX = 0;
        int nextZ = 0;
        Dictionary<Point2D, GameObject> buildingTilesCopy = new Dictionary<Point2D, GameObject>();
        int nbList = 0;
        foreach (var i in buildingTiles)
        {
            buildingTilesCopy.Add(i.Key, i.Value);
        }

        //on chercher le fil éléectrique à la case que l'on veut effacer
        foreach (var i in wireDictionary)
        {
            foreach (var wire in i.Key)
            {
                nextX = (int)(wire.transform.position.x + gridSize) / 2;
                nextZ = (int)(wire.transform.position.z + gridSize) / 2;
                if (nextX == posX && nextZ == posZ)
                {
                    listFound = i.Key;
                }
            }
            nbList++;
        }

        //pour le fil truové on accède à toute sa rangée 
        //(le fil complèt créer à partir de l'algorithme de recherche)
        foreach (var i in listFound)
        {
            nextX = (int)(i.transform.position.x + gridSize) / 2;
            nextZ = (int)(i.transform.position.z + gridSize) / 2;
            foreach (var j in buildingTilesCopy)
            {
                if (j.Value == i)
                {
                    tileState[j.Key.X, j.Key.Z] = true;
                    electrictyMap[j.Key.X, j.Key.Z] = false;
                    buildingTiles.Remove(j.Key);
                }
            }
            foreach (var j in buildingTilesCopy)
            {
                if (j.Key.X == nextX && j.Key.Z == nextZ)
                {
                    Destroy(j.Value);
                }
            }
        }
        wireDictionary.Remove(listFound); //on l'enleve de la liste du fil électrique


        //après l'avoir enlever, on regarde quels fils sont maintenant alimentés
        foreach (var i in wireDictionary)
        {
            WireList(electrictyMap, i.Key);
        }

    }

    private void Wire(Electricity electricity, int posX, int posZ, GameObject newBuilding, Dictionary<Point2D, GameObject> buildingTiles,
        List<GameObject> wireList, Dictionary<List<GameObject>, bool> wireDictionary)
    {
        List<GameObject> wireListCopy = new List<GameObject>();

        //S'il y a déjà un point de départ, on utilise un algorithme de recher pour se rendre jusqu'au prochain fil choisit
        if (wireList.Count != 0)
        {
            wireList.Add(newBuilding);
            int posXOld = (int)(wireList[wireList.Count - 2].transform.position.x + gridSize) / 2;
            int posZOld = (int)(wireList[wireList.Count - 2].transform.position.z + gridSize) / 2;
            Point2D PositionSource = new Point2D(posX, posZ);
            Point2D PositionDestination = new Point2D(posXOld, posZOld);


            //C'est ici qu'on accède à la classe qui s'occupera de faire l'algorithme de recherche pour placer des fils du point
            //de départ jusqu'au point d'arrivé.
            wireList = electricity.ElectrictyState(gridSize, tileState, PositionSource, PositionDestination, newBuilding, electrictyMap, buildingTiles, wireList);

            foreach (var i in wireList)
            {
                wireListCopy.Add(i);
            }
            wireDictionary.Add(wireListCopy, WireList(electrictyMap, wireList));
        }

            //s'il n'y a pas encore de point de départ, on créer le premier fil de la liste de fil
        if (wireList.Count == 0)
        {
            wireList.Add(newBuilding);
        }
    }

    /// <summary>
    /// on regarde quel fil sont alimenté et change leur couleur dépendament
    /// de leur alimentation.
    /// </summary>
    /// <param name="electrictyMap"></param>
    /// <param name="wireList"></param>
    /// <returns></returns>
    private bool WireList(bool[,] electrictyMap, List<GameObject> wireList)
    {
        bool conection = false;
        int nextX = 0;
        int nextZ = 0;
        foreach (GameObject i in wireList)
        {
            nextX = (int)(i.transform.position.x + gridSize) / 2;
            nextZ = (int)(i.transform.position.z + gridSize) / 2;
            if (electrictyMap[nextX + 1, nextZ] || electrictyMap[nextX - 1, nextZ] || electrictyMap[nextX, nextZ + 1] || electrictyMap[nextX, nextZ - 1])
            {
                conection = true;
                break;
            }
        }
        foreach (GameObject i in wireList)
        {
            nextX = (int)(i.transform.position.x + gridSize) / 2;
            nextZ = (int)(i.transform.position.z + gridSize) / 2;
            electrictyMap[nextX, nextZ] = conection;
            if (conection)
            {

                i.GetComponent<Renderer>().material = electricMat;
            }
            else
                i.GetComponent<Renderer>().material = noElectricity;
        }
        return conection;
    }

    /// <summary>
    /// on cherche la case associé à chaque obstalce et rend sa case non-indisponible
    /// </summary>
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
                    int posX = (int)(i.transform.position.x + gridSize) / 2;
                    int posZ = (int)(i.transform.position.z + gridSize) / 2;
                    tileState[(int)Math.Floor((float)posX) + x, (int)Math.Floor((float)posZ) + z] = false;
                }
            }
        }
    }

    /// <summary>
    /// réinitialisation des données pour le tableau d'alimentation
    /// </summary>
    /// <param name="map"></param>
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
