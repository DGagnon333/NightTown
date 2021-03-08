using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script est fait par Nassour Nassour.

public class MeleeWeaponComponent : MonoBehaviour // Mettre ce script sur l'arme
{
    [SerializeField()]
    private float meleeDamage;

    public float MeleeDamage // Dommages de l'arme
    {
        get => meleeDamage;
    }


    [SerializeField()]
    private float meleeRange;

    public float MeleeRange // Portée de l'arme
    {
        get => meleeRange;
    }

    [SerializeField()]
    private float meleeCooldown;
    public float MeleeCooldown // Portée de l'arme
    {
        get => meleeCooldown;
    }
}
