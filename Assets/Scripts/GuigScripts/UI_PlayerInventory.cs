using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerInventory : MonoBehaviour
{
    private PlayerInventory playerInventory;
    private Transform container;
    private Transform template;
    private Transform border;
    private Transform background;

    private void Awake()
    {
        container = transform.Find("container");
        template = container.Find("template");
        border = transform.Find("border");
        background = transform.Find("background");
        template.gameObject.SetActive(false);
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
        float slotSize = 35f;
        foreach  (PlayerItem item in playerInventory.GetPlayerItemList())
        {
            RectTransform rectTransform = Instantiate(template, container).GetComponent<RectTransform>();
            rectTransform.gameObject.SetActive(true);
            rectTransform.anchoredPosition = new Vector3(i * slotSize, j * slotSize, 0);
            i++;
            if (i > 4)
            {
                i = 0;
                j++;
            }
        }
    }
    //private void RefreshDisplaySize()
    //{
    //    if(playerInventory.GetPlayerItemList().Capacity > 8)
    //    {
    //        border/*.GetComponent<RectTransform>()*/.rect.height *= 2;
    //    }
    //}

}    
