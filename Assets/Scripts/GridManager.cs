using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    Points[,] array;
    public int gridSize =30;
    [SerializeField] public GameObject tiles;
    [SerializeField] public GameObject buildingLayout;
    Vector3 snap;
    private int step = 2;
    private void Awake()
    {
        ArrayCreation();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            GridState(false);
        }
        if (Input.GetKey(KeyCode.E))
        {
            GridState(true);
        }
    }
    private int[] ArrayCreation()
    {
        
        buildingLayout.transform.position = snap;
        array = new Points[gridSize * step, gridSize * step];
        for (int y = 0; y < gridSize - 1; y++)
        {
            for (int x = 0; x < gridSize - 1; x++)
            {
                array[y*step, x*step] = new Points(x*step, y*step);
                Instantiate(tiles, new Vector3(x * step, (float)(-0.08), y * step) + new Vector3 (1,0,1) * (-gridSize), Quaternion.identity);
                //ici on créer des tiles jusqu'à la grandeur de la grille voulue, à chaque bon du step et on diminue le vecteur par la taille 
                //de la grille pour qu'ils soient centré lors de leur création.
            }
        }
        return null;
        
    }
    public void GridState(bool state)
    {
        GameObject[] allTiles = GameObject.FindGameObjectsWithTag("GridPosition");
        foreach (GameObject t in allTiles)
        {
            t.GetComponent<Renderer>().enabled = state;
        }
        buildingLayout.SetActive(state);
    }

}
public class Points
{
    public static int X { get; private set; }
    public static int Y { get; private set; }

    public  Points(int x, int y)
    {
        X = x;
        Y = y;
    }

}
