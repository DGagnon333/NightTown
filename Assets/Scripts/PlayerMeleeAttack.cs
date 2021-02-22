using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField()]
    private Camera camera;

    [SerializeField()]
    GameObject Hand;

    [SerializeField()]
    MeleeWeaponComponent weapon;

    void Start()
    {
        weapon = Hand.GetComponentInChildren<MeleeWeaponComponent>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            CommitAttack();
        }
    }

    public void CommitAttack()
    {
        
    }
}
