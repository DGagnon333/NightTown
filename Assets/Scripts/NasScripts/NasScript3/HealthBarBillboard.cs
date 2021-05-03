using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ce script est fait par Nassour Nassour
// Inspiré par: https://youtu.be/BLfNP4Sc_iA
public class HealthBarBillboard : MonoBehaviour
{
    public Transform cam;


    void Update()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
