using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour
{
    // array to hold the names of level scenes
    public string[] levelScenes;

    // this method is called when level button is clicked
    public void LoadLevel(int levelIndex)
    {
        // checks if the index is within bounds
        if (levelIndex >= 0 && levelIndex < levelScenes.Length)
        {
            // loads the selected level by scene name
            SceneManager.LoadScene(levelScenes[levelIndex]);
        }
        else
        {
            Debug.LogError("Invalid level index: " + levelIndex);
        }
    }
}