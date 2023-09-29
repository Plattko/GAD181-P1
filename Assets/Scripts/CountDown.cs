using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CountDown : MonoBehaviour
{
    // Length of the countdown timer
    public float countdownDuration = 3.0f;
    // Whether the countdown is complete
    private bool countDownComplete = false;

    // Reference to the countdown text
    public TextMeshProUGUI countdownText;

    private void Start()
    {
        // Display the countdown text as the countdown duration to 1 integer
        countdownText.text = countdownDuration.ToString("0");
        // Begin repeating the UpdateCountdown method once every second after 1 second
        InvokeRepeating("UpdateCountdown", 1.0f, 1.0f);
    }

    private void UpdateCountdown()
    {
        // Reduce countdownDuration by 1
        countdownDuration--;

        // If the countdown reaches 0, do the following
        if (countdownDuration <= 0)
        {
            // Set the countdown duration to 0
            countdownDuration = 0;
            // Set countDownComplete to true
            countDownComplete = true;
            // Stop repeating the UpdateCountdown method every second
            CancelInvoke("UpdateCountdown");

            // Call the DisplayGoMessage coroutine
            StartCoroutine(DisplayGoMessage());
        }
        // If the countdown is still running, update the countdown text
        else
        {
            countdownText.text = countdownDuration.ToString("0");

        }
    }

    // Display the go text and then make it disappear after 1 second
    private IEnumerator DisplayGoMessage()
    {
        // Set the countdown text to "Go!"
        countdownText.text = "Go!";
        // Wait for 1 second
        yield return new WaitForSeconds(1.0f); 
        // Set the countdown text to nothing
        countdownText.text = ""; 
    }
}

