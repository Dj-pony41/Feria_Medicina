using UnityEngine;
using UnityEngine.AI;

public class AcidosGrasosTrans : MonoBehaviour
{
    // Componentes
    private NavMeshAgent agent;
    private SphereCollider detectionCollider;

    // Configuración de patrulla
    public float patrolRadius = 10f;
    public float waitTime = 2f;
    public float detectionRadius = 5f;

    // Velocidad del agente
    public float agentSpeed = 2.5f;

    // Estados
    private bool persiguiendoJugador = false;
    private Transform jugador;
    private Vector3 puntoDestino;
    private bool esperando = false;
    private float tiempoEspera;

    void Start()
    {
        // Inicializar componentes
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
            agent = gameObject.AddComponent<NavMeshAgent>();

        // Configurar la velocidad del agente
        agent.speed = agentSpeed;

        // Configurar detector
        ConfigurarDetector();

        // Iniciar patrulla
        BuscarNuevoDestino();

        // Buscar al jugador
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (persiguiendoJugador)
        {
            PerseguirJugador();
        }
        else
        {
            Patrullar();
        }
    }

    void ConfigurarDetector()
    {
        detectionCollider = gameObject.AddComponent<SphereCollider>();
        detectionCollider.radius = detectionRadius;
        detectionCollider.isTrigger = true;
    }

    void Patrullar()
    {
        // Buscar un nuevo punto de destino cada frame
        BuscarNuevoDestino();

        // Ajustar la velocidad del agente mientras patrulla
        agent.speed = agentSpeed;

        // Mover al enemigo hacia el nuevo punto de destino
        agent.SetDestination(puntoDestino);
    }

    void BuscarNuevoDestino()
    {
        // Generar una nueva dirección aleatoria dentro del rango de patrulla
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

        // Encontrar un punto válido en la malla de navegación
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            puntoDestino = hit.position;
        }
    }

    void PerseguirJugador()
    {
        if (jugador != null)
        {
            agent.SetDestination(jugador.position);

            // Ajustar la velocidad del agente mientras persigue al jugador
            agent.speed = agentSpeed * 1.5f; // Aumentar la velocidad mientras persigue

            // Si el jugador está muy lejos, volver a patrullar
            if (Vector3.Distance(transform.position, jugador.position) > detectionRadius * 1.5f)
            {
                persiguiendoJugador = false;
                BuscarNuevoDestino();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            persiguiendoJugador = true;
        }
    }

    // Opcional: Visualización en el editor
    void OnDrawGizmosSelected()
    {
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawWireSphere(transform.position, detectionRadius);
        // if (puntoDestino != Vector3.zero)
        // {
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawSphere(puntoDestino, 0.3f);
        // }
    }
}