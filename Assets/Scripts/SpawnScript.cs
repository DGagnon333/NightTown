using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SpawnScript : MonoBehaviour
{

    float time = 1;

    [SerializeField] int spawnTime = 3;

    [SerializeField] private GameObject ennemie;

    [SerializeField] private int range;
    private void Update()
    {
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Instantiate(ennemie, new Vector3(Random.Range(-range, range) + transform.position.x,
                Random.Range(0, 0) + transform.position.y, Random.Range(-range, range) + transform.position.z), Quaternion.identity);
            time += spawnTime;
        }

    }
}