using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
