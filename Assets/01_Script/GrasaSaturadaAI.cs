using UnityEngine;
using UnityEngine.AI;

public class GrasaSaturadaAI : MonoBehaviour
{
    public Transform[] patrolPoints;  // Puntos de patrullaje
    public GameObject oilTrailPrefab; // Prefab del rastro aceitoso
    private NavMeshAgent agent;
    private int currentPatrolIndex;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Verifica que haya puntos de patrullaje y que el agente esté en el NavMesh
        if (patrolPoints.Length > 0 && agent.isOnNavMesh)
        {
            agent.destination = patrolPoints[0].position;
        }

        // Invoca LeaveOilTrail cada 0.5 segundos para crear el rastro
        InvokeRepeating("LeaveOilTrail", 0.5f, 0.5f);
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        // Verifica que el agente esté activo en el NavMesh antes de acceder a remainingDistance
        if (agent.isOnNavMesh && agent.remainingDistance < 0.5f && patrolPoints.Length > 0)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    void LeaveOilTrail()
    {
        if (agent.velocity.magnitude > 0.1f) // Solo deja rastro si está en movimiento
        {
            Instantiate(oilTrailPrefab, transform.position, Quaternion.identity);
        }
    }
}
