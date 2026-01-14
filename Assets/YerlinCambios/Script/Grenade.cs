using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
	public float explosionDelay = 2f; 	public float explosionRadius = 5f; 	public float innerRadius = 2f; 	public int innerDamage = 50; 	public int outerDamage = 30; 
	void Start()
	{
				StartCoroutine(ExplodeAfterDelay());
	}

	IEnumerator ExplodeAfterDelay()
	{
		yield return new WaitForSeconds(explosionDelay);
		Explode();
	}

	void Explode()
	{
				Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

		foreach (Collider nearbyObject in colliders)
		{
						EnemyHealth enemy = nearbyObject.GetComponent<EnemyHealth>();
			if (enemy != null)
			{
								float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);

								if (distance <= innerRadius)
				{
					enemy.TakeDamage(innerDamage); 				}
				else if (distance <= explosionRadius)
				{
					enemy.TakeDamage(outerDamage); 				}
			}
		}

				Destroy(gameObject);
	}
}
