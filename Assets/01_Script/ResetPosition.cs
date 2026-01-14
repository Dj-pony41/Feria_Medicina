using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    // Posición a la que volverá el personaje cuando toque el collider con el tag especificado.
    public Vector3 respawnPosition = new Vector3(0, 10, 0);

    // Tag del objeto que sirve como límite del mapa
    public string limiteTag = "Limite";

    // Método que se ejecuta al entrar en colisión con otro objeto
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisionó tiene el tag "Limite"
        if (other.CompareTag(limiteTag))
        {
            // Reinicia la posición del personaje
            transform.position = respawnPosition;
        }
    }
}
