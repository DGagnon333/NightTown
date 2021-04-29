using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    Grid theGrid;//Permet d'acceder au Script/classe "Grid"
    public Transform StartPosition;//Position du départ pour le pathfinding
    public Transform TargetPosition;//Position d'arrivé pour le pathfinding

    private void Awake()
    {
        theGrid = GetComponent<Grid>();//Obtenir une référence au Grid (une matrice) pour pouvoir faire le pathfinding.
    }

    private void Update()
    {
        FindPath(StartPosition.position, TargetPosition.position);//Une fonction qui trouve le chemin le plus court.
    }

    void FindPath(Vector3 StartPosition, Vector3 FinalPosition)
    {
        Node StartNode = theGrid.NodeFromWorldPoint(StartPosition);//Trouve le noeud où se trouve la position initale
        Node FinalNode = theGrid.NodeFromWorldPoint(FinalPosition);//Trouve le noeud où se trouve à la position finale. 

        List<Node> OpenList = new List<Node>();//Liste de noeuds pour faire le tour des noeuds.
        HashSet<Node> ClosedList = new HashSet<Node>();//Autre liste de noeuds (avec l'utilisation de HashSet qui est un peu différent de "List" pour conserver les noeuds utilisés dans le chemin.

        OpenList.Add(StartNode);//Ajoute le noeud de départ dans la liste

        while (OpenList.Count > 0)//Tant que la liste n'est pas vide, on fait certaines actions.
        {
            Node CurrentNode = OpenList[0];//Créer un noeud et le mettre comme premier noeud de la liste.
            for (int i = 1; i < OpenList.Count; i++)//Boucle passant au travers les noeuds de la liste. 
            {
                if (OpenList[i].fCost < CurrentNode.fCost || OpenList[i].fCost == CurrentNode.fCost && OpenList[i].hCost < CurrentNode.hCost)//Si le cout total d'un noeud de la liste est plus petit ou égal à celui qui est le noeud retenu présentement. 
                {
                    CurrentNode = OpenList[i];//Mettre ce noeud comme étant le noeud présent.
                }
            }
            OpenList.Remove(CurrentNode);//Enlever ce noeud de la première liste.
            ClosedList.Add(CurrentNode);//Rajouter le noeud dans la liste utilisée pour conserver les noeuds

            if (CurrentNode == FinalNode)//Si le noeud présent est la destination finale.
            {
                GetFinalPath(StartNode, FinalNode);//Trouve le chemin final
            }

            foreach (Node NeighborNode in theGrid.GetNeighboringNodes(CurrentNode))//Boucle qui regarde les noeuds autour du noeud qui est le noeud présent.
            {
                if (!NeighborNode.buildingIsOnNode || ClosedList.Contains(NeighborNode))//Si le noeud est sur un bâtiment ou s'il a déjà été vérifié.
                {
                    continue;
                }
                int MoveCost = CurrentNode.gCost + GetManhattenDistance(CurrentNode, NeighborNode);//Trouver le coût total du noeud

                if (MoveCost < NeighborNode.gCost || !OpenList.Contains(NeighborNode))//Si le cout total est plus petit que le cout pour passer au prochain noeud ou que ce n'est pas dans la liste de noeuds. 
                {
                    NeighborNode.gCost = MoveCost;////Déterminer le coût g du noeud.
                    NeighborNode.hCost = GetManhattenDistance(NeighborNode, FinalNode);//Déterminer le coût h du noeud.
                    NeighborNode.ParentNode = CurrentNode;//Mettre le noeud présent comme étant le parent afin de pouvoir retracer le chemin parcouru.

                    if (!OpenList.Contains(NeighborNode))//Si le noeud n'est pas dans la liste le rajouter
                    {
                        OpenList.Add(NeighborNode);
                    }
                }
            }

        }
    }



    void GetFinalPath(Node firstNode, Node lastNode)
    {
        List<Node> FinalPath = new List<Node>();//Liste avec les noeuds du chemin final
        Node CurrentNode = lastNode;//Mettre le noeud présent comme le dernier noeud.

        while (CurrentNode != firstNode)//Va au travers des parents jusqu'au premier noeud du chemin.
        {
            FinalPath.Add(CurrentNode);//Ajouter le noeud
            CurrentNode = CurrentNode.ParentNode;//Mettre le noeud parent comme étant le noeud présent.
        }

        FinalPath.Reverse();//Inverser le chemin pour avoir le bon ordre

        theGrid.FinalPath = FinalPath;//Mettre le chemin final.

    }

    int GetManhattenDistance(Node firstNode, Node secondNode) //Trouver la distance de Manhatten entre deux noeuds.
    {
        int x = Mathf.Abs(firstNode.positionInX - secondNode.positionInX);//x1-x2
        int y = Mathf.Abs(firstNode.positionInY - secondNode.positionInY);//y1-y2

        return x + y;
    }
}
