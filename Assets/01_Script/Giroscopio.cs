using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giroscopio : MonoBehaviour
{
    public float sensitivity = 0.1f; // Control de sensibilidad
    public float smoothSpeed = 5f; // Control de suavidad del movimiento

    private Quaternion initialCameraRotation; // Rotación inicial de la cámara

    void Start()
    {
        // Activa el giroscopio si está disponible
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            initialCameraRotation = transform.rotation; // Guarda la rotación inicial de la cámara
        }
        else
        {
            Debug.LogWarning("Giroscopio no soportado en este dispositivo.");
        }
    }

    void Update()
    {
        if (Input.gyro.enabled)
        {
            // Obtiene la rotación del giroscopio
            Quaternion deviceRotation = Input.gyro.attitude;
            deviceRotation = Quaternion.Euler(90f, 0f, 0f) * new Quaternion(-deviceRotation.x, -deviceRotation.y, deviceRotation.z, deviceRotation.w);

            // Calcula la rotación de la cámara usando la sensibilidad
            Quaternion targetRotation = initialCameraRotation * deviceRotation;

            // Suaviza el movimiento hacia la rotación objetivo
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        }
    }
}