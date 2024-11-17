using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int maxHealth = 100;
	private int currentHealth;

	void Start()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		Debug.Log("Enemigo recibió daño. Vida actual: " + currentHealth);

		if (currentHealth <= 0)
		{
			Die();
		}
	}




	void Die()
	{
				Destroy(gameObject);
	}
}
