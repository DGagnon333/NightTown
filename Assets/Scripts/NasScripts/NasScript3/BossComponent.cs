using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Assisté par derick gagnon
[RequireComponent(typeof(Rigidbody))]
public class BossComponent : MonoBehaviour
{
    [Header("Settings")]
    public Transform playerPosition; // position du joueur
    public Material normalMat; // Materiel du boss
    public Material vulnerableMat; // Mat du boss quand il est vulnérable

    [Header("Values")]
    public float speed = 15f; // vitesse du boss

    private Animator animator;
    private BossHealthComponent health;
    private SkinnedMeshRenderer mesh;

    
    
    // Deux compteurs
    private float elapsedTime = 0f; 
    private float idleTimer = 0f;


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

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<BossHealthComponent>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        direction = transform.position;
    } 

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        idleTimer += Time.deltaTime;

        transform.LookAt(playerPosition);
        if (elapsedTime >= 5)
        {
            direction = playerPosition.position;
            animator.SetTrigger("run");
            mesh.material = normalMat;
            Vulnerable = false;
            elapsedTime -= 5;
        }
        else if (idleTimer >= 0.2f )
        {          
            if ((transform.position - direction) == Vector3.zero)
            {
                animator.ResetTrigger("run");
                animator.SetTrigger("idle");
                mesh.material = vulnerableMat;
                Vulnerable = true;

                transform.LookAt(transform.forward);
            }
            idleTimer -= 0.2f;         
        }
        transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
        //Debug.Log(transform.position - direction);
    }
}
