using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Whether the game is playing, only able to be changed in this script
    public bool gamePlaying { get; private set; }
    // Whether the game can be paused
    public bool gameIsPausable = false;
    // Whether the pause menu is active
    private bool gamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject levelWinUI;

    // Reference to current scene
    private Scene currentScene;
    // String to store name of current scene
    private string sceneName;

    //[SerializeField] private List<GameObject> uiMenus = new List<GameObject>();
    //private GameObject lastMenu;

    // Start is called before the first frame update
    void Awake()
    {
        // Get the player's current scene
        currentScene = SceneManager.GetActiveScene();
        // Get the scene name
        sceneName = currentScene.name;

        gamePlaying = false;
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

        //if (Input.GetKeyDown(KeyCode.Escape) && !pauseMenuUI.gameObject.activeInHierarchy)
        //{
        //    for (int i = 0; i < uiMenus.Count; i++)
        //    {
        //        uiMenus[i].gameObject.SetActive(false);
        //    }

        //    pauseMenuUI.SetActive(true);
        //}
        //else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenuUI.gameObject.activeInHierarchy)
        //{
        //    pauseMenuUI.SetActive(false);
        //    lastMenu.SetActive(true);
        //}

        if (Input.GetKeyDown(KeyCode.Tab) && gameIsPausable)
        {
            PauseMenu();
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
        gameIsPausable = true;
    }

    public void PauseMenu()
    {
        gamePaused = !gamePaused;
        pauseMenuUI.SetActive(gamePaused);
        gamePlaying = !gamePaused;
    }

    public void PlayerDeath()
    {
        gamePlaying = false;
        gameIsPausable = false;
    }

    public void LevelComplete()
    {
        gamePlaying = false;
        gameIsPausable = false;
        levelWinUI.SetActive(true);
    }
}
