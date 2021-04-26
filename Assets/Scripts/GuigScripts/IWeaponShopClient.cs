using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponShopClient
{
    void BoughtItem(PlayerItem.PlayerItemType itemType);
    bool TryGetNewItem(PlayerItem.PlayerItemType itemType);
    bool TrySpendRessources(int woodCost, int stoneCost, int goldCost);
}
