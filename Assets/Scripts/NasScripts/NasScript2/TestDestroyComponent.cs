﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDestroyComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(transform.parent.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
