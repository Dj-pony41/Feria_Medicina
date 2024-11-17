using UnityEngine;
using UnityEngine.AI;

public class AcidosGrasosTrans : MonoBehaviour
{
        private NavMeshAgent agent;
    private SphereCollider detectionCollider;

        public float patrolRadius = 10f;
    public float waitTime = 2f;
    public float detectionRadius = 5f;

        public float agentSpeed = 2.5f;

        private bool persiguiendoJugador = false;
    private Transform jugador;
    private Vector3 puntoDestino;
    private bool esperando = false;
    private float tiempoEspera;

    void Start()
    {
                        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }
        else
        {
            Debug.LogWarning("No se encontró un NavMesh en la posición actual. Agente no creado.");
        }
    

        agent.speed = agentSpeed;

                ConfigurarDetector();

                BuscarNuevoDestino();

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
                BuscarNuevoDestino();

                agent.speed = agentSpeed;

                agent.SetDestination(puntoDestino);
    }

    void BuscarNuevoDestino()
    {
                Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;

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

                        agent.speed = agentSpeed * 1.5f; 
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

        void OnDrawGizmosSelected()
    {
                                                            }
}