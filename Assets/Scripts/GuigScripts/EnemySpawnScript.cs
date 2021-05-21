// auteur: Guillaume Varin avec aide de Dérick Gagnon pour la logique du spawn loop
// help: https://youtu.be/q0SBfDFn2Bs
using System.Collections;
using UnityEngine;
public class EnemySpawnScript : MonoBehaviour
{
    public enum WaveState { Inactive, Active, Attack, AllCompleted, NbWaveStates };

    // Guillaume: une mini classe qui définit une vague de zombie et qui est personnalisable à partir de Unity
    [System.Serializable]
    public class EnemyWave
    {
        public string waveName;
        public GameObject waveEnemySpawn;
        public int waveEnemyCount;
        public float waveSpawnRate;
    }
    // Guillaume: Étant donné que l'opération FindGameObjectWithTag() est lourde et qu'on n'a pas besoin de savoir
    //            à chaque frame si la vague est éliminée, on doit établir un interval plus long entre les vérifications.
    [SerializeField]
    private GameObject enemySpawn;
    [SerializeField]
    private float spawnCountdown = 10f;
    [SerializeField]
    private float searchEnemyCountdown = 5f;
    [SerializeField]
    private DayNightCycle DayNightManager;
    [SerializeField]
    private Transform PlayerTransform;

    public EnemyWave[] waves;
    private int nextWave = 0;
    public WaveState CurrentWaveState { get; private set; }

    public DayNightCycle dayNightCycle;
    private void Start()
    {
        CurrentWaveState = WaveState.Inactive;
        dayNightCycle = DayNightManager.GetComponentInChildren<DayNightCycle>();
    }

    private void Update()
    {
        bool isDay = DayNightManager.IsDay;
        if (CurrentWaveState == WaveState.Attack)
        {
            if (!EnemyIsAlive())
            {
                ManageEndOfWave();
            }
            else { return; } // Guillaume: Si des ennemies sont toujours vivant, on veut éviter de 
                             //            vérifier les conditions pour démarrer une nouvelle vague
        }

        if (isDay)
            SpawnLoop();


        if (!isDay && CurrentWaveState == WaveState.Inactive)
        {
            KeyCode waveActivationKey = KeyCode.V; // Guillaume: Input pour l'activation d'une vague
            if (!isDay && Input.GetKeyDown(waveActivationKey))
            {
                Debug.Log("wave Activated");
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
    }
    private void ManageEndOfWave()
    {
        Debug.Log("Wave Completed!");

        //Ajout de nas:
        dayNightCycle.Pause = false;
        //
        CurrentWaveState = WaveState.Inactive;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVES COMPLETED!");
            CurrentWaveState = WaveState.AllCompleted;
        }
        else
            nextWave++;
    }
    private bool EnemyIsAlive()
    {
        searchEnemyCountdown -= Time.deltaTime;
        if (searchEnemyCountdown <= 0f)
        {
            searchEnemyCountdown = 5f; // Guillaume: Je n'ai pas trouver une façon de ne pas décider la valeur du searchEnemyCountdown à 2 lignes différentes
            if (GameObject.FindGameObjectWithTag("Enemy") == null) // Guillaume: s'assurer que les prefabs d'ennemies sont tag: "Enemy"
            {
                return false;
            }
        }
        return true;
    }
    private IEnumerator SpawnWave(EnemyWave currentWave)
    {
        Debug.Log("Spawning Wave: " + currentWave.waveName);
        CurrentWaveState = WaveState.Active;

        // Spawning
        for(int i=0; i < currentWave.waveEnemyCount; i++)
        {
            SpawnEnemy(currentWave.waveEnemySpawn);
            yield return new WaitForSeconds(5f/currentWave.waveSpawnRate); // Guillaume: nécessite une division pour que plus le rate est haut plus c'est rapide
        }

        CurrentWaveState = WaveState.Attack;

        yield break;
    }
    private void SpawnLoop()
    {
        spawnCountdown -= Time.deltaTime;
        if (spawnCountdown <= 0f)
        {
            SpawnEnemy(enemySpawn);
            spawnCountdown = 10f;
        }
    }
    private void SpawnEnemy(GameObject enemy)
    {
        Vector3 spawnPoint = DetermineSpawnPosition();
        Instantiate(enemy, spawnPoint, Quaternion.identity);
    }
    private Vector3 DetermineSpawnPosition()
    {
        float rangeX = Random.Range(15, 20) * Mathf.Pow(-1f, Random.Range(1, 3));
        float spawnY = 1f;
        float rangeZ = Random.Range(15, 20) * Mathf.Pow(-1f, Random.Range(1, 3));
        return PlayerTransform.position + new Vector3(rangeX, spawnY, rangeZ);
    }
}