using UnityEngine;

public class RastroAceitoso : MonoBehaviour
{
    public float tiempoEntreRastros = 2f;     private float duracion;
    private float tiempoVida;
    private Material material;

    public Transform player;     public float speed = 3f;     public float damageAmount = 10f; 
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
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }

                tiempoVida -= Time.deltaTime;
        if (material != null)
        {
            Color color = material.color;
            color.a = Mathf.Clamp01(tiempoVida / duracion);
            material.color = color;
        }
        if (tiempoVida <= 0)
        {
            Destroy(gameObject);
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
            glucoseManager.IncreaseGlucose(damageAmount);             Debug.Log("Daño aplicado al jugador: " + damageAmount);
        }
    }
}
