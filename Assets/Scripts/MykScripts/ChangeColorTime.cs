using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorTime : MonoBehaviour
{
    float theTime = 3.0f;
    Color color1 = Color.green;
    Color color2 = Color.red;

    private Light light;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        // set light color
        float t = Mathf.PingPong(Time.time, theTime) / theTime;
        light.color = Color.Lerp(color1, color2, t);
    }
}