using UnityEngine;

public class ShieldBarrier : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float pulseSpeed = 1f;
    public float pulseAmount = 0.1f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Rotar el escudo
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Efecto de pulso
        float pulse = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = originalScale * pulse;
    }
}