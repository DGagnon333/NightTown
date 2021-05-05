using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IWeaponShopClient
{
    [SerializeField]
    private UI_PlayerInventory uiPlayerInventory;

    private PlayerInventory playerInventory;

    private const int MAX_DEFAULT_INVENTORY_SIZE = 8,
                      MAX_BACKPACK_INVENTORY_SIZE = 16;
    public event EventHandler OnWoodAmountChanged;
    public event EventHandler OnStoneAmountChanged;
    public event EventHandler OnGoldAmountChanged;
    //[SerializeField] private PlayerMovementScript movementHandler;
    //[SerializeField] private PlayerInventory inventory;
    private int woodAmount;
    public int WoodAmount
    {
        get
        {
            return woodAmount;
        }
        private set 
        {
            woodAmount = value;
        }
    }
    private int stoneAmount;
    public int StoneAmount
    {
        get
        {
            return stoneAmount;
        }
        private set
        {
            stoneAmount = value;
        }
    }
    private int goldAmount;
    public int GoldAmount
    {
        get
        {
            return goldAmount;
        }
        private set
        {
            goldAmount = value;
        }
    }
    private int itemAmount;
    public int ItemAmount
    {
        get
        {
            return itemAmount;
        }
        private set
        {
            itemAmount = value;
        }
    }
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
    private void Awake()
    {
        OwnsBackpack = false;
        WoodAmount = 0;
        StoneAmount = 0;
        GoldAmount = 0;

        playerInventory = new PlayerInventory();
        uiPlayerInventory.SetPlayerInventory(playerInventory);
        uiPlayerInventory.Hide();
    }
    public void EquipBackpack()
    {
        OwnsBackpack = true;
    }
    public void AddWoodAmount(int addWoodAmount)
    {
        WoodAmount += addWoodAmount;
    }
    public void AddStoneAmount(int addStoneAmount)
    {
        StoneAmount += addStoneAmount;
    }
    public void AddGoldAmount(int addGoldAmount)
    {
        GoldAmount += addGoldAmount;
    }
    public void BoughtItem(PlayerItem.PlayerItemType itemType)
    {
        Debug.Log("Bought item: " + itemType);
        switch (itemType)
        {
            case PlayerItem.PlayerItemType.Backpack: EquipBackpack(); break;
                //case PlayerItem.PlayerItemType.HandTorch: return HANDTORCH_NAME;
                //case PlayerItem.PlayerItemType.Spear: return SPEAR_NAME;
                //case PlayerItem.PlayerItemType.Axe: return AXE_NAME;
                //case PlayerItem.PlayerItemType.Sword: return SWORD_NAME;
                //case PlayerItem.PlayerItemType.Arrow: return ARROW_NAME;
                //case PlayerItem.PlayerItemType.Bow: return BOW_NAME;
        }
    }

    public bool TrySpendRessources(int woodCost, int stoneCost, int goldCost)
    {
        if (woodAmount >= woodCost && stoneAmount >= stoneCost && goldAmount >= goldCost)
        {
            WoodAmount -= woodCost;
            StoneAmount -= stoneCost;
            GoldAmount -= goldCost;
            OnWoodAmountChanged?.Invoke(this, EventArgs.Empty);
            OnStoneAmountChanged?.Invoke(this, EventArgs.Empty);
            OnGoldAmountChanged?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
            return false;
    }

    public bool TryGetNewItem(PlayerItem.PlayerItemType itemType)
    {
        bool canGetNewItem;
        if (itemType == PlayerItem.PlayerItemType.Backpack && !OwnsBackpack)
            canGetNewItem = true;
        else if (OwnsBackpack && ItemAmount < MAX_BACKPACK_INVENTORY_SIZE)
            canGetNewItem = true;
        else if (!OwnsBackpack && ItemAmount < MAX_DEFAULT_INVENTORY_SIZE)
            canGetNewItem = true;
        else
            canGetNewItem = false;
        return canGetNewItem;
    }
}
