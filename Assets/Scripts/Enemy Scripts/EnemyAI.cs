using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    // Reference to the player's transform
    public Transform target;

    // The enemy's move speed
    public float moveSpeed = 3f;
    // How close the enemy needs to be to a waypoint before it chooses the next one
    public float nextWaypointDistance = 3f;

    // The current path the enemy is following
    private Path path;
    // Int storing the current waypoint along the path the enemy is following
    private int currentWaypoint = 0;
    // Whether the enemy has reached the end of the path
    private bool reachedEndOfPath = false;

    // Reference to the enemy's Seeker script
    private Seeker seeker;
    // Reference to the enemy's RigidBody2D
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the enemy's Seeker script
        seeker = GetComponent<Seeker>();
        // Get the enemy's RigidBody2D
        rb = GetComponent<Rigidbody2D>();

        // Call a method to update the path at a specific time interval;
        InvokeRepeating("UpdatePath", 0f, 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If the path is null, return out of the function
        if (path == null)
        {
            return;
        }

        // If the enemy has reached the end of the path, stop moving
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // Get the direction of the next waypoint along the path
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        // Calculate the force to add to the enemy to make it move in that direction
        Vector2 force = direction * moveSpeed * Time.deltaTime;
        // Add the force to the enemy
        rb.AddForce(force);

        // Find the distance to the next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        // If that distance is less than the nextWaypointDistance, the enemy has reached the current waypoint
        if(distance < nextWaypointDistance)
        {
            // Increase the currentWaypoint value by 1
            currentWaypoint++;
        }
    }

    // Start a path from the enemy's position to the player's position, and call OnPathComplete once the path is created
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    // Take in the generated path and do the following
    void OnPathComplete(Path p)
    {
        // Check if there are any errors with the path
        if (!p.error)
        {
            // Set the current path to the newly generated path
            path = p;
            // Reset the enemy's progress along its path
            currentWaypoint = 0;
        }
    }
}
