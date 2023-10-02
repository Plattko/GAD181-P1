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
    // Reference to the Game Controller script
    public GameController gameController;

    // Update is called once per frame
    void Update()
    {
        // If the timer is running, call the Countdown method
        if (timerRunning)
        {
            Countdown();
            timerText.text = levelTimer.ToString("0.0");
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

            // Call the LevelComplete method from the Game Controller script
            gameController.LevelComplete();
        }
    }
}
