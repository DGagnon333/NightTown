//Ce script a été pris de Brackeys, START MENU in Unity, sur youtube
//https://www.youtube.com/watch?v=zc8ac_qUXQY
//
//modifié par Dérick Gagnon

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Fermeture de la fenêtre");
        Application.Quit();
    }
}
