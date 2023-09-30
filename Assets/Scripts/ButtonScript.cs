using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button button;
    public AudioSource AudioSource;
    public AudioClip buttonPress;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(PlayAudioAndDisableButton);
    }

    void PlayAudioAndDisableButton()
    {
        button.interactable = false; //this disables the button from being pressed again
        AudioSource.PlayOneShot(buttonPress); //plays the audio

        StartCoroutine(WaitForAudioToFinish());
    }

    IEnumerator WaitForAudioToFinish()
    {
        yield return new WaitForSeconds(buttonPress.length);

        button.interactable = true;
    }
}
