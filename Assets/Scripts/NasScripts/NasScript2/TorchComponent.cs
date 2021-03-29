using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchComponent : MonoBehaviour
{
    [SerializeField]
    private bool consumable = false;

    [SerializeField]
    private float maxRange = 100f; // portée maximale

    [SerializeField]
    private float minRange = 0.4f; // [0,1] pour le pourcentage d'efficacité selon la portée maximale

    private Light torch;

    private void Awake()
    {
        torch = GetComponentInChildren<Light>();
        torch.range = maxRange;
    }

    private void Start()
    {
        if(consumable)
        {
            InvokeRepeating("reduceLightRange", 0f, 5f);
        }       
    }

    private void reduceLightRange()
    {
        torch.range -= 2;
    }
}
