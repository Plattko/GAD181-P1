using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Whether the game is playing, only able to be changed in this script
    public bool gamePlaying { get; private set; }
    // Whether the pause menu is active
    private bool gamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject levelWinUI;

    // Reference to current scene
    private Scene currentScene;
    // String to store name of current scene
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        // Get the player's current scene
        currentScene = SceneManager.GetActiveScene();
        // Get the scene name
        sceneName = currentScene.name;

        //gamePlaying = false;
        gamePlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gamePlaying)
        {
            Time.timeScale = 1;
        }
        else if (!gamePlaying)
        {
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
            pauseMenuUI.SetActive(gamePaused);
            gamePlaying = !gamePaused;
        }

        if (levelWinUI.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            // Load the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void BeginGame()
    {
        Debug.Log("Called BeginGame method");
        gamePlaying = true;
    }

    //public void PlayerDeath()
    //{
    //    gamePlaying = false;
    //}

    public void LevelComplete()
    {
        gamePlaying = false;
        levelWinUI.SetActive(true);
    }
}
