using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    public AudioClip startSound;  // Clip de sonido que se reproducirá al inicio del juego
    private AudioSource audioSource;

    void Start()
    {
        // Obtener el componente AudioSource del GameObject
        audioSource = GetComponent<AudioSource>();

        // Asegurarse de que el sonido solo se reproduzca una vez al inicio
        if (startSound != null)
        {
            audioSource.PlayOneShot(startSound);
        }
    }
}
