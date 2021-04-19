using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBigMap : MonoBehaviour
{

    public GameObject map;

    void Start()
    {
        map.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            map.SetActive(true);
        }

        if (Input.GetKey(KeyCode.O))
        {
            map.SetActive(false);
        }
    }
}
