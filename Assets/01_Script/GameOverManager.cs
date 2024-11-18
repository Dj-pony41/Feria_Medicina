using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverText;  // Texto que muestra "Game Over"
    public Image blackScreenOverlay; // Imagen para oscurecer la pantalla
    public float fadeSpeed = 1.0f;   // Velocidad de desvanecimiento

    private bool isGameOver = false;

    void Start()
    {
        // Asegurarse de que el texto de Game Over esté desactivado al inicio
        gameOverText.SetActive(false);
        // Asegurarse de que la pantalla esté clara al inicio
        blackScreenOverlay.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        if (isGameOver)
        {
            // Gradualmente hacer la pantalla más oscura
            blackScreenOverlay.color = Color.Lerp(blackScreenOverlay.color, new Color(0, 0, 0, 1), fadeSpeed * Time.deltaTime);
        }
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        gameOverText.SetActive(true);
    }

    // Este método puede ser llamado cuando la vida del personaje llegue a 0
    public void OnCharacterDeath()
    {
        TriggerGameOver();
    }
}
