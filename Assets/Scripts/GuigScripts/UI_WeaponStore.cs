using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_WeaponStore : MonoBehaviour
{
    private Transform container;
    private Transform itemTemplate;
    private IWeaponShopClient weaponShopClient;

    private void Awake()
    {
        container = transform.Find("container");
        itemTemplate = container.Find("itemTemplate");
        itemTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        for(int i = 0; i < (int)PlayerItem.PlayerItemType.NbPlayerItemType - 1; i++)
        {
            CreateItemButton((PlayerItem.PlayerItemType)i, PlayerItem.GetSprite((PlayerItem.PlayerItemType)i), PlayerItem.GetName((PlayerItem.PlayerItemType)i), PlayerItem.GetWoodCost((PlayerItem.PlayerItemType)i), PlayerItem.GetStoneCost((PlayerItem.PlayerItemType)i), PlayerItem.GetGoldCost((PlayerItem.PlayerItemType)i), i);
        }
        HideClient();
    }

    private void CreateItemButton(PlayerItem.PlayerItemType itemType, Sprite itemIcon, string itemName, int woodCost, int stoneCost, int goldCost, int orderIndex)
    {
        Transform itemTransform = Instantiate(itemTemplate, container);
        itemTransform.gameObject.SetActive(true);
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
        float itemHeight = 60f;
        itemRectTransform.anchoredPosition = new Vector3(container.position.x, container.position.y - itemHeight * orderIndex, 0);

        itemTransform.Find("itemNameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        itemTransform.Find("itemWoodCostText").GetComponent<TextMeshProUGUI>().SetText(woodCost.ToString());
        itemTransform.Find("itemStoneCostText").GetComponent<TextMeshProUGUI>().SetText(stoneCost.ToString());
        itemTransform.Find("itemGoldCostText").GetComponent<TextMeshProUGUI>().SetText(goldCost.ToString());

        itemTransform.Find("itemIcon").GetComponent<Image>().sprite = itemIcon;

        itemTransform.GetComponent<Button>().onClick.AddListener(delegate { TryBuyItem(itemType); });
    }
    private void TryBuyItem(PlayerItem.PlayerItemType playerItemType)
    {
        if (weaponShopClient.TryGetNewItem(playerItemType))
        {
            if (weaponShopClient.TrySpendRessources(PlayerItem.GetWoodCost(playerItemType), PlayerItem.GetStoneCost(playerItemType), PlayerItem.GetGoldCost(playerItemType)))
            {
                weaponShopClient.BoughtItem(playerItemType);
            }
        }
    }

    public void ShowClient(IWeaponShopClient weaponShopClient)
    {
        this.weaponShopClient = weaponShopClient;
        gameObject.SetActive(true);
    }
    public void HideClient()
    {
        gameObject.SetActive(false);
    }

}
