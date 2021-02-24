//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyWaveManager : MonoBehaviour
//{
//    enum WaveState { Inactive, Activation, NbWaveStates }

//    [SerializeField]
//    GameObject Enemy;
//    [SerializeField]
//    GameObject DayNightManager;
//    private bool waveActivated { get; set; }
//    private int waveNumber { get; set; }
//    private bool playerDead { get; set; }

//    private DayNightCycle dayNightCycle;
//    private WaveState CurrentState { get; set; }
    
//    void Start()
//    {
//        CurrentState = WaveState.Inactive;
//        DayNightCycle dayNightCycle = DayNightManager.GetComponentInChildren<DayNightCycle>();
//    }

//    void Update()
//    {
//        bool isDay = dayNightCycle.IsDay;
//    }
//    // Guillaume: pas certain du input pour l'activation d'une vague ennemi. 
//    //            --> hésite entre un bouton (UI) dans la base ou une key sur le clavier
//    private void WaveActivation(bool isDay)
//    {
//        KeyCode waveActivationKey = KeyCode.V; // Guillaume: Input temporaire pour l'activation d'une vague
//        if (!isDay && Input.GetKeyDown(waveActivationKey))
//        {
//            waveActivated = true;

//        }
//    }
//}
