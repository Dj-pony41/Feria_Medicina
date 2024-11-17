using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
	public float speed = 4f;
	public float jumpForce = 5f;
	public float groundCheckDistance = 0.1f;
	private bool isGrounded = true;
	Vector2 dir = Vector2.zero;
	public Rigidbody rb;
	public Transform cameraTransform;

	public List<GameObject> weapons;
	private int currentWeaponIndex = 0;
	public Transform firePoint;
	public float bulletSpeed = 20f;

	NewInputSystem inputSystem;
	private Giroscopio gyroScript;

	public Animator animator;

	void Awake()
	{
		inputSystem = new NewInputSystem();

		inputSystem.Player.Movement.performed += ctx => dir = ctx.ReadValue<Vector2>();
		inputSystem.Player.Movement.canceled += ctx => dir = Vector2.zero;
		inputSystem.Player.Shoot.performed += ctx => Shoot();
		inputSystem.Player.Jump.performed += ctx => Jump();
		inputSystem.Player.CenterCamera.performed += ctx => CenterCamera();
		inputSystem.Player.SwitchWeapon.performed += ctx => SwitchWeapon();
	}

	void Start()
	{
		animator = GetComponent<Animator>(); // Asignar Animator automáticamente
		gyroScript = cameraTransform.GetComponent<Giroscopio>();

		for (int i = 0; i < weapons.Count; i++)
		{
			weapons[i].SetActive(i == currentWeaponIndex);
		}
	}

	void OnEnable()
	{
		inputSystem.Enable();
	}

	void OnDisable()
	{
		inputSystem.Disable();
	}

	void Update()
	{
		Movement();
		GroundCheck();
	}

	void Movement()
	{
		Vector3 forward = cameraTransform.forward;
		Vector3 right = cameraTransform.right;

		forward.y = 0;
		right.y = 0;

		forward.Normalize();
		right.Normalize();

		Vector3 moveDirection = (forward * dir.y + right * dir.x).normalized;

		rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);

		// Calcular y actualizar "Speed" en el Animator
		float movementSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
		animator.SetFloat("Speed", movementSpeed);
		Debug.Log("Speed: " + movementSpeed);
	}

	void Jump()
	{
		if (isGrounded)
		{
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			isGrounded = false;

			// Activar animación de salto
			animator.SetTrigger("Jump");
		}
	}

	void GroundCheck()
	{
		isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
	}

	void Shoot()
	{
		if (weapons[currentWeaponIndex] == null) return;

		GameObject bulletPrefab = weapons[currentWeaponIndex].GetComponent<weapon>().bulletPrefab;
		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
		if (bulletRb != null)
		{
			bulletRb.velocity = firePoint.forward * bulletSpeed;
		}

		// Activar animación de disparo
		animator.SetTrigger("Shoot");
	}

	void CenterCamera()
	{
		if (gyroScript != null)
		{
			gyroScript.CenterToCustomRotation();
		}
	}

	void SwitchWeapon()
	{
		weapons[currentWeaponIndex].SetActive(false);

		currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

		weapons[currentWeaponIndex].SetActive(true);
	}
}
