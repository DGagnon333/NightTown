using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponComponent : MonoBehaviour
{
    [SerializeField()]
    private float meleeDamage;

    public float MeleeDamage
    {
        get => meleeDamage;
    }


    [SerializeField()]
    private float meleeRange;

    public float MeleeRange
    {
        get => meleeRange;
    }

}
