using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f; // Duración del proyectil antes de desaparecer

    void Start()
    {
        // Destruir el proyectil después de cierto tiempo
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destruir el proyectil al colisionar con cualquier objeto
        Destroy(gameObject);
    }
}
