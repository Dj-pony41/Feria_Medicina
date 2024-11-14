using System.Collections;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
	public GameObject grenadePrefab; // Prefab de la granada
	public float throwForce = 10f; // Fuerza del lanzamiento
	public Camera mainCamera; // Cámara principal

	void Update()
	{
		// Detectar si se hace clic con el botón izquierdo del mouse
		if (Input.GetMouseButtonDown(0))
		{
			ThrowGrenade();
		}
	}

	void ThrowGrenade()
	{
		// Crear la granada en la posición del jugador
		GameObject grenade = Instantiate(grenadePrefab, transform.position + transform.forward, Quaternion.identity);

		// Obtener el Rigidbody de la granada para aplicar la fuerza
		Rigidbody rb = grenade.GetComponent<Rigidbody>();

		// Apuntar hacia el punto de la pantalla donde está el mouse
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		Vector3 throwDirection;
		if (Physics.Raycast(ray, out hit))
		{
			// Calcular la dirección hacia el punto de impacto
			throwDirection = (hit.point - transform.position).normalized;
		}
		else
		{
			// Si no golpea nada, lanzar hacia adelante
			throwDirection = transform.forward;
		}

		// Aplicar la fuerza para lanzar la granada
		rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
	}
}
