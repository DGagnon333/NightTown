using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem
{
    private const string
        BACKPACK_DESCRIPTION = "A backpack will double your player's inventory.",
        HANDTORCH_DESCRIPTION = "A hand torch will lighten your player's surroundings for a certain period of time",
        QUESTOBJECT_DESCRIPTION = "Collect all quest objects to complete the game",
        SPEAR_DESCRIPTION = "Spears are low cost / low damage melee weapons.",
        AXE_DESCRIPTION = "Axes are medium cost / medium damage melee weapons.",
        SWORD_DESCRIPTION = "Swords are high cost / high damage melee weapons.",
        ARROW_DESCRIPTION = "It serves as ammunition for your bow.",
        BOW_DESCRIPTION = "A bow is a long range weapon. It requires arrows";
    public enum PlayerItemType 
    { 
        Backpack,
        HandTorch,
        QuestObject,
        Spear,
        Axe,
        Sword,
        Arrow,
        Bow,
        NbPlayerItemType
    }
    public static string GetDescription(PlayerItemType playerItemType)
    {
        switch (playerItemType)
        {
            default:
            case PlayerItemType.Backpack:    return BACKPACK_DESCRIPTION;
            case PlayerItemType.HandTorch:   return HANDTORCH_DESCRIPTION;
            case PlayerItemType.QuestObject: return QUESTOBJECT_DESCRIPTION;
            case PlayerItemType.Spear:       return SPEAR_DESCRIPTION;
            case PlayerItemType.Axe:         return AXE_DESCRIPTION;
            case PlayerItemType.Sword:       return SWORD_DESCRIPTION;
            case PlayerItemType.Arrow:       return ARROW_DESCRIPTION;
            case PlayerItemType.Bow:         return BOW_DESCRIPTION;
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
