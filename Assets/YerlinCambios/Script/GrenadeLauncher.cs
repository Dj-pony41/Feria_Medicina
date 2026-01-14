using System.Collections;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
	public GameObject grenadePrefab; 	public float throwForce = 10f; 	public Camera mainCamera; 
	void Update()
	{
				if (Input.GetMouseButtonDown(0))
		{
			ThrowGrenade();
		}
	}

	void ThrowGrenade()
	{
				GameObject grenade = Instantiate(grenadePrefab, transform.position + transform.forward, Quaternion.identity);

				Rigidbody rb = grenade.GetComponent<Rigidbody>();

				Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		Vector3 throwDirection;
		if (Physics.Raycast(ray, out hit))
		{
						throwDirection = (hit.point - transform.position).normalized;
		}
		else
		{
						throwDirection = transform.forward;
		}

				rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
	}
}
