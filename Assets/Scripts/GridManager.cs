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
    private void Start()
    {
        Debug.Log(step);

        ArrayCreation();
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
                Instantiate(tiles, new Vector3(x * step, 0, y * step) + Vector3.zero, Quaternion.identity);
                //pour snapper on utilise le tag du cube et on arrondie le transform au transform d'un cube le plus proche avec MathF.Round()
            }
        }
        return null;
        
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
