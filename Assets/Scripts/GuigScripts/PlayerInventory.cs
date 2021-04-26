using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInventory /*: IWeaponShopClient*/
{
    const int
        INVENTORY_SIZE = 8,
        INVENTORY_BACKPACK_SIZE = 16;
    private List<PlayerItem> playerItemList;
    private bool ownBackpack = false;

    public PlayerInventory()
    {
        playerItemList = new List<PlayerItem>();
        if (ownBackpack)
            playerItemList.Capacity = INVENTORY_BACKPACK_SIZE;
        else
            playerItemList.Capacity = INVENTORY_SIZE;
    }
    public void HandleNewPlayerItem(PlayerItem item)
    {
        //switch ((int)item.playerItemType)
        //{
        //    default:
        //    case 0: return BACKPACK_DESCRIPTION;
        //    case 1: return HANDTORCH_DESCRIPTION;
        //    case 2: return QUESTOBJECT_DESCRIPTION;
        //    case 3: return SPEAR_DESCRIPTION;
        //    case 4: return AXE_DESCRIPTION;
        //    case 5: return SWORD_DESCRIPTION;
        //    case 6: return ARROW_DESCRIPTION;
        //    case 7: return BOW_DESCRIPTION;
        //}
    }
    public void AddPlayerItem(PlayerItem item)
    {
        //playerItemList.Add(item);
        //foreach(PlayerItem i in playerItemList)
        //{
        //    if(i.playerItemType == item.playerItemType)
        //}

        //if (playerItemList.Contains(item))
        //    playerItemList.
    }
    public void RemovePlayerItem(PlayerItem item)
    {
        playerItemList.Remove(item);
    }
    public void IncreasePlayerInventoryCapacity()
    {
        ownBackpack = true;
        playerItemList.Capacity = INVENTORY_BACKPACK_SIZE;
    }

    public void BoughtItem(PlayerItem.PlayerItemType itemType)
    {
        Debug.Log("Bought item: " + itemType);
    }
}
