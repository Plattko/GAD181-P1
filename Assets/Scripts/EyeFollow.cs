using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollow : MonoBehaviour
{
    public Transform playerPosition;

    public float offsetDistance = 0.5f;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        // Get the direction the eyes will be offset in
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerPosition.position).normalized;
        // Set the eyes' position to be offset in the direction of the cursor
        transform.position = (Vector2)playerPosition.position + direction * offsetDistance;
    }
}
