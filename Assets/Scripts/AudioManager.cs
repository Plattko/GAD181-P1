using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio CLip")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip wallCollide;

    //public GameObject audioSettingsUI;
    //private bool settingsOpen = false;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        //audioSettingsUI.SetActive(false);
    }

    private void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    settingsOpen = !settingsOpen;
        //    audioSettingsUI.SetActive(settingsOpen);
        //}
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}

