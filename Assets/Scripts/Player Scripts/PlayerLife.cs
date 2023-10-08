using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    // Reference to current scene
    //private Scene currentScene;
    // String to store name of current scene
    //private string sceneName;
    // The amount of time to delay before reloading the scene after the player dies
    public float deathDelay = 2f;
    // Reference to the game over UI
    public GameObject playerDeathUI;
    //Reference to AudioManager
    AudioManager audioManager;
    public GameController gameController;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //// Get the player's current scene
        //currentScene = SceneManager.GetActiveScene();
        //// Get the scene name
        //sceneName = currentScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player death menu is active and the player clicks, reload the level
        //if (playerDeathUI.activeSelf && Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    // Unpause the game by setting the time scale to 1
        //    Time.timeScale = 1f;
        //    // Load the current scene
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Killbox"))
        {
            // Call the PlayerDeath method
            PlayerDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Killbox"))
        {
            // Call the PlayerDeath method
            PlayerDeath();
        }
    }
    void PlayerDeath()
    {
        Debug.Log("Player has died");

        //Play death sound effect
        audioManager.PlaySFX(audioManager.death);
        // Activate the game over UI
        playerDeathUI.SetActive(true);
        // Call the PlayerDeath method from the Game Controller script
        gameController.PlayerDeath();
    }


    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        // Wait before reloading scene
        yield return new WaitForSeconds(delay);
        
        // Unpause the game by setting the time scale to 1
        //Time.timeScale = 1f;
        
        // Load the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
