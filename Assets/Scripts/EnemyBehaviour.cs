//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//// Auteur: Guillaume
//public class EnemyBehaviour : MonoBehaviour
//{
//    [SerializeField]
//    const int Movement_Speed = 10; // Guillaume: à ajuster
//    [SerializeField]
//    int health = 100;
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private GameObject[] joueur;
    [SerializeField] private string targetTag;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int moveSpeed = 150;

    private Vector3 dir = new Vector3();
    private void Update()
    {
        joueur = GameObject.FindGameObjectsWithTag(targetTag);
        dir = joueur[0].transform.position - transform.position;
        rb.MovePosition(transform.position + dir.normalized * moveSpeed * Time.deltaTime);
    }
}

