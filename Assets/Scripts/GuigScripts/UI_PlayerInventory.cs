using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerInventory : MonoBehaviour
{
    private PlayerInventory inventory;
    private Transform container;
    private Transform inventoryTemplate;

    private void Start()
    {
        container = transform.Find("container");
        inventoryTemplate = container.Find("inventoryTemplate");
        Hide();
    }

    public void SetPlayerInventory(PlayerInventory playerInventory)
    {
        inventory = playerInventory;
        RefreshInventory();
    }
    public void RefreshInventory()
    {
        int i = 0;
        int j = 0;
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
        }
    }
    public void Show()
    {
        transform.Find("container").gameObject.SetActive(true);
        inventoryTemplate.gameObject.SetActive(false);
    }
    public void Hide()
    {
        transform.Find("container").gameObject.SetActive(false);
    }
    //private float SlotDisplaySize(bool ownsBackpack)
    //{
    //    if (ownsBackpack)
    //        return 17.5f;
    //    else
    //        return 35f;
    //}

}    
