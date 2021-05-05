using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerInventory : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private Transform inventoryContainer;
    private Transform inventoryTemplate;

    private void Start()
    {
        inventoryContainer = transform.Find("inventoryContainer");
        inventoryTemplate = inventoryContainer.Find("inventoryTemplate");
        Hide();
    }

    public void SetPlayerInventory(PlayerInventory playerInventory)
    {
        this.playerInventory = playerInventory;
        RefreshInventory();
    }
    private void RefreshInventory()
    {
        int i = 0;
        int j = 0;
        float slotSize = 90f;
        foreach  (PlayerItem item in playerInventory.GetPlayerItemList())
        {
            Transform itemTransform = Instantiate(inventoryTemplate, inventoryContainer);
            itemTransform.gameObject.SetActive(true);
            RectTransform rectTransform = itemTransform.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(i * slotSize, j * slotSize, 0);
            Image image = rectTransform.Find("itemIcon").GetComponent<Image>();
            image.sprite = PlayerItem.GetSprite(item.Type);

            i++;
            if (i > 4)
            {
                i = 0;
                j++;
            }
        }
    }
    public void Show()
    {
        transform.Find("Inventory").gameObject.SetActive(true);
    }
    public void Hide()
    {
        transform.Find("Inventory").gameObject.SetActive(false);
    }
    //private float SlotDisplaySize(bool ownsBackpack)
    //{
    //    if (ownsBackpack)
    //        return 17.5f;
    //    else
    //        return 35f;
    //}

}    
