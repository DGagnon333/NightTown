using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ce script est fait par Nassour Nassour
// Ce script gère le comportement du boss.
// Etat 1: Le boss est invulnerable et fonce vers le joueur.
// Etat 2: Le boss ne bouge pas et est vulnérable.
// Assisté par derick gagnon
[RequireComponent(typeof(Rigidbody))]
public class BossComponent : MonoBehaviour
{
    [Header("Settings")]
    public Transform playerPosition; // position du joueur
    public Material normalMat; // Materiel du boss
    public Material vulnerableMat; // Mat du boss quand il est vulnérable
    public GameObject zombie; // El zombie a spawner

    [Header("Values")]
    public float speed = 15f; // vitesse du boss

    private Animator animator;
    private BossHealthComponent health;
    private SkinnedMeshRenderer mesh;  
    
    // Deux compteurs
    private float elapsedTime = 0f; 
    private float idleTimer = 0f;
    private float zombieTimer = 0f;


    // Détermine si le boss est vulnérable
    private bool _vulnerable = true;
    public bool Vulnerable
    {
        get
        {
            return _vulnerable;
        }

        set
        {
            _vulnerable = value;
        }
    }

    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<BossHealthComponent>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        playerPos = transform.position;
    } 

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        idleTimer += Time.deltaTime;
        zombieTimer += Time.deltaTime;

        transform.LookAt(playerPosition); // Le boss regarde le joueur
        if (elapsedTime >= 5)
        {
            playerPos = playerPosition.position;
            animator.SetTrigger("run"); // animation pour courir
            mesh.material = normalMat; 
            Vulnerable = false; // est invulnérable lorsqu'il cours
            elapsedTime -= 5;
        }
        else if (idleTimer >= 0.2f )
        {          
            if ((transform.position - playerPos) == Vector3.zero) // Vérifie si le boss est immobile.
            {
                animator.ResetTrigger("run");
                animator.SetTrigger("idle");
                mesh.material = vulnerableMat;
                Vulnerable = true;
            }
            idleTimer -= 0.2f;         
        }
        transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime); // le boss cours vers le joueur
        //Debug.Log(transform.position - direction);

        if (zombieTimer >= 15) 
        {
            Debug.Log("Spawn un zombie"); // J'attends que guillaume finisse ses zombies
            zombieTimer -= 15;
        }
    }
}
