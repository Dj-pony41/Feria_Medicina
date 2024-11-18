using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public enum CharacterType { Player, Enemy }  // Enum para definir el tipo de personaje
    public CharacterType characterType = CharacterType.Player; // Combobox en el Inspector para definir si es Player o Enemy

    public float health = 100f;
    public GameObject objectToDestroy;
    public GameOverManager gameOverManager; // Referencia al GameOverManager para activar el Game Over

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Personaje ha muerto");

        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
        else
        {
            Debug.LogWarning("No se ha asignado el objeto a destruir.");
        }

        if (characterType == CharacterType.Player && gameOverManager != null)
        {
            gameOverManager.OnCharacterDeath(); // Llamar al método para activar el Game Over si el personaje es el Player
            Invoke("RestartScene", 3f); // Reiniciar la escena después de 3 segundos
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reiniciar la escena actual
    }
}
