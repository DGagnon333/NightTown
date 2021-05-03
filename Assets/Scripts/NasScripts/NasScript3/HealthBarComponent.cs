using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Ce script est fait par Nassour Nassour
// Inspiré par:https://youtu.be/BLfNP4Sc_iA
public class HealthBarComponent : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;      
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
