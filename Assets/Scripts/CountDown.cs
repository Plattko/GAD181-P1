using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Countdown : MonoBehaviour
{
    // Length of the countdown timer
    public int countdownTime = 3;

    // Reference to the countdown UI
    public GameObject countdownUI;
    // Reference to the countdown text
    public TextMeshProUGUI countdownText;
    // Reference to the Game Controller script
    public GameController gameController;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    private IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();

            yield return new WaitForSecondsRealtime(1f);

            countdownTime--;
        }

        countdownText.text = "GO!";

        yield return new WaitForSecondsRealtime(0.5f);

        countdownUI.SetActive(false);
        gameController.BeginGame();
    }
}

