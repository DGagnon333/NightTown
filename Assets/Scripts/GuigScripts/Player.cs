﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fait par Guillaume
// Définit simplement ce que possède un joueur (inventaire, ressource, etc.)

public class Player : MonoBehaviour, IWeaponShopClient, IInventoryUser
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
    public int EnergyCapacity { get; private set; }
    public int EnergyUsage { get; private set; }
    public int ItemAmount { get; private set; }
    public bool OwnsBackpack { get; private set; }
    private void Start()
    {
        OwnsBackpack = false;
        WoodAmount = 100;
        StoneAmount = 100;
        GoldAmount = 100;
        EnergyCapacity = 100;
        EnergyUsage = 0;
        playerInventory = new PlayerInventory();
        uiRessources.RefreshRessources(WoodAmount, StoneAmount, GoldAmount);
        uiPlayerInventory.SetPlayerInventory(playerInventory);
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
    public void EquipWeapon(PlayerItem item)
    {
        Transform playerRightHand = transform.Find("Caracter3_Model_animations Variant").Find("Armature").Find("Spine1").Find("Spine2").Find("Spine3").Find("Shoulder.R").Find("UpperArm.R").Find("LowerArm.R").Find("Hand.R").Find("Hand.R_end").Find("HandHeldObject");
        Debug.Log(playerRightHand);
        if(playerRightHand.Find("weapon") != null)
            Destroy(playerRightHand.Find("weapon").gameObject);
        GameObject weaponEquip = Instantiate(PlayerItem.GetObject(item.Type), playerRightHand, false);
        // new modif
        transform.GetComponent<PlayerMeleeAttack>().weapon = playerRightHand.GetComponentInChildren<MeleeWeaponComponent>();
        //
        weaponEquip.name = "weapon";
    }
    public void UseHandTorch(PlayerItem item)
    {
        // coder ce que la handtorch fait
        Debug.Log("using" + item.Type);

        playerInventory.RemovePlayerItem(item);
        uiPlayerInventory.RefreshInventory();
    }
    public void UseItemInInventory(PlayerItem item, int index)
    {
        if (item.Type != PlayerItem.PlayerItemType.Backpack && item.Type != PlayerItem.PlayerItemType.Arrow)
        {
            if (item.Type == PlayerItem.PlayerItemType.HandTorch)
            {
                UseHandTorch(item);
            }
            else
                EquipWeapon(item);
            // use the specific item
            ///
            ///

            // remove the item from the inventory
            ///
            ///
            //playerInventory.RemovePlayerItem(item);
            //uiPlayerInventory.RefreshInventory();
        }
    }
}
