using UnityEngine;

public class Frutin : MonoBehaviour
{
    public float glucoseValue = 50f;

    private GlucoseManager glucoseManager;
    private HealthManager healthManager;

    private void Start()
    {
        // Encuentra los managers al iniciar el objeto
        glucoseManager = FindObjectOfType<GlucoseManager>();
        healthManager = FindObjectOfType<HealthManager>();

        if (glucoseManager == null || healthManager == null)
        {
            Debug.LogError("No se encontraron GlucoseManager o HealthManager en la escena.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (glucoseManager != null)
            {
                // Estabiliza la glucosa en 50
                glucoseManager.currentGlucose = glucoseValue;
                glucoseManager.UpdateGlucoseBar();
            }

            if (healthManager != null)
            {
                // Reinicia el temporizador
                healthManager.OnEatFood();
            }

            Debug.Log("Frutin consumido: Glucosa estabilizada en 50. Temporizador reiniciado.");
            Destroy(gameObject);
        }
    }
}