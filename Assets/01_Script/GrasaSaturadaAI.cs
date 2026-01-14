using UnityEngine;
using UnityEngine.AI;

public class GrasaSaturadaAI : MonoBehaviour
{
        private NavMeshAgent agent;
    private SphereCollider detectionCollider;

        public float patrolRadius = 10f;
    public float waitTime = 2f;
    public float detectionRadius = 5f;

        public GameObject rastroAceitosoPrefab;
    public float tiempoEntreRastros = 0.5f;
    private float rastroTimer;

        private bool persiguiendoJugador = false;
    private Transform jugador;
    private Vector3 puntoDestino;
    private bool esperando = false;
    private float tiempoEspera;

    void Start()
    {
                agent = GetComponent<NavMeshAgent>();
        if (agent == null)
            agent = gameObject.AddComponent<NavMeshAgent>();

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

                        if (Vector3.Distance(transform.position, jugador.position) > detectionRadius * 1.5f)
            {
                persiguiendoJugador = false;
                BuscarNuevoDestino();
            }
        }
    }



    void CrearRastro()
    {
                rastroTimer -= Time.deltaTime;
        if (rastroTimer <= 0)
        {
                        if (rastroAceitosoPrefab != null)
            {
                Instantiate(rastroAceitosoPrefab,
                            new Vector3(transform.position.x, 0.01f, transform.position.z),
                            Quaternion.identity);
            }
                        rastroTimer = tiempoEntreRastros;
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        if (puntoDestino != Vector3.zero)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(puntoDestino, 0.3f);
        }
    }
}