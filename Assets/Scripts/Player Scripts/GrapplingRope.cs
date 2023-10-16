using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    [Header("General References:")]
    public GrapplingHook grapplingGun;
    public LineRenderer m_lineRenderer;

    [Header("General Settings:")]
    [SerializeField] private int precision = 40;

    [Header("Rope Progression:")]
    public AnimationCurve ropeProgressionCurve;
    [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 6.7f;

    private float moveTime = 0;

    private bool ropeAtGrapplePoint = true;
    
    // Whether the player is grappling
    [HideInInspector] public bool isGrappling = true;

    private void OnEnable() 
    {
        // Set the move time to zero
        moveTime = 0;
        // Set the number of points in the line renderer to precision
        m_lineRenderer.positionCount = precision;
        // Set whether the rope has reached the grapple point is straight to false
        ropeAtGrapplePoint = false;

        // Call the LinePointsToFirePoint method
        LinePointsToFirePoint();

        // Enable the line renderer
        m_lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        // Disable the line renderer
        m_lineRenderer.enabled = false;

        // Set isGrappling to false
        isGrappling = false;
    }

    // Set the position of every point in the line to the fire point
    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < precision; i++)
        {
            m_lineRenderer.SetPosition(i, grapplingGun.firePoint.position);
        }
    }

    // Increase the move time with delta time
    private void Update()
    {
        moveTime += Time.deltaTime;
        DrawRope();
    }

    void DrawRope()
    {
        if (!ropeAtGrapplePoint)
        {
            // If the last point in the line renderer hasn't reached the grapple point, call the RopeToGrapplePoint method
            if (m_lineRenderer.GetPosition(precision - 1).x != grapplingGun.grapplePoint.x)
            {
                // Call the RopeToGrapplePoint method
                RopeToGrapplePoint();
            }
            // Otherwise, if the last point in the line renderer reaches the grapple point, set ropeAtGrapplePoint to true
            else if (m_lineRenderer.GetPosition(precision - 1).x == grapplingGun.grapplePoint.x)
            {
                // Set ropeAtGrapplePoint to true
                ropeAtGrapplePoint = true;
            }
        }
        else
        {
            // Activate the grappling once the grappling rope reaches its destination
            if (!isGrappling)
            {
                // Call the Grapple method from the Grappling Gun script
                grapplingGun.Grapple();

                // Set isGrappling to true
                isGrappling = true;
            }

            // Reduce the number of points in the line to 2 if above 2
            if (m_lineRenderer.positionCount != 2)
            {
                m_lineRenderer.positionCount = 2;
            }
            
            // Call the RopeConnected method
            RopeConnected();
        }
    }

    // Lerp each point of the grappling rope towards the grapple point
    void RopeToGrapplePoint()
    {
        // Iterate through every point on the line renderer
        for (int i = 0; i < precision; i++)
        {
            float delta = (float)i / ((float)precision - 1f);
            
            Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint, delta);
            Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            // Update the position of each point to the correct position
            m_lineRenderer.SetPosition(i, currentPosition);
        }
    }

    // Set the last point in the line to the grapple point and the first point in the line to the fire point
    void RopeConnected()
    {
        m_lineRenderer.SetPosition(0, grapplingGun.firePoint.position);
        m_lineRenderer.SetPosition(1, grapplingGun.grapplePoint);
    }
}
