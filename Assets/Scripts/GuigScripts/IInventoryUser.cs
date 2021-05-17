using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryUser
{
    void EquipWeapon(PlayerItem item);
    //void UseItem(PlayerItem item);
    void UseHandTorch(PlayerItem item);
    void UseItemInInventory(PlayerItem index, int itemIndex);
}
