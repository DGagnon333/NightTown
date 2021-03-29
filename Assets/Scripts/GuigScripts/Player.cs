using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] private PlayerMovementScript movementHandler;
    [SerializeField] private PlayerInventory inventory;
    private void Awake()
    {
        inventory = new PlayerInventory();
    }

}
