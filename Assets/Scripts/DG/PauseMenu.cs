using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
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

    private void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
