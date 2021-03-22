﻿// auteur: Guillaume Varin avec aide de Dérick Gagnon pour la logique du spawn loop

// vidéo temps: 9min
// help: https://youtu.be/q0SBfDFn2Bs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemySpawnScript : MonoBehaviour
{
    public enum WaveState { Inactive, Active, Attack, AllCompleted, NbWaveStates };

    // Guillaume: une mini classe qui définit une vague de zombie et qui est personnalisable à partir de Unity
    [SerializeField]
    public class EnemyWave
    {
        public string name;
        public GameObject enemySpawn;
        public int enemyCount;
        public float spawnRate;
    }
    // Guillaume: Étant donné que l'opération FindGameObjectWithTag() est lourde et qu'on n'a pas besoin de savoir
    //            à chaque frame si la vague est éliminée, on doit établir un interval plus long entre les vérifications.
    [SerializeField]
    private float searchEnemyCountdown = 5f;
    [SerializeField]
    private GameObject DayNightManager;
    [SerializeField]
    private GameObject BuildingManager;

    public EnemyWave[] waves;
    private int nextWave = 0;
    private WaveState currentWaveState = WaveState.Inactive;
    public DayNightCycle dayNightCycle;
    private void Start()
    {
        dayNightCycle = DayNightManager.GetComponentInChildren<DayNightCycle>();
    }
    private void Update()
    {
        bool isDay = dayNightCycle.IsDay;
        if (currentWaveState == WaveState.Attack)
        {
            if (!EnemyIsAlive())
            {
                ManageEndOfWave();
            }
            else { return; } // Guillaume: Si des ennemies sont toujours vivant, on veut éviter de 
                             //            vérifier les conditions pour démarrer une nouvelle vague
        }
        //Guillaume : un test!!! faut enlever!!!!
        if (!isDay)
            Debug.Log("C'est la nuit");
        
        if (!isDay && currentWaveState == WaveState.Inactive)
        {
            // Guillaume: pas certain du input pour l'activation d'une vague ennemi. 
            //            --> hésite entre un bouton (UI) dans la base ou une key sur le clavier
            KeyCode waveActivationKey = KeyCode.V; // Guillaume: Input temporaire pour l'activation d'une vague
            if (!isDay && Input.GetKeyDown(waveActivationKey))
                StartCoroutine(SpawnWave(waves[nextWave]));
        }
    }
    private void ManageEndOfWave()
    {
        Debug.Log("Wave Completed!");

        currentWaveState = WaveState.Inactive;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETED!");
            currentWaveState = WaveState.AllCompleted;
        }
        else
            nextWave++;
    }
    private bool EnemyIsAlive()
    {
        searchEnemyCountdown -= Time.deltaTime;
        if (searchEnemyCountdown <= 0f)
        {
            searchEnemyCountdown = 5f; // Guillaume: je dois trouver une façon de ne pas décider la valeur à 2 lignes différentes
            if (GameObject.FindGameObjectWithTag("Enemy") == null) // Guillaume: s'assurer que les prefabs d'ennemies sont tag: "Enemy"
            {
                return false;
            }
        }
        return true;
    }
    private IEnumerator SpawnWave(EnemyWave currentWave)
    {
        Debug.Log("Spawning Wave: " + currentWave.name);
        currentWaveState = WaveState.Active;

        // Spawning
        for(int i=0; i < currentWave.enemyCount; i++)
        {
            SpawnEnemy(currentWave.enemySpawn);
            yield return new WaitForSeconds(1f / currentWave.spawnRate);
        }

        currentWaveState = WaveState.Attack;

        yield break;
    }
    private void SpawnEnemy(GameObject enemy)
    {
        //Vector3 spawnPoint = DetermineSpawnPosition();
        //Debug.Log("Spawning Enemy: " + enemy.name);
        //Instantiate(enemy, spawnPoint, Quaternion.identity);
    }
    private Vector3 DetermineSpawnPosition(GameObject spawnObject, bool isDay) { return Vector3.zero; }
    private void SpawnLoop(GameObject spawnObject, int spawnTime)
    {
        //if (!waveActivated)
        //{
        //    time -= Time.deltaTime;
        //    if (time <= 0)
        //    {

        //        Instantiate(spawnObject, new Vector3(Random.Range(-range, range) + transform.position.x,
        //            Random.Range(0, 0) + transform.position.y, Random.Range(-range, range) + transform.position.z), Quaternion.identity);
        //        time += spawnTime;
        //    }
        //}
    }
    private void DetermineSpawnRate(bool isDay, bool isWave)
    {
        //int currentSpawnRate;
        //if (isDay && !isWave)
        //    currentSpawnRate = spawnTime *
    }
}