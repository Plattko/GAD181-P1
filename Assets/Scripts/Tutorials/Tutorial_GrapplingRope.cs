using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_GrapplingRope : MonoBehaviour
{
    [Header("General Refernces:")]
    public Tutorial_GrapplingGun grapplingGun;
    public LineRenderer m_lineRenderer;

    [Header("General Settings:")]
    [SerializeField] private int precision = 40;
    [Range(0, 20)] [SerializeField] private float straightenLineSpeed = 5;

    [Header("Rope Animation Settings:")]
    public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)] [SerializeField] private float StartWaveSize = 2;
    float waveSize = 0;

    [Header("Rope Progression:")]
    public AnimationCurve ropeProgressionCurve;
    [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 1;

    float moveTime = 0;

    [HideInInspector] public bool isGrappling = true;

    bool straightLine = true;


    private void OnEnable()
    {
        moveTime = 0;
        // Set the number of points in the line renderer to precision
        m_lineRenderer.positionCount = precision;
        // Set the wave size to the start wave size
        waveSize = StartWaveSize;
        // Set whether the line is straight to false
        straightLine = false;

        // Call the LinePointstoFirePoint method
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

    // Set the location of every point in the line to the fire point
    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < precision; i++)
        {
            m_lineRenderer.SetPosition(i, grapplingGun.firePoint.position);
        }
    }

    private void Update()
    {
        moveTime += Time.deltaTime;
        DrawRope();
    }


    void DrawRope()
    {
        if (!straightLine)
        {
            // If the last point in the line renderer reaches the grapple point, start making the line straight
            if (m_lineRenderer.GetPosition(precision - 1).x == grapplingGun.grapplePoint.x)
            {
                // Set straighLine to true
                straightLine = true;
            }
            // Otherwise, call the DrawRopeWaves method
            else
            {
                DrawRopeWaves();
            }
        }
        else
        {
            // Activate the grappling once the grappling rope reaches its destination
            if (!isGrappling)
            {
                // Call the Grapple method from the grapplingGun script
                grapplingGun.Grapple();
                // Set isGrappling to true
                isGrappling = true;
            }
            if (waveSize > 0)
            {
                // Gradually reduce the size of the waves while it is still above 0
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                // Set the wave size to 0
                waveSize = 0;

                // If the amount of points in the line is not 2, make it 2 for better performance
                if (m_lineRenderer.positionCount != 2) 
                {
                    m_lineRenderer.positionCount = 2; 
                }

                // Call the DrawRopeNoWaves method
                DrawRopeNoWaves();
            }
        }
    }

    // !!! COME BACK TO THIS !!!
    void DrawRopeWaves()
    {
        // Iterate through every point on the line renderer
        for (int i = 0; i < precision; i++)
        {
            float delta = (float)i / ((float)precision - 1f);
            Vector2 offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            m_lineRenderer.SetPosition(i, currentPosition);
        }
    }

    // Set the last point in the line to the grapple point and the first point in the line to the fire point
    void DrawRopeNoWaves()
    {
        m_lineRenderer.SetPosition(0, grapplingGun.firePoint.position);
        m_lineRenderer.SetPosition(1, grapplingGun.grapplePoint);
    }
}
