using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
	public int damage = 50; 
		private void OnTriggerEnter(Collider other)
	{
				Debug.Log("Espada tocó: " + other.name);

				EnemyHealth enemy = other.GetComponent<EnemyHealth>();
		if (enemy != null)
		{
						Debug.Log("Enemigo detectado, aplicando daño");
			enemy.TakeDamage(damage);
		}
	}
}

