using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobwebs : MonoBehaviour
{
    private GameObject cobweb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Player was caught in a cobweb");
        }
    }
}
