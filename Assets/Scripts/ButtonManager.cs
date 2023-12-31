using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour

{
    // The name of the next level to load
    public string nextLevel;
    public string levelSelectScene;
    public string startScreen;

    public void LoadNextLevel()
    {
        // Load the specified level
        SceneManager.LoadScene(nextLevel);
    }
    public void LoadLevelSelectScene()
    {
        SceneManager.LoadScene(levelSelectScene);
    }

    public void RetryLevel()
    {
        // Load the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Quit to menu
        SceneManager.LoadScene(startScreen);
    }
}
