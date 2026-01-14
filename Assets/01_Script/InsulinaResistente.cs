using UnityEngine;
using System.Collections;

public class InsulinaResistente : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float shieldDuration = 5f;
    public float shieldCooldown = 8f;
    public float barrierRadius = 3f;

    [Header("References")]
    public GameObject shieldVisualPrefab;
    private GameObject activeShield;
    private bool isShieldActive = false;
    private bool canUseShield = true;

    [Header("Effects")]
    public Material normalMaterial;
    public Material shieldedMaterial;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
                if (canUseShield && !isShieldActive)
        {
            DetectWhiteBloodCells();
        }
    }

    private void DetectWhiteBloodCells()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, barrierRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("WhiteBloodCell"))
            {
                ActivateShield();
                break;
            }
        }
    }

    public void ActivateShield()
    {
        if (!canUseShield) return;

        StartCoroutine(ShieldRoutine());
    }

    private IEnumerator ShieldRoutine()
    {
                isShieldActive = true;
        canUseShield = false;

                activeShield = Instantiate(shieldVisualPrefab, transform.position, Quaternion.identity);
        activeShield.transform.parent = transform;

                meshRenderer.material = shieldedMaterial;

                yield return new WaitForSeconds(shieldDuration);

                if (activeShield != null)
        {
            Destroy(activeShield);
        }
        meshRenderer.material = normalMaterial;
        isShieldActive = false;

                yield return new WaitForSeconds(shieldCooldown);
        canUseShield = true;
    }

    public void TakeDamage(float damage)
    {
        if (isShieldActive) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
                        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
                Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, barrierRadius);
    }
}