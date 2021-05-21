using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fait par Guillaume
// Ce code nous permet de définir un GameObject qui utilise un inventaire et définit des fonctions nécessaires pour utiliser l'inventaire.

public interface IInventoryUser
{
    void EquipWeapon(PlayerItem item);
    //void UseItem(PlayerItem item);
    void UseHandTorch(PlayerItem item);
    void UseItemInInventory(PlayerItem index, int itemIndex);
}
