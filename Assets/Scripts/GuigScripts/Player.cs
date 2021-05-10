using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IWeaponShopClient
{
    [SerializeField]
    private UI_PlayerInventory uiPlayerInventory;
    [SerializeField]
    private UI_Ressources uiRessources;

    private PlayerInventory playerInventory;

    private const int MAX_DEFAULT_INVENTORY_SIZE = 8,
                      MAX_BACKPACK_INVENTORY_SIZE = 16;

    public int WoodAmount { get; private set; }
    public int StoneAmount { get; private set; }
    public int GoldAmount { get; private set; }
    public int ItemAmount { get; private set; }
    public bool OwnsBackpack { get; private set; }
    private void Start()
    {
        OwnsBackpack = false;
        WoodAmount = 100;
        StoneAmount = 100;
        GoldAmount = 100;
        playerInventory = new PlayerInventory();
        uiPlayerInventory.SetPlayerInventory(playerInventory);
        uiRessources.RefreshRessources(WoodAmount, StoneAmount, GoldAmount);
    }
    public void EquipBackpack()
    {
        OwnsBackpack = true;
        playerInventory.AddPlayerItem(new PlayerItem(PlayerItem.PlayerItemType.Backpack, 1));
        uiPlayerInventory.RefreshInventory();
    }
    public void AddWoodAmount(int addWoodAmount)
    {
        WoodAmount += addWoodAmount;
        uiRessources.RefreshRessources(WoodAmount, StoneAmount, GoldAmount);
    }
    public void AddStoneAmount(int addStoneAmount)
    {
        StoneAmount += addStoneAmount;
        uiRessources.RefreshRessources(WoodAmount, StoneAmount, GoldAmount);
    }
    public void AddGoldAmount(int addGoldAmount)
    {
        GoldAmount += addGoldAmount;
        uiRessources.RefreshRessources(WoodAmount, StoneAmount, GoldAmount);
    }
    public void BoughtItem(PlayerItem.PlayerItemType itemType)
    {
        if (itemType == PlayerItem.PlayerItemType.Backpack)
            EquipBackpack();
        else
            playerInventory.AddPlayerItem(new PlayerItem(itemType, 1));
        uiPlayerInventory.RefreshInventory();
    }

    public bool TrySpendRessources(int woodCost, int stoneCost, int goldCost)
    {
        if (WoodAmount >= woodCost && StoneAmount >= stoneCost && GoldAmount >= goldCost)
        {
            SpendRessources(woodCost, stoneCost, goldCost);
            return true;
        }
        else
            return false;
    }
    public void SpendRessources(int woodCost, int stoneCost, int goldCost)
    {
        WoodAmount -= woodCost;
        StoneAmount -= stoneCost;
        GoldAmount -= goldCost;
        uiRessources.RefreshRessources(WoodAmount, StoneAmount, GoldAmount);
    }

    public bool TryGetNewItem(PlayerItem.PlayerItemType itemType)
    {
        bool canGetNewItem;
        if (itemType == 0 && OwnsBackpack)
            canGetNewItem = false;
        else if (playerInventory.GetPlayerItemList().Count >= playerInventory.GetPlayerItemList().Capacity)
            canGetNewItem = false;
        else
            canGetNewItem = true;
        return canGetNewItem;
    }
}
