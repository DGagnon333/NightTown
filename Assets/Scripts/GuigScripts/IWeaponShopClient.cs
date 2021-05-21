using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fait par Guillaume
// Cette interface définit quel GameObject est un client du magasin d'armes et définit les fonctions nécessaires pour utiliser le magasin d'armes.
public interface IWeaponShopClient
{
    void BoughtItem(PlayerItem.PlayerItemType itemType);
    bool TryGetNewItem(PlayerItem.PlayerItemType itemType);
    bool TrySpendRessources(int woodCost, int stoneCost, int goldCost);
}
