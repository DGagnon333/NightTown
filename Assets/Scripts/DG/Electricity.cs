using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    public void Awake()
    {
        ElectrictyState();
    }
    public void ElectrictyState()
    {
        int scaleX = (int)(transform.localScale.x); 
        int scaleZ = (int)(transform.localScale.z);
        Debug.Log(scaleX);
        int nb = 0;
        for (int x = scaleX; x <= scaleX*2; x++)
        {
            for (int z = scaleZ; z <= scaleZ*2; z++)
            {
                nb++;
                Debug.Log(nb);
            }
        }
        

    }
}
