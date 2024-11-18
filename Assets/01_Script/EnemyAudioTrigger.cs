using UnityEngine;

public class EnemyAudioTrigger : MonoBehaviour
{
    public AudioClip alertClip; // El clip de audio que quieres reproducir
    private bool hasPlayed = false; // Indica si el audio ya se ha reproducido
    private AudioSource audioSource;

    void Start()
    {
        // Obtén el componente AudioSource del jugador
        audioSource = GetComponent<AudioSource>();

        // Asegúrate de que el AudioSource esté configurado
        if (audioSource == null)
        {
            Debug.LogError("No se encontró AudioSource en el jugador. Agrega un AudioSource.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en el trigger es un enemigo
        if (other.CompareTag("Enemy") && !hasPlayed)
        {
            // Reproduce el clip de audio si aún no se ha reproducido
            if (alertClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(alertClip);
                hasPlayed = true; // Marca que el audio ya se ha reproducido
            }
        }
    }
}
