using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponShopClient
{
    void BoughtItem(PlayerItem.PlayerItemType itemType);
}
