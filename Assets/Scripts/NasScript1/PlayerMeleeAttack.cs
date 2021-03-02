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

    private float weaponSpeed = 0.01f;

    void Start()
    {
        weapon = Hand.GetComponentInChildren<MeleeWeaponComponent>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            CommitAttack();
            FakeAnimation();
        }
    }

    public void CommitAttack()
    {
        RaycastHit hit;

        Vector3 direction = Hand.transform.forward;

        Ray ray = new Ray(weaponEdge.position, direction);
        Debug.DrawRay(weaponEdge.position, direction, Color.red);

        if(Physics.Raycast(ray, out hit, weapon.MeleeRange))
        {
            if(hit.collider.CompareTag("Enemy"))
            {
                // Health of enemy reduced (check avec un debuglog)
                Debug.Log("Nas a 200iq");
            }
        }
    }

    public void FakeAnimation()
    {
        Vector3 pos = Hand.transform.position;
        Hand.transform.Translate(Hand.transform.forward * Time.deltaTime);
          
    }
}
