using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Fait par Guillaume
// Ce code permet d'afficher et de cacher l'interface du magasin d'armes lorsque le joueur "rentre" dans le magasin, qu'on définit avec un collider.
public class WeaponStoreTrigger : MonoBehaviour
{
    [SerializeField]
    private UI_WeaponStore uiWeaponStore;
    private void OnTriggerEnter(Collider collider)
    {
        IWeaponShopClient shopClient = collider.GetComponent<IWeaponShopClient>();
        if(shopClient != null)
        {
            uiWeaponStore.ShowClient(shopClient);
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        IWeaponShopClient shopClient = collider.GetComponent<IWeaponShopClient>();
        if (shopClient != null)
        {
            uiWeaponStore.HideClient();
        }
    }
}
