//Fait par Mykaël Arsenault
//Vidéo permettant de comprendre la théorie du changement de scène : https://www.youtube.com/watch?v=jwXcv722bzo 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractWithWeaponStore : MonoBehaviour, IInteractable
{
    public float MaxRange { get { return maxRange; } }
    public GameObject textForRessourcesManager; // Le texte content les instructions pour l'intéraction.
    private float maxRange = 1000000f;
    public string changeScene; //Le nom de la scène que nous voulons changer. 

    public void OnStartHover()
    {
        textForRessourcesManager.SetActive(true); //Affiche les indications pour l'intéraction.
    }
    public void OnInteract()
    {
        SceneManager.LoadScene(changeScene); //Permet de changer de scène.
    }
    public void OnEndHover()
    {
        textForRessourcesManager.SetActive(false);//Enlève les indications pour l'intéraction.
    }
}
