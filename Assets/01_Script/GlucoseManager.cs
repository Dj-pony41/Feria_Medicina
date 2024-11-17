using UnityEngine;
using UnityEngine.UI;

public class GlucoseManager : MonoBehaviour
{
    public Slider glucoseBar;              public Image fillImage;                public float maxGlucose = 100f;        public float minGlucose = 0f;          public float currentGlucose;      
    void Start()
    {
                currentGlucose = 50f;
        glucoseBar.maxValue = maxGlucose;
        glucoseBar.minValue = minGlucose;
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
        glucoseBar.value = currentGlucose;

                if (currentGlucose > 50)
        {
            fillImage.color = Color.red;         }
        else if (currentGlucose == 50)
        {
            fillImage.color = Color.green;         }
        else if (currentGlucose < 50)
        {
            fillImage.color = Color.yellow;         }
    }
}
