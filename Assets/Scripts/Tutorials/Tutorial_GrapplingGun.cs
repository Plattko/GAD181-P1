using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public Tutorial_GrapplingRope grappleRope;
    public GameController gameController;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistance = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    // When the game starts, disable the GrappleRope script and the player's SpringJoint2D
    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
    }


    private void Update()
    {
        if (gameController.gamePlaying)
        {
            // If the left mouse button is pressed, call the SetGrapplePoint method
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SetGrapplePoint();
            }
            // If the left mouse button is held, do the following
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                // If the GrappleRope script is enabled, rotate the grappling gun to follow the grapple point immediately (instead of rotate over time)
                if (grappleRope.enabled)
                {
                    RotateGun(grapplePoint, false);
                }
                else
                {
                    Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                    RotateGun(mousePos, true);
                }

                // MAY REMOVE
                if (launchToPoint && grappleRope.isGrappling)
                {
                    // Launch calculation for transform launch
                    if (launchType == LaunchType.Transform_Launch)
                    {
                        Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                        Vector2 targetPos = grapplePoint - firePointDistnace;
                        gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                    }
                }
            }
            // If the left mouse button is released, disable the GrappleRope script and the player's SpringJoint2D
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                grappleRope.enabled = false;
                m_springJoint2D.enabled = false;

                // If it is a transform launch, set the player's gravity scale back to 1
                if (launchType == LaunchType.Transform_Launch)
                {
                    m_rigidbody.gravityScale = 1;
                }
            }
            // If the player is not pressing anything, rotate the grappling gun towards the mouse's position
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }
        }
    }

    // Rotate the grappling gun in the direction of the mouse's position
    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        // Calculate the distance vector between the position of the mouse and the gun pivot
        Vector3 distanceVector = lookPoint - gunPivot.position;
        // Calculate the angle the grappling gun needs to rotate
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        
        if (rotateOverTime && allowRotationOverTime)
        {
            // Lerp between the current rotation and the desired rotation
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            // Instantly rotate in the direction of the mouse
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    void SetGrapplePoint()
    {
        Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                // If the grapplable object is within the grappling gun's max distance or there is no max distance
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistance || !hasMaxDistance)
                {
                    // Set the grapple point to the hit point
                    grapplePoint = _hit.point;
                    // Set the grappleDistanceVector variable to be used in the GrappleRope script
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    // Enable the grappling rope in the GrappleRope script
                    grappleRope.enabled = true;
                }
            }
        }
    }

    public void Grapple()
    {
        // Disable auto configure distance
        m_springJoint2D.autoConfigureDistance = false;
        
        // !!! COME BACK TO THIS !!!
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }
        // If launching the player to the grapple point is true, check which kind of launch it is
        else
        {
            switch (launchType)
            {
                // If it is a physics launch
                case LaunchType.Physics_Launch:
                    // Set the connected anchor to the grapple point
                    m_springJoint2D.connectedAnchor = grapplePoint;
                    // Calculate the distance vector of the fire point and the player's position
                    Vector2 distanceVector = firePoint.position - gunHolder.position;
                    // Set the distance of the spring joint to the magnitude of the distance vector
                    m_springJoint2D.distance = distanceVector.magnitude;
                    // Set the frequency to launch speed
                    m_springJoint2D.frequency = launchSpeed;
                    // Enable the spring joint
                    m_springJoint2D.enabled = true;

                    break;

                // If it is a transform launch
                case LaunchType.Transform_Launch:
                    // Set the player's gravity scale to 0
                    m_rigidbody.gravityScale = 0;
                    // Set the player's velocity to 0
                    m_rigidbody.velocity = Vector2.zero;
                    
                    break;
            }
        }
    }

    // Show the maximum distance of the grappling hook as a green circle in the inspector
    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }

}

