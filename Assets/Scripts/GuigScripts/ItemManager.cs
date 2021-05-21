using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager _itemManagerInstance;
    public static ItemManager itemManagerInstance
    {
        get
        {
            if (_itemManagerInstance == null)
                _itemManagerInstance = Instantiate(Resources.Load<ItemManager>("ItemManager"));
            return _itemManagerInstance;
        }
    }
    [SerializeField]
    public GameObject backpackObject;
    [SerializeField]
    public GameObject handTorchObject;
    [SerializeField]
    public GameObject questObjectObject;
    [SerializeField]
    public GameObject spearObject;
    [SerializeField]
    public GameObject axeObject;
    [SerializeField]
    public GameObject swordObject;
    [SerializeField]
    public GameObject arrowObject;
    [SerializeField]
    public GameObject bowObject;
}
