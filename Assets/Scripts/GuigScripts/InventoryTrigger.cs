using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTrigger : MonoBehaviour
{
    [SerializeField]
    private UI_PlayerInventory uiPlayerInventory;
    [SerializeField]
    private Transform player;

    private IInventoryUser inventoryUser;

    private bool isShowing;

    private void Awake()
    {
        isShowing = false;
        uiPlayerInventory.Hide();
        inventoryUser = player.GetComponent<IInventoryUser>();
    }

    void Update()
    {
        KeyCode showInventoryKey = KeyCode.Mouse1; // Guillaume: Input pour l'affichage de l'inventaire
        if (Input.GetKeyDown(showInventoryKey))
        {
            if (!isShowing)
            {
                uiPlayerInventory.Show(inventoryUser);
                isShowing = true;
            }
            else
            {
                uiPlayerInventory.Hide();
                isShowing = false;
            }
        }
    }
}
