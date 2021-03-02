using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField()]
    GameObject Hand; // la main qui tient l'arme
    
    MeleeWeaponComponent weapon; // l'arme

    [SerializeField()]
    Transform weaponEdge; // le bout de l'arme

    void Start()
    {
        weapon = Hand.GetComponentInChildren<MeleeWeaponComponent>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0)) // faire l'attaque lorsque le boutton gauche de la souris est appuyé.
        {
            CommitAttack();
            FakeAnimation();
        }
    }

    public void CommitAttack()
    {
        RaycastHit hit; // garde l'information de l'objet que le raycast touche

        Vector3 direction = Hand.transform.forward; // direction avant de la main du joueur

        Ray ray = new Ray(weaponEdge.position, direction); // le ray qui part du bout de l'arme vers l'avant
        Debug.DrawRay(weaponEdge.position, direction, Color.red); // aide a déboguer

        if(Physics.Raycast(ray, out hit, weapon.MeleeRange)) // si le rayon touche quelque chose
        {
            if(hit.collider.CompareTag("Enemy")) // si l'objet touché est un ennemi
            {
                EnemyHealthComponent enemyHealth = hit.collider.GetComponent<EnemyHealthComponent>();
                enemyHealth.TakeDamage(weapon.MeleeDamage); // L'ennemi perds de la vie egal au dommage de l'arme
            }
        }
    }

    public void FakeAnimation()
    {
        Vector3 pos = Hand.transform.position;
        //Hand.transform.position = Mathf.PingPong()
          
    }
}
