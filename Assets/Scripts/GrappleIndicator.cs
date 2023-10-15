using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleIndicator : MonoBehaviour
{
    private GrapplingHook grapplingGun;
    
    // Start is called before the first frame update
    void Start()
    {
        grapplingGun = GameObject.FindGameObjectWithTag("Player").GetComponent<GrapplingHook>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 hitPoint = grapplingGun.hit.point;
        transform.position = hitPoint;
    }
}
