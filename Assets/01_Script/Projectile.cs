using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 500f;     private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);         Destroy(gameObject, 5f);     }

    void OnTriggerEnter(Collider other)
    {
                if (other.CompareTag("Enemy"))
        {
                        SlowEffect slowEffect = other.GetComponent<SlowEffect>();
            if (slowEffect != null)
            {
                Debug.Log("Proyectil impactó al player, aplicando ralentización.");                 slowEffect.ApplySlowEffect();             }
            Destroy(gameObject);         }
        else if (!other.CompareTag("Weapon"))
        {
                        Destroy(gameObject);
        }
    }
}