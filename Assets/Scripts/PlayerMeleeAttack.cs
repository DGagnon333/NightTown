using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField()]
    private Camera cam;

    [SerializeField()]
    GameObject Hand;

    
    MeleeWeaponComponent weapon;

    [SerializeField()]
    Transform weaponEdge;

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
        RaycastHit hit;

        Vector3 direction = Vector3.forward;

        Ray ray = new Ray(weaponEdge.position, direction);
        Debug.DrawRay(weaponEdge.position, direction);

        if(Physics.Raycast(ray, out hit, weapon.MeleeRange))
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                // Health of enemy reduced (check avec un debuglog)
                Debug.Log("Nas a 200iq");
            }
        }
    }
}
