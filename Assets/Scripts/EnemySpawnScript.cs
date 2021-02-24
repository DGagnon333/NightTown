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
    private GameObject DayNightManager;

    [SerializeField]
    private GameObject spawnObject;

    [SerializeField]
    private int spawnTime = 3;

    private bool waveActivated { get; set; }
    private int waveNumber { get; set; }
    private bool playerDead { get; set; }

    private DayNightCycle dayNightCycle;

    private void Start()
    {
        waveActivated = false;
        waveNumber = 0;
        DayNightCycle dayNightCycle = DayNightManager.GetComponentInChildren<DayNightCycle>();
    }
    private void Update()
    {
        bool isDay = dayNightCycle.IsDay;
        SpawnLoop(spawnObject, spawnTime);
    }

    private void SpawnLoop(GameObject spawnObject, int spawnTime)
    {
        if (!waveActivated)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {

                //Instantiate(spawnObject, new Vector3(Random.Range(-range, range) + transform.position.x,
                //    Random.Range(0, 0) + transform.position.y, Random.Range(-range, range) + transform.position.z), Quaternion.identity);
                //time += spawnTime;
            }
        }
    }
    private Vector3 DetermineSpawnPosition(GameObject spawnObject, bool isDay) { return Vector3.zero; }
    //private int DetermineSpawnRate(bool isDay, bool isWave)
    //{
    //    int currentSpawnRate;
    //    if (isDay && !isWave)
    //        currentSpawnRate = spawnTime *

    //}
    // Guillaume: pas certain du input pour l'activation d'une vague ennemi. 
    //            --> hésite entre un bouton (UI) dans la base ou une key sur le clavier
    private void WaveActivation(bool isDay)
    {
        KeyCode waveActivationKey = KeyCode.V; // Guillaume: Input temporaire pour l'activation d'une vague
        if (!isDay && Input.GetKeyDown(waveActivationKey))
        {
            waveActivated = true;

        }
    }
}