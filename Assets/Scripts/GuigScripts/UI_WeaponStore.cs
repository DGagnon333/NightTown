using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_WeaponStore : MonoBehaviour
{
    private Transform container;
    private Transform itemTemplate;

    private void Awake()
    {
        container = transform.Find("container");
        itemTemplate = container.Find("itemTemplate");
        itemTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        Sprite test = PlayerItem.GetSprite(PlayerItem.PlayerItemType.Backpack);
        CreateItemButton(PlayerItem.GetSprite(PlayerItem.PlayerItemType.Backpack), "Backpack", PlayerItem.GetWoodCost(PlayerItem.PlayerItemType.Backpack), PlayerItem.GetStoneCost(PlayerItem.PlayerItemType.Backpack), PlayerItem.GetGoldCost(PlayerItem.PlayerItemType.Backpack), 0);

        CreateItemButton(PlayerItem.GetSprite(PlayerItem.PlayerItemType.HandTorch), "Hand Torch", PlayerItem.GetWoodCost(PlayerItem.PlayerItemType.HandTorch), PlayerItem.GetStoneCost(PlayerItem.PlayerItemType.HandTorch), PlayerItem.GetGoldCost(PlayerItem.PlayerItemType.HandTorch), 1);

        CreateItemButton(PlayerItem.GetSprite(PlayerItem.PlayerItemType.Spear), "Spear", PlayerItem.GetWoodCost(PlayerItem.PlayerItemType.Spear), PlayerItem.GetStoneCost(PlayerItem.PlayerItemType.Spear), PlayerItem.GetGoldCost(PlayerItem.PlayerItemType.Spear), 2);

        CreateItemButton(PlayerItem.GetSprite(PlayerItem.PlayerItemType.Axe), "Axe", PlayerItem.GetWoodCost(PlayerItem.PlayerItemType.Axe), PlayerItem.GetStoneCost(PlayerItem.PlayerItemType.Axe), PlayerItem.GetGoldCost(PlayerItem.PlayerItemType.Axe), 3);

        CreateItemButton(PlayerItem.GetSprite(PlayerItem.PlayerItemType.Sword), "Sword", PlayerItem.GetWoodCost(PlayerItem.PlayerItemType.Sword), PlayerItem.GetStoneCost(PlayerItem.PlayerItemType.Sword), PlayerItem.GetGoldCost(PlayerItem.PlayerItemType.Sword), 4);

        CreateItemButton(PlayerItem.GetSprite(PlayerItem.PlayerItemType.Arrow), "Arrow", PlayerItem.GetWoodCost(PlayerItem.PlayerItemType.Arrow), PlayerItem.GetStoneCost(PlayerItem.PlayerItemType.Arrow), PlayerItem.GetGoldCost(PlayerItem.PlayerItemType.Arrow), 5);

        CreateItemButton(PlayerItem.GetSprite(PlayerItem.PlayerItemType.Bow), "Bow", PlayerItem.GetWoodCost(PlayerItem.PlayerItemType.Bow), PlayerItem.GetStoneCost(PlayerItem.PlayerItemType.Bow), PlayerItem.GetGoldCost(PlayerItem.PlayerItemType.Bow), 6);
    }

    private void CreateItemButton(Sprite itemIcon, string itemName, int woodCost, int stoneCost, int goldCost, int orderIndex)
    {
        Transform itemTransform = Instantiate(itemTemplate, container);
        itemTransform.gameObject.SetActive(true);
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
        float itemHeight = 35f;
        float startingHeight = 105f;
        itemRectTransform.anchoredPosition = new Vector3(80, startingHeight - itemHeight * orderIndex, 0);

        itemTransform.Find("itemNameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        itemTransform.Find("itemWoodCostText").GetComponent<TextMeshProUGUI>().SetText(woodCost.ToString());
        itemTransform.Find("itemStoneCostText").GetComponent<TextMeshProUGUI>().SetText(stoneCost.ToString());
        itemTransform.Find("itemGoldCostText").GetComponent<TextMeshProUGUI>().SetText(goldCost.ToString());

        itemTransform.Find("itemIcon").GetComponent<Image>().sprite = itemIcon;
    }

}
