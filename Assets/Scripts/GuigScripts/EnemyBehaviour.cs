// Auteur: Guillaume
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent navigation;

    [SerializeField] private LayerMask knowsPlayer;

    [SerializeField] private LayerMask knowsBuilding;

    private Transform player;
    private Transform closestBuilding;

    private GameObject[] buildings;

    [SerializeField] private string playerTag;
    [SerializeField] private string buildingTag;

    private Vector3 destination;
    private bool destinationSet;
    private bool chasingPlayer;

    [SerializeField] private float sight; // Guillaume: la grandeur de sa distance de vision
    private bool playerInSight;

    [SerializeField] private float attackRange;
    private bool playerInAttackRange;
    private bool buildingInAttackRange;

    private bool attackingBuilding;
    private bool attackReady;
    [SerializeField] private float attackRate;
    [SerializeField] private float attackDamage;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).transform;
        navigation = GetComponent<NavMeshAgent>();
        attackReady = true;
    }
    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sight, knowsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, knowsPlayer);
        buildingInAttackRange = Physics.CheckSphere(transform.position, attackRange, knowsBuilding);

        if (!playerInSight && !playerInAttackRange)
            ManageRoaming();
        else if (playerInSight && !playerInAttackRange)
            ManageChasing();
        else if (playerInSight && playerInAttackRange)
            ManageAttacking();

        transform.LookAt(new Vector3(destination.x,1,destination.z)); //Guillaume: la valeur de y (1) est codé pour empêcher que l'ennemi regarde le ciel
    }
    private void ManageRoaming()
    {
        Debug.Log("roaming");
        navigation.isStopped = false;
        // Guillaume: On regarde si l'ennemi cherche à cause qu'il a perdu de vue le joueur
        if (buildingInAttackRange)
        {
            attackingBuilding = true;
            Debug.Log(closestBuilding);
            ManageAttacking();
        }
        else
        {
            attackingBuilding = false;
            destinationSet = false;
        }
        if (chasingPlayer)
        {
            chasingPlayer = false;
            destinationSet = false;
        }
        if (!destinationSet)
        {
            FindClosestBuildingPosition();
        }
        

    }
    private void ManageChasing()
    {
        navigation.isStopped = false;
        chasingPlayer = true;
        destination = player.position;
        navigation.SetDestination(destination);
    }
    private void ManageAttacking()
    {
        // Guillaume: l'ennemi s'arrête à une distance d'attaque de sa cible pour attaquer
        navigation.isStopped = true;

        if (attackReady)
        {
            /// Coder l'attaque d'un ennemi ici (les visuels dans la scene, TakeDamage, DestroyBuilding ---> destinationSet = false, KillPlayer, etc.)
            /// 
            /// 
            /// 
            /// 
            //destination.parent.GetComponent<HealthComponent>.TakeDamage(attackDamage);
            if (playerInAttackRange)
            {
                player.GetComponent<HealthComponent>().TakeDamage(attackDamage);
                Debug.Log("Attack");
            }
            else if (buildingInAttackRange)
            {
                closestBuilding.GetComponent<HealthComponent>().TakeDamage(attackDamage);
                Debug.Log("Attack");
            }
            attackReady = false;
            Invoke(nameof(RechargeAttack), attackRate);
        }
    }

    // Guillaume: la recharge de l'attaque se fait selon le "attackRate" de l'ennemi
    private void RechargeAttack()
    {
        attackReady = true;
    }

    private void FindClosestBuildingPosition()
    {
        buildings = GameObject.FindGameObjectsWithTag(buildingTag);
        if (buildings.Length >= 1)
        {
            int closestBuildingIndex = 0;
            Vector3 closestBuildingDirection = buildings[closestBuildingIndex].transform.position - transform.position;

            for (int i = 1; i < buildings.Length; i++)
            {
                Vector3 currentBuildingDirection = buildings[i].transform.position - transform.position;
                if (closestBuildingDirection.magnitude > currentBuildingDirection.magnitude)
                {
                    closestBuildingDirection = currentBuildingDirection;
                    closestBuildingIndex = i;
                }
            }
            closestBuilding = buildings[closestBuildingIndex].transform;
            destination = closestBuilding.position;
            navigation.SetDestination(destination);
            destinationSet = true;
        }
        else destinationSet = false; // Guillaume: doit coder ce qui arrive lorsque tous les buildings sont détruits
    }

    // Guillaume: cette fonction permet de visualiser la vision de l'ennemi et le distance d'attaque de l'ennemi dans la scene

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sight);
    }


    /// Version précédente moins efficace avec quelques bogues


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag(playerTag)|| collision.gameObject.CompareTag(buildingTag))
    //    {
    //        Debug.Log("collision with " + collision.gameObject.name);
    //        target = collision.gameObject;
    //        currentEnemyState = EnemyState.Attacking;
    //    }
    //}
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag(playerTag) || collision.gameObject.CompareTag(buildingTag))
    //    {
    //        Debug.Log("ATTACK RANGE");
    //        rb.velocity = Vector3.zero;
    //        target.GetComponent<HealthComponent>().TakeDamage(5);
    //        if (target == null)
    //            currentEnemyState = EnemyState.Searching;
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    if(target == null)
    //        currentEnemyState = EnemyState.Searching;
    //    else
    //    {
    //        targetDirection = (target.transform.position - transform.position).normalized;
    //        rb.AddForce(targetDirection * moveSpeed);
    //    }
    //}
}

