using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : MonoBehaviour
{
    public float poisonDamage = 7f;           public float poisonDuration = 4f;         public float poisonInterval = 0.5f;   
    private bool isPoisoned = false;

        public void ApplyPoison()
    {
        if (!isPoisoned)
        {
            StartCoroutine(PoisonDamageOverTime());
        }
    }

    private IEnumerator PoisonDamageOverTime()
    {
        isPoisoned = true;
        float elapsed = 0f;

                PlayerHealth playerHealth = GetComponent<PlayerHealth>();

        if (playerHealth == null)
        {
            Debug.LogWarning("No se encontró el componente PlayerHealth en el objeto.");
            yield break;         }

        while (elapsed < poisonDuration)
        {
                        playerHealth.TakeDamage(poisonDamage);

            Debug.Log("Efecto de veneno: aplicando " + poisonDamage + " de daño");

            yield return new WaitForSeconds(poisonInterval);
            elapsed += poisonInterval;
        }

        isPoisoned = false;
        Debug.Log("Efecto de veneno terminado");
    }
}