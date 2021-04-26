using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInventory
{
    private const int MAX_DEFAULT_INVENTORY_SIZE = 8,
                      MAX_BACKPACK_INVENTORY_SIZE = 16;
    private List<PlayerItem> playerItemList;
    private bool ownsBackpack;
    public bool OwnsBackpack
    {
        get
        {
            return ownsBackpack;
        }
        private set
        {
            ownsBackpack = value;
        }
    }
    public PlayerInventory()
    {
        playerItemList = new List<PlayerItem>();
        AddPlayerItem(new PlayerItem(PlayerItem.PlayerItemType.Axe, 1));
        AddPlayerItem(new PlayerItem(PlayerItem.PlayerItemType.Sword, 2));
        AddPlayerItem(new PlayerItem(PlayerItem.PlayerItemType.Bow, 1));
        OwnsBackpack = false;
        playerItemList.Capacity = MAX_DEFAULT_INVENTORY_SIZE;
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
        playerItemList.Add(item);
    }
    public void RemovePlayerItem(PlayerItem item)
    {
        playerItemList.Remove(item);
    }
    public void IncreasePlayerInventoryCapacity()
    {
        OwnsBackpack = true;
        playerItemList.Capacity = MAX_BACKPACK_INVENTORY_SIZE;
    }
    public List<PlayerItem> GetPlayerItemList()
    {
        return playerItemList;
    }
}
