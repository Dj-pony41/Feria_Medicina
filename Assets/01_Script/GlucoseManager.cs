using UnityEngine;
using UnityEngine.UI;

public class GlucoseManager : MonoBehaviour
{
    public Slider glucoseBarLeft;  // Barra de glucosa izquierda
    public Slider glucoseBarRight; // Barra de glucosa derecha
    public Image fillImageLeft;    // Imagen de relleno para la barra izquierda
    public Image fillImageRight;   // Imagen de relleno para la barra derecha
    public float maxGlucose = 100f;
    public float minGlucose = 0f;
    public float currentGlucose;

    void Start()
    {
        currentGlucose = 50f;

        // Configuración de ambas barras de glucosa
        if (glucoseBarLeft != null)
        {
            glucoseBarLeft.maxValue = maxGlucose;
            glucoseBarLeft.minValue = minGlucose;
        }

        if (glucoseBarRight != null)
        {
            glucoseBarRight.maxValue = maxGlucose;
            glucoseBarRight.minValue = minGlucose;
        }

        UpdateGlucoseBar();
    }

    public void IncreaseGlucose(float amount)
    {
        currentGlucose += amount;
        currentGlucose = Mathf.Clamp(currentGlucose, minGlucose, maxGlucose);
        UpdateGlucoseBar();
    }

    public void DecreaseGlucose(float amount)
    {
        currentGlucose -= amount;
        currentGlucose = Mathf.Clamp(currentGlucose, minGlucose, maxGlucose);
        UpdateGlucoseBar();
    }

    public void UpdateGlucoseBar()
    {
        if (glucoseBarLeft != null)
        {
            glucoseBarLeft.value = currentGlucose;
        }

        if (glucoseBarRight != null)
        {
            glucoseBarRight.value = currentGlucose;
        }

        // Actualizar el color de relleno para ambas barras
        if (currentGlucose > 50)
        {
            if (fillImageLeft != null)
            {
                fillImageLeft.color = Color.red;
            }
            if (fillImageRight != null)
            {
                fillImageRight.color = Color.red;
            }
        }
        else if (currentGlucose == 50)
        {
            if (fillImageLeft != null)
            {
                fillImageLeft.color = Color.green;
            }
            if (fillImageRight != null)
            {
                fillImageRight.color = Color.green;
            }
        }
        else if (currentGlucose < 50)
        {
            if (fillImageLeft != null)
            {
                fillImageLeft.color = Color.yellow;
            }
            if (fillImageRight != null)
            {
                fillImageRight.color = Color.yellow;
            }
        }
    }
}
