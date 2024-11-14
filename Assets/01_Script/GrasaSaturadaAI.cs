using UnityEngine;
using UnityEngine.AI;

public class GrasaSaturadaAI : MonoBehaviour
{
<<<<<<< HEAD
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
=======
    // Componentes
    private NavMeshAgent agent;
    private SphereCollider detectionCollider;

    // Configuración de patrulla
    public float patrolRadius = 10f;
    public float waitTime = 2f;
    public float detectionRadius = 5f;

    // Configuración de rastro
    public GameObject rastroAceitosoPrefab;
    public float tiempoEntreRastros = 0.5f;
    private float rastroTimer;

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

        // Configurar detector
        ConfigurarDetector();

        // Iniciar patrulla
        BuscarNuevoDestino();

        // Buscar al jugador
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
>>>>>>> 0540c6e978925f29fd2e70c250dfec17d574f602
    }

    void Update()
    {
<<<<<<< HEAD
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
=======
        if (persiguiendoJugador)
        {
            PerseguirJugador();
        }
        else
        {
            Patrullar();
        }

        if (agent.velocity.magnitude > 0.1f)
        {
            CrearRastro();
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
        if (esperando)
        {
            tiempoEspera -= Time.deltaTime;
            if (tiempoEspera <= 0)
            {
                esperando = false;
                BuscarNuevoDestino();
            }
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            esperando = true;
            tiempoEspera = waitTime;
        }
    }

    void BuscarNuevoDestino()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
        {
            puntoDestino = hit.position;
            agent.SetDestination(puntoDestino);
        }
    }

    void PerseguirJugador()
    {
        if (jugador != null)
        {
            agent.SetDestination(jugador.position);

            // Si el jugador está muy lejos, volver a patrullar
            if (Vector3.Distance(transform.position, jugador.position) > detectionRadius * 1.5f)
            {
                persiguiendoJugador = false;
                BuscarNuevoDestino();
            }
        }
    }



    void CrearRastro()
    {
        // Disminuir el temporizador
        rastroTimer -= Time.deltaTime;
        if (rastroTimer <= 0)
        {
            // Crear un rastro independientemente de los anteriores
            if (rastroAceitosoPrefab != null)
            {
                Instantiate(rastroAceitosoPrefab,
                            new Vector3(transform.position.x, 0.01f, transform.position.z),
                            Quaternion.identity);
            }
            // Reinicia el temporizador para el próximo rastro
            rastroTimer = tiempoEntreRastros;
        }
    }

    //void CrearRastro()
    //{
    //    // Disminuir el temporizador
    //    rastroTimer -= Time.deltaTime;

    //    if (rastroTimer <= 0)
    //    {
    //        // Crear un rastro independientemente de los anteriores
    //        if (rastroAceitosoPrefab != null)
    //        {
    //            Instantiate(rastroAceitosoPrefab,
    //                        new Vector3(transform.position.x, 0.01f, transform.position.z),
    //                        Quaternion.identity);
    //        }

    //        // Reinicia el temporizador para el próximo rastro
    //        rastroTimer = tiempoEntreRastros;
    //    }
    //}



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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (puntoDestino != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(puntoDestino, 0.3f);
        }
    }
}
>>>>>>> 0540c6e978925f29fd2e70c250dfec17d574f602
