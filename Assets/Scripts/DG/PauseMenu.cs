//Ce code a été fortement inspiré par Brackeys
//sur youtube, PAUSE MENU in Unity, URL: https://www.youtube.com/watch?v=JivuXdrIHK0
//
//modifié par Dérick Gagnon



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject SettingsMenuUI;
    GameObject gridManager;
    GameObject music;

    private void Awake()
    {
        gridManager = GameObject.FindGameObjectWithTag("ConstructionMode");
        music = GameObject.FindGameObjectWithTag("Music");
        music.SetActive(false);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gridManager.GetComponent<BuildingManager>().BuildingMode(false);
            if (GameIsPaused)
            {
                Resume();
                music.SetActive(false);
            }
            else
            {
                Pause();
                music.SetActive(true);
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        music.SetActive(false);

    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
