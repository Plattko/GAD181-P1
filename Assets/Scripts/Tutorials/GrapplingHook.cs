using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("Scripts Ref:")]
    public Tutorial_GrapplingRope grappleRope;
    public GameController gameController;


    public RaycastHit2D hit;

    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform playerPosition;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D springJoint2D;
    public Rigidbody2D rb;

    //[Header("Rotation:")]
    //[SerializeField] private bool rotateOverTime = true;
    //[Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    //[Header("Distance:")]
    //[SerializeField] private bool hasMaxDistance = false;
    //[SerializeField] private float maxDistance = 20;
    private enum WhetherToLaunch
    {
        Launch,
        No_Launch
    }

    [Header("Launching:")]
    //[SerializeField] private bool launchToPoint = true;
    [SerializeField] private float launchSpeed = 1;
    private WhetherToLaunch whetherToLaunch = WhetherToLaunch.Launch;

    [Header("No Launch To Point")]
    //[SerializeField] private bool autoConfigureDistance = false;
    //[SerializeField] private float targetDistance = 3;
    //[SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    private float slowEffect = 1;

    // Start is called before the first frame update
    void Start()
    {
        grappleRope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.gamePlaying)
        {
            // If the left or right mouse button is pressed, call the SetGrapplePoint method
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                SetGrapplePoint();
            }
            // If the left or right mouse button is held, do the following
            else if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
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
            // If the left or right mouse button is released, disable the GrappleRope script
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                grappleRope.enabled = false;
            }
            // If the player is not pressing anything, rotate the grappling gun towards the mouse's position
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Hit point is: " + hit.point);
        }
    }

    private void FixedUpdate()
    {
        int layerMask = 1 << 6;
        Vector2 direction = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;

        // Shoot raycast that only detects surfaces
        hit = Physics2D.Raycast(firePoint.position, direction.normalized, 50f, layerMask);

        if (hit.collider != null)
        {
            Debug.DrawLine(firePoint.position, hit.point, Color.green);
        }
        
        if (grappleRope.isGrappling)
        {
            switch (whetherToLaunch)
            {
                case WhetherToLaunch.Launch:
                    Debug.Log("WhetherToLaunch = Launch");
                    rb.AddForce((grapplePoint - (Vector2)playerPosition.position).normalized * launchSpeed * slowEffect);
                    break;

                case WhetherToLaunch.No_Launch:

                    break;

            }
        }
        
    }
    private void SetGrapplePoint()
    {
        grapplePoint = hit.point;

        grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
        grappleRope.enabled = true;
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

    //public void Grapple()
    //{
    //    Debug.Log("Grapple called");
        
    //    switch (whetherToLaunch)
    //    {
    //        case WhetherToLaunch.Launch:
    //            Debug.Log("WhetherToLaunch = Launch");
    //            rb.AddForce((grapplePoint - (Vector2)playerPosition.position).normalized * launchSpeed * slowEffect);
    //            break;

    //        case WhetherToLaunch.No_Launch:

    //            break;

    //    }
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(hit.point, 1f);
    }
}
