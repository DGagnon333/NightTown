using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDamage : MonoBehaviour
{
    private PlayerHealthComponent health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponentInChildren<PlayerHealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
