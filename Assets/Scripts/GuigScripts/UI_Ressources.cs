using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Fait par Guillaume
// Ce code gère l'interface qui affiche les ressources du joueur
public class UI_Ressources : MonoBehaviour
{
    private Transform container;
    private Transform ressourceBar;
    private Transform ressourceBarTransform;

    private void Awake()
    {
        container = transform.Find("container");
        ressourceBar = container.Find("ressourceBar");
        ressourceBarTransform = ressourceBar.GetComponent<RectTransform>();
    }
    public void RefreshRessources(int woodAmount, int stoneAmount, int goldAmount)
    {
        ressourceBarTransform.Find("woodAmountText").GetComponent<TextMeshProUGUI>().SetText(woodAmount.ToString());
        ressourceBarTransform.Find("stoneAmountText").GetComponent<TextMeshProUGUI>().SetText(stoneAmount.ToString());
        ressourceBarTransform.Find("goldAmountText").GetComponent<TextMeshProUGUI>().SetText(goldAmount.ToString());
    }
}
