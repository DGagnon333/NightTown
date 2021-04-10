using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WeaponStore : MonoBehaviour
{
    private Transform container;
    private Transform itemTemplate;

    private void Awake()
    {
        container = transform.Find("container");
        itemTemplate = transform.Find("itemTemplate");
        itemTemplate.gameObject.SetActive(false);
    }

}
