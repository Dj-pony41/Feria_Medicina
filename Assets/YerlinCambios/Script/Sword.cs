using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
	public int damage = 50; // Daño que inflige la espada

	// Este método se llama automáticamente cuando el Collider de la espada toca otro Collider
	private void OnTriggerEnter(Collider other)
	{
		// Mostrar un mensaje en la consola cuando la espada toca algo
		Debug.Log("Espada tocó: " + other.name);

		// Verificar si el objeto que tocó la espada es un enemigo
		EnemyHealth enemy = other.GetComponent<EnemyHealth>();
		if (enemy != null)
		{
			// Mostrar un mensaje si es un enemigo y aplicar daño
			Debug.Log("Enemigo detectado, aplicando daño");
			enemy.TakeDamage(damage);
		}
	}
}

