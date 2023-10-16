using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;
    public GameController gameController;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform playerPosition;
    public Transform gunPivot;
    public Transform firePoint;
    public Transform grappleIndicator;

    [Header("Physics Ref:")]
    public SpringJoint2D springJoint2D;
    public Rigidbody2D rb;

    [Header("Launching:")]
    [SerializeField] private float launchSpeed = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    private RaycastHit2D hit;
    private int layerMask = 1 << 6;

    // Start is called before the first frame update
    void Start()
    {
        // Disable the Grapple Rope script and the SpringJoint2D
        grappleRope.enabled = false;
        springJoint2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {        
        if (gameController.gamePlaying)
        {
            // If the left mouse button is pressed, call the SetGrapplePoint method
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SetGrapplePoint();
            }
            // If the left mouse button is held, rotate the grappling gun in the direction of the grapple point
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                if (grappleRope.enabled)
                {
                    RotateGun(grapplePoint);
                }
                else
                {
                    Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                    RotateGun(mousePos);
                }
            }
            // If the left or mouse button is released, disable the Grapple Rope script and the SpringJoint2D
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                grappleRope.enabled = false;
                springJoint2D.enabled = false;
            }
            // If the player is not pressing anything, rotate the grappling gun following the mouse's position
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos);
            }
        }
    }

    private void RotateGun(Vector3 lookPoint)
    {
        // Calculate the distance vector between the position of the mouse and the gun pivot
        Vector3 distanceVector = lookPoint - gunPivot.position;
        // Calculate the angle the grappling gun needs to rotate
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        // Rotate gun in the direction of the lookPoint
        gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void SetGrapplePoint()
    {
        // Set the grapple point to the Raycast2D hit point
        grapplePoint = hit.point;
        // Set the grappleDistanceVector variable to be used in the GrappleRope script
        grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
        // Enable the grappling rope in the GrappleRope script
        grappleRope.enabled = true;
    }

    public void Grapple()
    {
        Debug.Log("Grapple called");

        // Disable auto configure distance
        springJoint2D.autoConfigureDistance = false;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Set the connected anchor to the grapple point
            springJoint2D.connectedAnchor = grapplePoint;
            // Calculate the distance vector of the fire point and the player's position
            Vector2 distanceVector = firePoint.position - playerPosition.position;
            // Set the distance of the spring joint to the magnitude of the distance vector
            springJoint2D.distance = distanceVector.magnitude;
            // Set the frequency to launch speed
            springJoint2D.frequency = launchSpeed;
            // Enable the spring joint
            springJoint2D.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        // Get the direction for the raycast
        Vector2 direction = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        // Shoot raycast that only detects surfaces
        hit = Physics2D.Raycast(playerPosition.position, direction.normalized, 50f, layerMask);

        if (hit.collider != null)
        {
            // Draw a line representing the raycast in the editor
            Debug.DrawLine(playerPosition.position, hit.point, Color.green);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a circle around the hit's position in the editor
        Gizmos.DrawWireSphere(hit.point, 1f);
    }
}
