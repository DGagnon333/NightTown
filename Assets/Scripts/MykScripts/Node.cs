using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Fait par Mykael Arsenault et je me suis inspiré de ce vidéo afin de faire le Pathfinding : https://www.youtube.com/watch?v=AKKpPmxx07w
public class Node
{

    public int positionInX;//Position en x dans la matrice du noeud.
    public int positionInY;//Position en y dans la matrice du noeud.

    public bool buildingIsOnNode;//Dit s'il y a un bâtiment sur un noeud.
    public Vector3 worldPosition;//Position dans le monde du noeud.

    public Node ParentNode;//Le noeud précédent celui présent

    public int gCost;//Le coût pour passer au prochain noeud. 
    public int hCost;//La distance entre le noeud et la position d'arrivée.

    public int fCost { get { return gCost + hCost; } }//Le fCost qui est la somme du gCost et hCost.

    public Node(bool theBuildingIsOnNode, Vector3 thePosition, int thePositionInX, int thePositionInY)
    {
        buildingIsOnNode = theBuildingIsOnNode;//Dit s'il y a un bâtiment sur un noeud.
        worldPosition = thePosition;//Position dans le monde du noeud.
        positionInX = thePositionInX;//Position en x dans la matrice du noeud.
        positionInY = thePositionInY;//Position en y dans la matrice du noeud.
    }

}
