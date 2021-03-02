using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script est fait par: Nassour Nassour

// isDay reparer


public class DayNightCycle : MonoBehaviour // mettre ce script sur un DayNightManager (gameObject vide)
{
    // Ceci est la partie 1: Création de la notion de temps:

    [SerializeField]
    private float targetDayLength; // Cet attribut permet de choisir la longueure d'une jourée. (En minutes)

    [SerializeField]
    private float timeOfDay; // Cet attribut est une valeure de 0 à 1 qui établit l'heure courante de la journée.
    public float TimeOfDay
    {
        get => timeOfDay;
    }

    public bool pause = false; // Cet attribut permet de pauser le passage du temps. (ne pause pas le jeu)
    public bool Pause
    {
        get => pause;
    }


    private int dayNumber; // Cet attribut permet d'établir le jour courant du joueur.
    public int DayNumber
    {
        get => dayNumber;
    }

    private float timeScale; // Permet de convertir le temps réel en temps virtuel selon la longueure d'une journée.

    private bool isDay = true;
    public bool IsDay
    {
        get => isDay;
    }

    const int SECONDS_IN_DAY = 84000;

    /// <summary>
    /// Cette méthode fait progresser le temps courant. Quand un jour est terminé, le compteur de jour s'incrémente,
    /// Puis le passage du temps se pause (le jeu ne se pause pas) tant que le joueur n'a pas fini sa vague de zombie.
    /// </summary>
    private void UpdateTime()
    {
        timeOfDay += Time.deltaTime * timeScale / SECONDS_IN_DAY; // Passage du temps
        
        if (timeOfDay <= 0.8)
        {
            isDay = true;
        }
        else
        {
            isDay = false; 
        }

        if(timeOfDay > 1)
        {
            dayNumber++;
            timeOfDay -= 1;
            pause = false; // [Note pour Nas]: Quand le testage est fini,
                           // remplace par true pour forcer le joueur a faire sa vague
        }
    }

    private void Awake()
    {
        timeScale = 24 / (targetDayLength / 60); 
    }

    private void Update()
    {
        if(!pause)
        {            
            UpdateTime();
        }
        AdjustSunRotation();
        SunIntensity();
    }

    ////////////////////////////////////////////////////////////////////////
    // Ceci est la partie 2: Intensité et angulation du soleil selon le temps.

    [SerializeField]
    private Transform dailyRotation;

    private void AdjustSunRotation()
    {
        float sunAngle = (timeOfDay * 180f) - 75f;
        dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, sunAngle)); 
        // Applique une rotation sur le soleil
    }

    ////////////////////////////////////////////////////////////////////////
    // Partie 3: Intensité du soleil.

    [SerializeField]
    private Light sun;

    private float intensity;

    private void SunIntensity()
    {
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        Mathf.Clamp01(intensity);

        // Projection orthogonale du vecteur du soleil sur l'axe des y.

        sun.intensity = intensity;
    }
}