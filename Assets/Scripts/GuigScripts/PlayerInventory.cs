using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInventory
{
    private const int MAX_DEFAULT_INVENTORY_SIZE = 8,
                      MAX_BACKPACK_INVENTORY_SIZE = 16;
    private List<PlayerItem> playerItemList;

    public bool OwnsBackpack { get; private set; }
    public PlayerInventory()
    {
        playerItemList = new List<PlayerItem>();
        OwnsBackpack = false;
        playerItemList.Capacity = MAX_DEFAULT_INVENTORY_SIZE;
    }
    public void AddPlayerItem(PlayerItem item)
    {
        if(item.Type == 0)
        {
            OwnsBackpack = true;
            playerItemList.Capacity = MAX_BACKPACK_INVENTORY_SIZE;
        }
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
