using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrow : MonoBehaviour
{
    public float speed = 500f;         
    public float knockbackForce = 25f; 
    public int knockbackFrames = 2;    
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed); 
        Destroy(gameObject, 5f); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
                        PoisonEffect poisonEffect = other.GetComponent<PoisonEffect>();
            if (poisonEffect != null)
            {
                Debug.Log("Flecha venenosa impactó al player, aplicando veneno y retroceso.");
                poisonEffect.ApplyPoison();
            }

                        Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                knockbackDirection.y = 0;                 StartCoroutine(ApplyKnockback(playerRb, knockbackDirection));
            }

            Destroy(gameObject);         }
        else if (!other.CompareTag("Weapon"))
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ApplyKnockback(Rigidbody playerRb, Vector3 direction)
    {
        for (int i = 0; i < knockbackFrames; i++)
        {
            playerRb.AddForce(direction * knockbackForce, ForceMode.VelocityChange);
            yield return null;         }
    }
}