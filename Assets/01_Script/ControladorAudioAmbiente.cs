using UnityEngine;
using UnityEngine.Audio;

public class ControladorAudioAmbiente : MonoBehaviour
{
    public AudioSource audioAmbiente; // Arrastra el AudioSource de ambiente aquí desde el Inspector.
    public AudioMixer audioMixer; // Arrastra el AudioMixer desde el Inspector.
    private const string volumenParametro = "VolumenAmbiente";

    void Start()
    {
        // Inicializa el volumen del ambiente al 50%
        SetVolume(0.5f);
    }

    // Método para ajustar el volumen del audio ambiente
    public void SetVolume(float volume)
    {
        // Asegúrate de que el volumen esté entre 0.0 y 1.0
        volume = Mathf.Clamp(volume, 0.0f, 1.0f);

        // Ajustar el volumen del mixer
        audioMixer.SetFloat(volumenParametro, Mathf.Log10(volume) * 20);
    }
}
