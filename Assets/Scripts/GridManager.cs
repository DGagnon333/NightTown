using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    GameObject ground;
    Points[,] array;
    [SerializeField] GameObject cube;
    private int step = 2;
    void Awake()
    {
        ground = GameObject.FindGameObjectsWithTag("Ground")[0];
        ArrayCreation();
    }

    void Update()
    {
        
    }

    //private int[] ArrayCreation()
    //{
    //    array = new Points[(int)(ground.transform.localScale.x - 1)*3, (int)(ground.transform.localScale.x - 1)*3];
    //    for(int y =0; y < ground.transform.localScale.x - 1; y++)
    //    {
    //        for (int x = 0; x < ground.transform.localScale.x - 1; x++)
    //        {
    //            array[y*3, x*3] = new Points(x*3, y*3);
    //            Instantiate(cube, new Vector3(x*3, 0, y*3) + transform.position, Quaternion.identity);
    //        }
    //    }
    //    return null;
    //}
    private int[] ArrayCreation()
    {
        array = new Points[(int)(ground.transform.localScale.x -1) * step, (int)(ground.transform.localScale.x -1) * step];
        for (int y = 0; y < ground.transform.localScale.x - 1; y++)
        {
            for (int x = 0; x < ground.transform.localScale.x - 1; x++)
            {
                array[y*step, x*step] = new Points(x*step, y*step);
                Instantiate(cube, new Vector3(x * step, 0, y * step) + transform.position, Quaternion.identity);
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
