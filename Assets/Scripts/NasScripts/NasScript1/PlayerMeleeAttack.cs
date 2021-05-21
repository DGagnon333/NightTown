using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ce script est fait par Nassour Nassour.
public class PlayerMeleeAttack : MonoBehaviour // Mettre sur le joueur
{
    [SerializeField()]
    GameObject Hand; // la main qui tient l'arme
    
    public MeleeWeaponComponent weapon; // l'arme

    [SerializeField()]
    Transform weaponEdge; // le bout de l'arme

    private float attackTimer = 0f;

    //void Start()
    //{
    //    weapon = Hand.GetComponentInChildren<MeleeWeaponComponent>();
    //}

    void Update()
    {
        //attackTimer += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.F) /*&& attackTimer*/ /*>= weapon.MeleeCooldown*/ )
            // faire l'attaque lorsque le boutton gauche de la souris est appuyé.
        {
            
            CommitAttack();
            //FakeAnimation();
            attackTimer = 0f;
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
                HealthComponent enemyHealth = hit.collider.GetComponent<HealthComponent>();
                enemyHealth.TakeDamage(weapon.MeleeDamage); // L'ennemi perds de la vie egal au dommage de l'arme
            }
        }
    }

}
