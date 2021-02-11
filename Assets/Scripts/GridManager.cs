using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    Points[,] array;
    private int gridSizeX;
    [SerializeField] GameObject cube;
    private int stepX;
    private void Awake()
    {
        cube.tag = "GridPosition";
        
    }
    private void Start()
    {

        //applique le tag GridPosition à tous les cubes pour pouvoir avoir les accéder plus tard
        stepX = (int)FindObjectOfType<SnapToGrid>().step.x;
        Debug.Log(stepX);
        gridSizeX = (int)FindObjectOfType<SnapToGrid>().gridSize.x;
        Debug.Log(gridSizeX);

        ArrayCreation();
    }
    private int[] ArrayCreation()
    {
        array = new Points[gridSizeX * stepX, gridSizeX * stepX];
        for (int y = 0; y < gridSizeX - 1; y++)
        {
            for (int x = 0; x < gridSizeX - 1; x++)
            {
                array[y*stepX, x*stepX] = new Points(x*stepX, y*stepX);
                Instantiate(cube, new Vector3(x * stepX, 0, y * stepX) + transform.position, Quaternion.identity);
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
