using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBloodCell : MonoBehaviour
{
    public float trappedDuration = 2f;

    public void Trapped()
    {
        // Aplicar efecto de atrapamiento al glóbulo blanco
        StartCoroutine(TrapRoutine());
    }

    private IEnumerator TrapRoutine()
    {
        // Detener el movimiento del glóbulo blanco
        GetComponent<CharacterController>().enabled = false;

        // Aplicar animación o efecto visual de atrapamiento
        // ...

        yield return new WaitForSeconds(trappedDuration);

        // Restablecer el movimiento del glóbulo blanco
        GetComponent<CharacterController>().enabled = true;
    }
}