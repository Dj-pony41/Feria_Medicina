using UnityEngine;

public class RastroAceitoso : MonoBehaviour
{
    public float tiempoEntreRastros = 2f;
    private float duracion;
    private float tiempoVida;
    private Material material;

    public Transform player;
    public float speed = 3f;
    public float damageAmount = 10f;
    private GlucoseManager glucoseManager;

    void Start()
    {
        duracion = CalcularTiempoDuracion();
        tiempoVida = duracion;

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            material = renderer.material = new Material(renderer.material);
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            glucoseManager = canvas.GetComponent<GlucoseManager>();
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Mover el enemigo hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Girar el enemigo para que mire hacia el jugador
            transform.LookAt(player);
        }

       
    }

    private float CalcularTiempoDuracion()
    {
        return tiempoEntreRastros * 2.5f;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && glucoseManager != null)
        {
            glucoseManager.IncreaseGlucose(damageAmount);
            Debug.Log("Daño aplicado al jugador: " + damageAmount);
        }
    }
}
