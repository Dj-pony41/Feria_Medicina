using System.Collections;
using UnityEngine;

public class HeartBeatEffect : MonoBehaviour
{
    public GlucoseManager glucoseManager;     public HealthManager healthManager;       public AudioSource heartBeatAudio;       private bool isCritical = false;     
   void Start()
    {
        if (heartBeatAudio == null)
        {
            heartBeatAudio = GetComponent<AudioSource>();
        }

        if (heartBeatAudio == null)
        {
            Debug.LogError("El AudioSource no está asignado. Por favor, verifica tu configuración.");
        }
    }



    void Update()
    {
        MonitorGlucoseLevels();
    }

    private void MonitorGlucoseLevels()
    {
        if (glucoseManager == null) return;

        float glucoseLevel = glucoseManager.currentGlucose;
        Debug.Log($"Nivel de glucosa actual: {glucoseLevel}");

        if (glucoseLevel > 75 && !isCritical)         {
            ActivateCriticalEffects();
        }
        else if (glucoseLevel <= 75 && isCritical)         {
            DeactivateCriticalEffects();
        }
    }

    private void ActivateCriticalEffects()
    {
        isCritical = true;

                if (heartBeatAudio != null && !heartBeatAudio.isPlaying)
        {
            Debug.Log("Reproduciendo latidos: Glucosa crítica.");
            heartBeatAudio.Play();
        }
        else
        {
            Debug.LogError("Error: AudioSource no está configurado correctamente.");
        }

                if (healthManager != null)
        {
            StartCoroutine(ReduceHealthOverTime());
        }
    }

    private void DeactivateCriticalEffects()
    {
        isCritical = false;

                if (heartBeatAudio != null && heartBeatAudio.isPlaying)
        {
            Debug.Log("Deteniendo latidos: Glucosa aceptable.");
            heartBeatAudio.Stop();
        }

                if (healthManager != null)
        {
            StopCoroutine(ReduceHealthOverTime());
        }
    }

    private IEnumerator ReduceHealthOverTime()
    {
        while (isCritical)
        {
            if (healthManager != null)
            {
                healthManager.DecreaseHealth(2f);                 Debug.Log("Vida reducida debido a glucosa crítica.");
            }
            yield return new WaitForSeconds(2f);         }
    }
}
