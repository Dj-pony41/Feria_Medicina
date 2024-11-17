using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : MonoBehaviour
{
    private Character_Controller characterController;
    private bool isSlowed = false;
    public float slowMultiplier = 0.2f;     public float slowDuration = 5f;     
    void Start()
    {
                characterController = GetComponent<Character_Controller>();
    }

    public void ApplySlowEffect()
    {
        if (!isSlowed && characterController != null)
        {
            StartCoroutine(SlowPlayer());
        }
    }

    private IEnumerator SlowPlayer()
    {
        isSlowed = true;

                Debug.Log("Ralentización activada. Velocidad antes: " + characterController.speed);

                characterController.speed *= slowMultiplier;

                Debug.Log("Nueva velocidad del player: " + characterController.speed);

                yield return new WaitForSeconds(slowDuration);

                characterController.speed /= slowMultiplier;

                Debug.Log("Ralentización desactivada. Velocidad restaurada a: " + characterController.speed);

        isSlowed = false;
    }
}