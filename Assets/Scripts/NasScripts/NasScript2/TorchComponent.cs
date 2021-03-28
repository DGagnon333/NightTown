using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchComponent : MonoBehaviour
{
    private Light torch;

    private void Awake()
    {
        torch = GetComponentInChildren<Light>();
    }

    private void Start()
    {
        
    }
}
