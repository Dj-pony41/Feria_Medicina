using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] patrolPoints; // Puntos de movimiento designados
    public float moveSpeed = 3f;
    private int currentPointIndex = 0;

    [Header("Attack Settings")]
    public float attackRadius = 5f; // Radio de ataque ajustable
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform firePoint; // Lugar desde donde dispara el proyectil
    public float fireRate = 1f; // Intervalo entre disparos
    private float nextFireTime;

    [Header("Player Detection")]
    public Transform player; // Referencia al jugador

    private void Update()
    {
        Patrol();
        DetectAndAttackPlayer();
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // Cambia al siguiente punto cuando llega al actual
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }

    private void DetectAndAttackPlayer()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius)
        {
            if (Time.time >= nextFireTime)
            {
                Attack();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void Attack()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (player.position - firePoint.position).normalized;
                rb.velocity = direction * 10f; // Velocidad del proyectil
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibuja el radio de ataque en la escena para referencia
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
