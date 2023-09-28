using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CountDown : MonoBehaviour
{
    public float countdownDuration = 3.0f; 
    public TextMeshProUGUI countdownText;
    private bool countDownComplete = false;

    private void Start()
    {
        countdownText.text = countdownDuration.ToString("0");
        InvokeRepeating("UpdateCountdown", 1.0f, 1.0f);
    }

    private void UpdateCountdown()
    {
        countdownDuration--;

        if (countdownDuration <= 0)
        {
            countdownDuration = 0;
            countDownComplete = true;
            CancelInvoke("UpdateCountdown");

            StartCoroutine(DisplayGoMessage());
        }
        else
        {
            countdownText.text = countdownDuration.ToString("0");

        }
    }
    private IEnumerator DisplayGoMessage()
    {
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1.0f); 
        countdownText.text = ""; 
    }
}

