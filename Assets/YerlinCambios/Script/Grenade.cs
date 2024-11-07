using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
	public float explosionDelay = 2f; // Tiempo antes de la explosión
	public float explosionRadius = 5f; // Radio de daño total
	public float innerRadius = 2f; // Radio de daño máximo (quita 50)
	public int innerDamage = 50; // Daño en el radio cercano
	public int outerDamage = 30; // Daño en el radio lejano

	void Start()
	{
		// Iniciar la cuenta regresiva para explotar
		StartCoroutine(ExplodeAfterDelay());
	}

	IEnumerator ExplodeAfterDelay()
	{
		yield return new WaitForSeconds(explosionDelay);
		Explode();
	}

	void Explode()
	{
		// Detectar todos los colliders en el radio de la explosión
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

		foreach (Collider nearbyObject in colliders)
		{
			// Verificar si el objeto es un enemigo
			EnemyHealth enemy = nearbyObject.GetComponent<EnemyHealth>();
			if (enemy != null)
			{
				// Calcular la distancia al enemigo
				float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);

				// Aplicar daño dependiendo de la distancia
				if (distance <= innerRadius)
				{
					enemy.TakeDamage(innerDamage); // Daño máximo
				}
				else if (distance <= explosionRadius)
				{
					enemy.TakeDamage(outerDamage); // Daño menor
				}
			}
		}

		// Destruir la granada después de la explosión
		Destroy(gameObject);
	}
}
