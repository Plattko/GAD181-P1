using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Length of the level timer
    private float levelTimer = 10.0f;
    // Whether the timer is running
    private bool timerRunning = true;
    
    // Reference to the timer text
    public TextMeshProUGUI timerText;
    // Reference to the level win UI
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
    }

    // Update is called once per frame
    void Update()
    {
        // If the timer is running, call the Countdown method
        if (timerRunning)
        {
            Countdown();
            timerText.text = levelTimer.ToString("0.0");
        }

        if (levelWinUI.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            // Unpause the game by setting the time scale to 1
            Time.timeScale = 1f;
            // Load the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Countdown until the timer reaches zero
    void Countdown()
    {
        if (levelTimer > 0)
        {
            // Reduce the timer
            levelTimer -= Time.deltaTime;
        }
        else
        {
            // Stop the timer from running
            timerRunning = false;
            // Set the level timer to stay at 0
            levelTimer = 0;
            // State that the timer reached 0 in the console
            Debug.Log("Timer reached zero");
            // Set the colour of the timer text to green
            timerText.color = Color.green;

            // Pause the game by setting time scale to 0
            Time.timeScale = 0f;
            // Activate the level win UI
            levelWinUI.SetActive(true);
        }
    }
}
