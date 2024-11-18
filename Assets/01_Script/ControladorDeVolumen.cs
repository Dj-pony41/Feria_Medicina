using UnityEngine;
using UnityEngine.Audio;

public class ControladorDeVolumen : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("VolumenAmbiente", Mathf.Log10(volume) * 20); // Ajustar volumen en decibelios
    }
}
