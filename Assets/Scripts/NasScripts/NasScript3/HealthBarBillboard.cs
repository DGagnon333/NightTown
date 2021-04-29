using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ce script est fait par Nassour Nassour
// Inspiré par:
public class HealthBarBillboard : MonoBehaviour
{
    public Transform cam;


    void Update()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
