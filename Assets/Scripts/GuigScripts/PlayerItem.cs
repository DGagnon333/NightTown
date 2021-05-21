using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fait par Guillaume
// Ce code donne accès à tous les informations utiles pour décrire un item (son nom, son type, la quantité, son icone, son cout, etc.)
public class PlayerItem
{
    public const string
        BACKPACK_NAME = "Backpack",
        HANDTORCH_NAME = "Hand torch",
        QUESTOBJECT_NAME = "Quest object",
        SPEAR_NAME = "Spear",
        AXE_NAME = "Axe",
        SWORD_NAME = "Sword",
        ARROW_NAME = "Arrow",
        BOW_NAME = "Bow";
    public enum PlayerItemType 
    { 
        Backpack,
        HandTorch,
        Spear,
        Axe,
        Sword,
        Arrow,
        Bow,
        QuestObject,
        NbPlayerItemType
    }

    public PlayerItemType Type { get; private set; }

    public int Amount { get; private set; }
    public PlayerItem(PlayerItemType type, int amount)
    {
        Type = type;
        Amount = amount;
    }
    public static string GetName(PlayerItemType playerItemType)
    {
        switch (playerItemType)
        {
            default:
            case PlayerItemType.Backpack:    return BACKPACK_NAME;
            case PlayerItemType.HandTorch:   return HANDTORCH_NAME;
            case PlayerItemType.QuestObject: return QUESTOBJECT_NAME;
            case PlayerItemType.Spear:       return SPEAR_NAME;
            case PlayerItemType.Axe:         return AXE_NAME;
            case PlayerItemType.Sword:       return SWORD_NAME;
            case PlayerItemType.Arrow:       return ARROW_NAME;
            case PlayerItemType.Bow:         return BOW_NAME;
        }
    }
    public static Sprite GetSprite(PlayerItemType playerItemType)
    {
        switch (playerItemType)
        {
            default:
            case PlayerItemType.Backpack:    return IconManager.iconManagerInstance.backpackIcon;
            case PlayerItemType.HandTorch:   return IconManager.iconManagerInstance.handTorchIcon;
            case PlayerItemType.QuestObject: return IconManager.iconManagerInstance.questObjectIcon;
            case PlayerItemType.Spear:       return IconManager.iconManagerInstance.spearIcon;
            case PlayerItemType.Axe:         return IconManager.iconManagerInstance.axeIcon;
            case PlayerItemType.Sword:       return IconManager.iconManagerInstance.swordIcon;
            case PlayerItemType.Arrow:       return IconManager.iconManagerInstance.arrowIcon;
            case PlayerItemType.Bow:         return IconManager.iconManagerInstance.bowIcon;
        }
    }
    public static GameObject GetObject(PlayerItemType playerItemType)
    {
        switch (playerItemType)
        {
            default:
            case PlayerItemType.Backpack:    return ItemManager.itemManagerInstance.backpackObject;
            case PlayerItemType.HandTorch:   return ItemManager.itemManagerInstance.handTorchObject;
            case PlayerItemType.QuestObject: return ItemManager.itemManagerInstance.questObjectObject;
            case PlayerItemType.Spear:       return ItemManager.itemManagerInstance.spearObject;
            case PlayerItemType.Axe:         return ItemManager.itemManagerInstance.axeObject;
            case PlayerItemType.Sword:       return ItemManager.itemManagerInstance.swordObject;
            case PlayerItemType.Arrow:       return ItemManager.itemManagerInstance.arrowObject;
            case PlayerItemType.Bow:         return ItemManager.itemManagerInstance.bowObject;
        }
    }
    // Guillaume: Afin de changer le coût d'un item, simplement changer la valeur associée
    //            à la ressource voulue dans les 3 fonctions ci-dessous.
    public static int GetWoodCost(PlayerItemType playerItemType)
    {
        switch (playerItemType)
        {
            default:
            case PlayerItemType.Backpack:    return 0;
            case PlayerItemType.HandTorch:   return 1;
            case PlayerItemType.QuestObject: return 0;
            case PlayerItemType.Spear:       return 4;
            case PlayerItemType.Axe:         return 2;
            case PlayerItemType.Sword:       return 0;
            case PlayerItemType.Arrow:       return 1;
            case PlayerItemType.Bow:         return 4;
        }
    }
    public static int GetStoneCost(PlayerItemType playerItemType)
    {
        switch (playerItemType)
        {
            default:
            case PlayerItemType.Backpack:    return 0;
            case PlayerItemType.HandTorch:   return 0;
            case PlayerItemType.QuestObject: return 0;
            case PlayerItemType.Spear:       return 2;
            case PlayerItemType.Axe:         return 4;
            case PlayerItemType.Sword:       return 4;
            case PlayerItemType.Arrow:       return 1;
            case PlayerItemType.Bow:         return 4;
        }
    }
    public static int GetGoldCost(PlayerItemType playerItemType)
    {
        switch (playerItemType)
        {
            default:
            case PlayerItemType.Backpack:    return 2;
            case PlayerItemType.HandTorch:   return 0;
            case PlayerItemType.QuestObject: return 0;
            case PlayerItemType.Spear:       return 0;
            case PlayerItemType.Axe:         return 0;
            case PlayerItemType.Sword:       return 2;
            case PlayerItemType.Arrow:       return 0;
            case PlayerItemType.Bow:         return 0;
        }
    }
}
