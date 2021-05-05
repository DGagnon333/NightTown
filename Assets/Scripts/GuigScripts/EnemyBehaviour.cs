// Auteur: Guillaume
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    enum EnemyState { Inactive, Active, Attack}
    private GameObject player;
    private GameObject[] buildings;
    [SerializeField] private string playerTag;
    [SerializeField] private string buildingTag;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int moveSpeed;

    private Vector3 targetDirection;
    private EnemyState currentEnemyState;
    private GameObject target;

    private void Awake()
    {
        currentEnemyState = EnemyState.Inactive;
    }
    private void Update()
    {
        switch (currentEnemyState)
        {
            case EnemyState.Inactive: ManageInactiveState(); break;
            case EnemyState.Active: ManageActiveState(); break;
        }
    }
    private void ManageInactiveState()
    {
        target = FindClosestTarget();
        currentEnemyState = EnemyState.Active;
    }
    private void ManageActiveState()
    {
        targetDirection = (target.transform.position-transform.position).normalized;
        rb.AddForce(targetDirection * moveSpeed);
    }

    private GameObject FindClosestTarget()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        Vector3 playerDirection = player.transform.position - transform.position;
        target = player;
        buildings = GameObject.FindGameObjectsWithTag(buildingTag);
        Vector3 closestBuildingDirection = buildings[0].transform.position - transform.position;
        int closestBuildingIndex = 0;
        for(int i = 1; i < buildings.Length; i++)
        {
            Vector3 currentBuildingDirection = buildings[i].transform.position - transform.position;
            if((closestBuildingDirection - currentBuildingDirection).magnitude > Vector3.zero.magnitude)
            {
                closestBuildingDirection = currentBuildingDirection;
                closestBuildingIndex = i;
            }
        }
        if((playerDirection -closestBuildingDirection).magnitude > Vector3.zero.magnitude)
        {
            target = buildings[closestBuildingIndex];
        }
        return target;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(playerTag)|| collision.gameObject.CompareTag(buildingTag))
        {
            Debug.Log("collision with " + collision.gameObject.name);
            target = collision.gameObject;
            currentEnemyState = EnemyState.Attack;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag) || collision.gameObject.CompareTag(buildingTag))
        {
            Debug.Log("ATTACK RANGE");
            rb.velocity = Vector3.zero;
            target.GetComponent<HealthComponent>().TakeDamage(5);
            if (target == null)
                currentEnemyState = EnemyState.Inactive;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(target == null)
            currentEnemyState = EnemyState.Inactive;
        else
        {
            targetDirection = (target.transform.position - transform.position).normalized;
            rb.AddForce(targetDirection * moveSpeed);
        }
    }
}

