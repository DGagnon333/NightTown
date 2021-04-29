using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    public Transform StartPosition;//La postition de départ pour le pathfinding. 
    public LayerMask Building;//Permet de contourner les obstacles. 
    public Vector2 gridSize;//Grandeur du grid. 
    public float nodeRadius;//Détermine comment gros sera chaque case du grid. 

    Node[,] NodeArray;//La matrice de noeuds. 
    public List<Node> FinalPath;//La liste des noeuds du chemin trouvés par le pathfinding.  


    float nodeDiameter;
    int gridSizeInX, gridSizeInY;//Taille du grid en unités pour la matrice.

    public GameObject cube;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;//Trouver le diamètre à partir du rayon. 
        gridSizeInX = Mathf.RoundToInt(gridSize.x / nodeDiameter);//Trouve la grandeur en x de chaque case du grid.
        gridSizeInY = Mathf.RoundToInt(gridSize.y / nodeDiameter);//Trouve la grandeur en y de chaque case du grid.
        CreateGrid();
    }



    void CreateGrid()
    {
        NodeArray = new Node[gridSizeInX, gridSizeInY];//Matruce de noeuds de la grosseur du grid.
        Vector3 bottomLeft = transform.position - Vector3.right * gridSize.x / 2 - Vector3.forward * gridSize.y / 2;//Prend la position dans le monde à partir d'en bas à gauche du grid.
        for (int x = 0; x < gridSizeInX; x++)
        {
            for (int y = 0; y < gridSizeInY; y++)//Les deux boucles for sont sont pour passer au travers de la matrice de noeuds. 
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);//Les coordonnées du monde. 
                bool isBuilding = true;//Faire du noeud comme s'il était sur un bâtiment et on va vérifier après s'il l'est et changer le "true" en "false" s'il ne l'est pas. 


                if (Physics.CheckSphere(worldPoint, nodeRadius, Building))
                {
                    isBuilding = false;//Dit que ce n'est pas un bâtiment s'il n'a pas de bâtiment à contourner. 
                }

                NodeArray[x, y] = new Node(isBuilding, worldPoint, x, y);//Crée un nouveau noeud.
            }
        }
    }

    //Trouve la distance par rapport au monde. 
    public Node NodeFromWorldPoint(Vector3 thePosition)//Trouve la distance du noeud par rapport au monde. 
    {
        float inX = ((thePosition.x + gridSize.x / 2) / gridSize.x);
        float inY = ((thePosition.z + gridSize.y / 2) / gridSize.y);

        inX = Mathf.Clamp01(inX);
        inY = Mathf.Clamp01(inY);

        int x = Mathf.RoundToInt((gridSizeInX - 1) * inX);
        int y = Mathf.RoundToInt((gridSizeInY - 1) * inY);

        return NodeArray[x, y];
    }

    public List<Node> GetNeighboringNodes(Node nodesAround)
    {
        List<Node> NeighborList = new List<Node>();//Une liste de tous les noeuds autour du noeud présent. 
        int lookAtX;//Regarde si la position en x n'est pas plus grande que la matrice.
        int lookAtY;//Regarde si la position en y n'est pas plus grande que la matrice.

        //Regarde à droite du noeud présent.
        lookAtX = nodesAround.positionInX + 1;
        lookAtY = nodesAround.positionInY;
        if (lookAtX >= 0 && lookAtX < gridSizeInX)//Si la position en x n'est pas plus grande que la matrice.
        {
            if (lookAtY >= 0 && lookAtY < gridSizeInY)//Si la position en y n'est pas plus grande que la matrice.
            {
                NeighborList.Add(NodeArray[lookAtX, lookAtY]);//Ajoute au grid le noeud à la liste de noeuds autour. 
            }
        }
        //Regarde à gauche du noeud présent.
        lookAtX = nodesAround.positionInX - 1;
        lookAtY = nodesAround.positionInY;
        if (lookAtX >= 0 && lookAtX < gridSizeInX)//Si la position en x n'est pas plus grande que la matrice.
        {
            if (lookAtY >= 0 && lookAtY < gridSizeInY)//Si la position en y n'est pas plus grande que la matrice.
            {
                NeighborList.Add(NodeArray[lookAtX, lookAtY]);//Ajoute au grid le noeud à la liste de noeuds autour. 
            }
        }
        //Regarde en haut du noeud présent.
        lookAtX = nodesAround.positionInX;
        lookAtY = nodesAround.positionInY + 1;
        if (lookAtX >= 0 && lookAtX < gridSizeInX)//Si la position en x n'est pas plus grande que la matrice.
        {
            if (lookAtY >= 0 && lookAtY < gridSizeInY)//Si la position en y n'est pas plus grande que la matrice.
            {
                NeighborList.Add(NodeArray[lookAtX, lookAtY]);//Ajoute au grid le noeud à la liste de noeuds autour. 
            }
        }
        //Regarde en bas du noeud présent.
        lookAtX = nodesAround.positionInX;
        lookAtY = nodesAround.positionInY - 1;
        if (lookAtX >= 0 && lookAtX < gridSizeInX)//Si la position en x n'est pas plus grande que la matrice.
        {
            if (lookAtY >= 0 && lookAtY < gridSizeInY)//Si la position en y n'est pas plus grande que la matrice.
            {
                NeighborList.Add(NodeArray[lookAtX, lookAtY]);//Ajoute au grid le noeud à la liste de noeuds autour. 
            }
        }

        return NeighborList;//Donne la liste des noeuds autour.
    }

   
    private void Update()
    {
        if (NodeArray != null)//Si le grid n'est pas vide
        {
            if (FinalPath != null)//Si un chemin final a été trouvé.
            {
                 foreach (Node n in FinalPath)
                 {
                    Instantiate(cube, n.worldPosition, cube.transform.rotation);//Mettre des cubes pour montrer le chemin. 
                 }
            }
        }
    }
}
