using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Whether the game is playing, only able to be changed in this script
    public bool gamePlaying { get; private set; }
    // Whether the pause menu is active
    private bool gamePaused = false;

    public GameObject levelWinUI;
    public GameObject pauseMenuUI;
    
    // Start is called before the first frame update
    void Start()
    {
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
    }

    private void BeginGame()
    {
        gamePlaying = true;
    }

    //private void GamePaused()
    //{
    //    gamePlaying = false;
    //}

    private void LevelComplete()
    {
        gamePlaying = false;
        Invoke("ShowLevelCompleteScreen", 1.25f);
    }

    private void ShowLevelCompleteScreen()
    {
        levelWinUI.SetActive(true);
        //HUD.SetActive(false);
    }
}
