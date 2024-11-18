using UnityEngine;
using UnityEngine.InputSystem;

public class DisparoSonido : MonoBehaviour
{
    public AudioClip sonidoDisparo;  // Clip de sonido que se reproducirá al disparar
    private AudioSource audioSource;
    private PlayerInput playerInput;

    void Start()
    {
        // Obtener el componente AudioSource que está adjunto al GameObject
        audioSource = GetComponent<AudioSource>();
        // Inicializar el sistema de entrada
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        // Vincular el evento de disparo al sistema de input
        playerInput.actions["Shoot"].performed += OnFire;
    }

    private void OnDisable()
    {
        // Desvincular el evento para evitar errores
        playerInput.actions["Shoot"].performed -= OnFire;
    }

    void OnFire(InputAction.CallbackContext context)
    {
        Disparar();
    }

    void Disparar()
    {
        // Lógica para disparar el proyectil aquí...

        // Reproducir el sonido del disparo
        if (sonidoDisparo != null)
        {
            audioSource.PlayOneShot(sonidoDisparo);
        }
    }

    public void Shoot() // Método para ser llamado desde otro script (como tu controlador principal)
    {
        Disparar();
    }
}
