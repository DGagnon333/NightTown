using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTrigger : MonoBehaviour
{
    [SerializeField]
    private UI_PlayerInventory uiPlayerInventory;

    private bool isShowing;

    private void Awake()
    {
        isShowing = false;
        uiPlayerInventory.Hide();
    }

    void Update()
    {
        KeyCode showInventoryKey = KeyCode.Mouse1; // Guillaume: Input pour l'affichage de l'inventaire
        if (Input.GetKeyDown(showInventoryKey))
        {
            if (!isShowing)
            {
                uiPlayerInventory.Show();
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
