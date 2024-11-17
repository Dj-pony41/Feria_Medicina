using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBloodCell : MonoBehaviour
{
    public float trappedDuration = 2f;

    public void Trapped()
    {
                StartCoroutine(TrapRoutine());
    }

    private IEnumerator TrapRoutine()
    {
                GetComponent<CharacterController>().enabled = false;

                
        yield return new WaitForSeconds(trappedDuration);

                GetComponent<CharacterController>().enabled = true;
    }
}