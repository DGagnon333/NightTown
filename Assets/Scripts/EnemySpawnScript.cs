// auteur: Guillaume Varin avec aide de Dérick Gagnon pour la logique du spawn loop
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemySpawnScript : MonoBehaviour
{

    float time = 1;

    private const int DAY_SPAWN_RATE = 1;

    private const int NIGHT_SPAWN_RATE = 2;

    [SerializeField]
    private int spawnTime = 3;

    [SerializeField]
    private GameObject spawnObject;

    private void Start()
    {

    }
    private void Update()
    {
        SpawnLoop(spawnObject, spawnTime);
    }

    private void SpawnLoop(GameObject spawnObject, int spawnTime)
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Instantiate(spawnObject, new Vector3(Random.Range(-range, range) + transform.position.x,
                Random.Range(0, 0) + transform.position.y, Random.Range(-range, range) + transform.position.z), Quaternion.identity);
            time += spawnTime;
        }
    }
    private Vector3 DetermineSpawnPosition(GameObject spawnObject, bool isDay, bool isWave) { return Vector3.zero; }
    private int DetermineSpawnRate(bool isDay, bool isWave)
}