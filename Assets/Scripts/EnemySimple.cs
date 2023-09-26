using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
}
