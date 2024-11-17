using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider healthBar;
    public GlucoseManager glucoseManager;
    public float maxHealth = 100f;
    private float currentHealth;
    public float healthDecayInterval = 3f;     public float highGlucoseDecayAmount = 5f;     public float moderateGlucoseDecayAmount = 3f;     public float slowEffectMultiplier = 0.5f;
    public float dizzinessEffectDuration = 5f;

    private bool isDizzy = false;
    private bool isApplyingEffects = false;
    private float normalSpeed;
    private Character_Controller characterController;

    private Transform leftCamera;
    private Transform rightCamera;

        private float timeSinceLastMeal = 0f;
    private bool isGlucoseDecreasing = false;
    public float glucoseDecayRate = 1f;     public float glucoseDecayDelay = 10f; 
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        healthBar.fillRect.GetComponent<Image>().color = Color.red;

        characterController = FindObjectOfType<Character_Controller>();
        if (characterController != null)
        {
            normalSpeed = characterController.speed;
        }
        else
        {
            Debug.LogError("No se encontró Character_Controller en la escena.");
        }

        leftCamera = GameObject.Find("Left_Camara")?.transform;
        rightCamera = GameObject.Find("Right_Camara")?.transform;

        if (leftCamera == null || rightCamera == null)
        {
            Debug.LogError("No se encontraron ambas cámaras Left_Camara y Right_Camara.");
        }
    }

    void Update()
    {
        if (glucoseManager != null)
        {
            MonitorGlucoseLevels();
        }
    }

    private void MonitorGlucoseLevels()
{
    float glucoseLevel = glucoseManager.currentGlucose;

    if (glucoseLevel == 50)     {
        timeSinceLastMeal += Time.deltaTime;

        if (timeSinceLastMeal >= glucoseDecayDelay && !isGlucoseDecreasing)
        {
            StartCoroutine(DecreaseGlucoseOverTime());         }
    }
    else
    {
        timeSinceLastMeal = 0f;         isGlucoseDecreasing = false;
        StopCoroutine(DecreaseGlucoseOverTime());     }

    if (glucoseLevel < 50 && !isDizzy)     {
        StartCoroutine(ApplyDizzinessEffect());
    }
}





    public void OnEatFood()
    {
        timeSinceLastMeal = 0f;         isGlucoseDecreasing = false;         StopAllCoroutines();     }



    private IEnumerator DecreaseGlucoseOverTime()
    {
        isGlucoseDecreasing = true;

        while (glucoseManager.currentGlucose > 0)
        {
            glucoseManager.DecreaseGlucose(2f);             Debug.Log($"Glucosa actual: {glucoseManager.currentGlucose}");             yield return new WaitForSeconds(1.5f);         }

        isGlucoseDecreasing = false;
    }



    private IEnumerator ApplyDizzinessEffect()
    {
        isDizzy = true;
        float elapsed = 0f;
        while (elapsed < dizzinessEffectDuration)
        {
            if (leftCamera != null && rightCamera != null)
            {
                float rotationAmount = Mathf.Sin(Time.time * 10) * 0.5f;
                leftCamera.localRotation = Quaternion.Euler(0, 0, rotationAmount);
                rightCamera.localRotation = Quaternion.Euler(0, 0, rotationAmount);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (leftCamera != null && rightCamera != null)
        {
            leftCamera.localRotation = Quaternion.identity;
            rightCamera.localRotation = Quaternion.identity;
        }
        isDizzy = false;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("El jugador ha muerto.");
        }
    }

}
