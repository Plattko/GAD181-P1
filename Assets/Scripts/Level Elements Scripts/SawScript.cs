using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawScript : MonoBehaviour
{
    public float speed;
    Vector3 targetPoints;

    public GameObject movement;
    public Transform[] movePoints;
    int pointIndex;
    int pointCount;
    int direction = 1;

    private void Awake()
    {
        movePoints = new Transform[movement.transform.childCount];
        for (int i = 0; i < movement.gameObject.transform.childCount; i++)
        {
            movePoints[i] = movement.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointCount = movePoints.Length;
        pointIndex = 1;
        targetPoints = movePoints[pointIndex].transform.position;
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPoints, step);

        if(transform.position == targetPoints)
        {
            NextPoint();
        }
    }

    void NextPoint()
    {
        if(pointIndex == pointCount - 1) //has arrived at last point
        {
            direction = -1;
        }
        if(pointIndex == 0) // has arrived at first point
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPoints = movePoints[pointIndex].transform.position;
    }
}
