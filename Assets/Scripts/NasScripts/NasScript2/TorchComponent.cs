using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ce script est écrit par Nassour Nassour
public class TorchComponent : MonoBehaviour
{
    [SerializeField]
    private bool consumable = false;

    [SerializeField]
    private float maxRange = 100f; // portée maximale

    [SerializeField]
    private float minRange = 0.4f; // [0,1] pour le pourcentage d'efficacité selon la portée maximale

    [SerializeField]
    DayNightCycle dayNight;

    private Light torch;

    private void Awake()
    {
        torch = GetComponentInChildren<Light>();
        torch.range = maxRange;
    }

    private void Start()
    {
        if(consumable) // Pour les torches à main
        {
            InvokeRepeating("ReduceLightRange", 0f, 5f);
        }
        else
        {
            InvokeRepeating("LightOnNight", 0f, 5f);
        }  
    }

    private void ReduceLightRange()
    {
        if(torch.range > maxRange * (1-minRange))
        {
            torch.range -= 2;
        } 
    }

    private void LightOnNight()
    {
        if(dayNight.IsDay)
        {
            torch.enabled = false;
            Debug.Log("lights off");
        }
        else
        {
            torch.enabled = true;
            Debug.Log("lights on");
        }
    }
}
