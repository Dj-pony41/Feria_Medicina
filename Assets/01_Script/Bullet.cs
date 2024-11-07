using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f; 
    void Start()
    {
                Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
                Destroy(gameObject);
    }
}
