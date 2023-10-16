using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioManager audioManager; // reference to AudioManager script

    private void Start()
    {
        // find the AudioManager in scene
        audioManager = FindObjectOfType<AudioManager>();
    }

    // This function is called when a collision occurs
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves anything with the "Surface" tag
        if (collision.gameObject.CompareTag("Surface"))
        {
            // play collision sound effect
            audioManager.PlaySFX(audioManager.wallCollide);
        }
    }
}
