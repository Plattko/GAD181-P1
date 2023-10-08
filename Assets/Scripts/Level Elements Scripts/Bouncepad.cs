using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncepad : MonoBehaviour
{
    public float bounceForce = 100f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
        }
    }
}
