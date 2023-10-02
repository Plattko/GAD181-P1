using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour

{
    // The name of the next level to load
    public string nextLevel;

    public void LoadNextLevel()
    {
        // Load the specified level
        SceneManager.LoadScene(nextLevel);
    }

    public void RetryLevel()
    {
        // Load the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}
