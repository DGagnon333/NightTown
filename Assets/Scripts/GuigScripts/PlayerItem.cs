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
    public PlayerItemType playerItemType;
    public int count;
    public PlayerItem(int amount, PlayerItemType itemType)
    {
        count = amount;
        playerItemType = itemType;
    }
    public string GetDescription()
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
}
