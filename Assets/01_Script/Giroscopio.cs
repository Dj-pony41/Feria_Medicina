using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giroscopio : MonoBehaviour
{
    public float sensitivity = 0.1f;     public float smoothSpeed = 5f;     public Vector3 customCenterRotation; 
    private Quaternion initialCameraRotation; 
    void Start()
    {
                if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            initialCameraRotation = transform.rotation;         }
        else
        {
            Debug.LogWarning("Giroscopio no soportado en este dispositivo.");
        }
    }

    void Update()
    {
        if (Input.gyro.enabled)
        {
                        Quaternion deviceRotation = Input.gyro.attitude;
            deviceRotation = Quaternion.Euler(90f, 0f, 0f) * new Quaternion(-deviceRotation.x, -deviceRotation.y, deviceRotation.z, deviceRotation.w);

                        Quaternion targetRotation = initialCameraRotation * deviceRotation;

                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        }
    }

        public void CenterToCustomRotation()
    {
        Quaternion customRotation = Quaternion.Euler(customCenterRotation);
        transform.rotation = customRotation;     }
}