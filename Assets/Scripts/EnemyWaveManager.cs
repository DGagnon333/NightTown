using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    enum WaveState { Inactive, Activation, Attack, Defeated }

    [SerializeField]
    GameObject Enemy;

    private bool waveActivated;
    private WaveState CurrentState { get; set; }
    private int waveNumber { get; set; }
    private bool playerDead { get; set; }
    void Start()
    {
        CurrentState = WaveState.Inactive;
    }

    void Update()
    {
        
    }
    private void WaveActivation()
    {
        
    }
}
