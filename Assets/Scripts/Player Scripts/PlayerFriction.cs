using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFriction : MonoBehaviour
{
    // Reference to the player's Rigidbody2D
    public Rigidbody2D rb;

    // Whether the player is touching a surface
    public bool onSurface = false;

    public float noFriction = 1f;
    // The collided surface's friction
    public float surfaceFriction = 0.95f;

    // Reference to the player's SpringJoint2D
    private SpringJoint2D spring;

    private void Start()
    {
        // Get the player's SpringJoint2D
        spring = GetComponent<SpringJoint2D>();
    }

    private void FixedUpdate()
    {
        // If the player is sliding along a surface and not grappling, slowly reduce their speed based on the surface type
        if (onSurface && !spring.enabled)
        {
            // Multiple the player's velocity by the surface's 'friction'
            rb.velocity = rb.velocity * surfaceFriction;
        }
        else
        {
            rb.velocity = rb.velocity * noFriction;
        }
    }

    // If the player collides with a surface, set onSurface to true and reduce their velocity based on the surface's friction
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Surface"))
        {
            Debug.Log("Player is not on a surface");

            // Set onSurface to true
            onSurface = true;

            FindAnyObjectByType<CameraShake>().ShakeCamera();
            Debug.Log(rb.velocity);
        }
    }

    // If the player is no longer on a surface, set onSurface to false
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Surface"))
        {
            Debug.Log("Player is not on a surface");

            // Set onSurface to false
            onSurface = false;
        }
    }
}
