using UnityEngine;

public class AutoWeapon : MonoBehaviour
{
    public GameObject projectilePrefab;       public Transform firePoint;               public float fireRate = 1f;               private float nextFireTime = 0f;      
    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;         }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);     }
}