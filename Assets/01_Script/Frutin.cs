using UnityEngine;

public class Frutin : MonoBehaviour
{
    public float glucoseValue = 50f; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlucoseManager glucoseManager = FindObjectOfType<GlucoseManager>();
            HealthManager healthManager = FindObjectOfType<HealthManager>();

            if (glucoseManager != null)
            {
                glucoseManager.currentGlucose = glucoseValue;                 glucoseManager.UpdateGlucoseBar();             }

            if (healthManager != null)
            {
                healthManager.OnEatFood();             }

            Debug.Log("Frutin consumido: Glucosa estabilizada en 50. Temporizador reiniciado.");
            Destroy(gameObject);         }
    }
}
