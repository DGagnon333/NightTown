using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Fait par Guillaume
// Ce code gère l'interface de l'inventaire du joueur et toutes ses fonctionalités
public class UI_PlayerInventory : MonoBehaviour
{
    private PlayerInventory inventory;
    private Transform container;
    private Transform inventoryTemplate;
    private IInventoryUser inventoryUser;

    private void Start()
    {
        container = transform.Find("container");
        inventoryTemplate = container.Find("inventoryTemplate");
        Hide();
    }

    public void SetPlayerInventory(PlayerInventory playerInventory)
    {
        inventory = playerInventory;
    }
    public void RefreshInventory()
    {
        Show(inventoryUser);
        Transform[] currentInventoryTemplates = container.GetComponentsInChildren<Transform>(true);
        for(int index = 7; index < currentInventoryTemplates.Length; index++)
        {
            Destroy(currentInventoryTemplates[index].gameObject);
        }
        int i = 0;
        int j = 0;
        int itemIndex = 0;
        float slotSize = 90f;
        foreach  (PlayerItem item in inventory.GetPlayerItemList())
        {
            Transform itemTransform = Instantiate(inventoryTemplate, container);
            itemTransform.gameObject.SetActive(true);
            RectTransform rectTransform = itemTransform.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(-140 + i * slotSize,140 - j * slotSize, 0);
            itemTransform.Find("itemIcon").GetComponent<Image>().sprite = PlayerItem.GetSprite(item.Type);
            i++;
            if (i >= 4)
            {
                i = 0;
                j++;
            }
            itemTransform.GetComponent<Button>().onClick.AddListener(delegate { UseItem(item, itemIndex); });
            itemIndex++;
        }
    }
    private void UseItem(PlayerItem item, int itemIndex)
    {
        //switch (item.Type)
        //{
        //    default:
        //    case PlayerItem.PlayerItemType.Arrow: break;
        //    case PlayerItem.PlayerItemType.QuestObject: break;
        //    case PlayerItem.PlayerItemType.Backpack: break;
        //    case PlayerItem.PlayerItemType.Axe: inventoryUser.EquipWeapon(item); break;
        //    case PlayerItem.PlayerItemType.Spear: inventoryUser.EquipWeapon(item); break;
        //    case PlayerItem.PlayerItemType.Bow: inventoryUser.EquipWeapon(item); break;
        //    case PlayerItem.PlayerItemType.Sword: inventoryUser.EquipWeapon(item); break;
        //    case PlayerItem.PlayerItemType.HandTorch: inventoryUser.UseHandTorch(item); break;
        //}
        inventoryUser.UseItemInInventory(item, itemIndex);
    }
    public void Show(IInventoryUser inventoryUser)
    {
        this.inventoryUser = inventoryUser;
        transform.Find("container").gameObject.SetActive(true);
        inventoryTemplate.gameObject.SetActive(false);
    }
    public void Hide()
    {
        transform.Find("container").gameObject.SetActive(false);
    }


}    
