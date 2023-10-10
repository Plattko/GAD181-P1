using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeIntensity = 1f;
    private float shakeTime = 0.2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin cbmcp;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        StopShake();
    }

    public void ShakeCamera()
    {
        cbmcp.m_AmplitudeGain = shakeIntensity;
        timer = shakeTime;
    }

    void StopShake()
    {
        cbmcp.m_AmplitudeGain = 0f;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ShakeCamera();
        }
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                StopShake();
            }
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShakeCamera();
        }
          if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                StopShake();
            }
        }

    }*/
}
