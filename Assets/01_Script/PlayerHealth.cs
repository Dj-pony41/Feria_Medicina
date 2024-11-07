using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    // Objeto que desaparecerá al morir (arrástralo en el editor)
    public GameObject objectToDestroy;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player ha muerto");

        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
        else
        {
            Debug.LogWarning("No se ha asignado el objeto a destruir.");
        }
    }
}
