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

    public List<GameObject> weapons; // Lista de armas en la escena
    private int currentWeaponIndex = 0; // Índice de arma actual
    public Transform firePoint;
    public float bulletSpeed = 20f;
    NewInputSystem inputSystem;

<<<<<<< HEAD
    private Giroscopio gyroScript; // Referencia al script de giroscopio
=======
    private Giroscopio gyroScript;
>>>>>>> parent of 5cff16e (Revert "conflicto resueltoe en Scene_Rover.unity manteniendo cambio remoto")

    void Awake()
    {
        inputSystem = new NewInputSystem();

        inputSystem.Player.Movement.performed += ctx => dir = ctx.ReadValue<Vector2>();
        inputSystem.Player.Movement.canceled += ctx => dir = Vector2.zero;
        inputSystem.Player.Shoot.performed += ctx => Shoot();
        inputSystem.Player.Jump.performed += ctx => Jump();
<<<<<<< HEAD
        inputSystem.Player.CenterCamera.performed += ctx => CenterCamera(); // Asignar la acción de centrar la cámara
=======
        inputSystem.Player.CenterCamera.performed += ctx => CenterCamera();
        inputSystem.Player.SwitchWeapon.performed += ctx => SwitchWeapon(); // Mapeo para cambiar de arma
>>>>>>> parent of 5cff16e (Revert "conflicto resueltoe en Scene_Rover.unity manteniendo cambio remoto")
    }

    void Start()
    {
<<<<<<< HEAD
        gyroScript = cameraTransform.GetComponent<Giroscopio>(); // Asigna el script de giroscopio de la cámara
=======
        gyroScript = cameraTransform.GetComponent<Giroscopio>();

        // Inicializar el arma actual y ocultar el resto
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }
>>>>>>> parent of 5cff16e (Revert "conflicto resueltoe en Scene_Rover.unity manteniendo cambio remoto")
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
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    void Shoot()
    {
        if (weapons[currentWeaponIndex] == null) return;

        GameObject bulletPrefab = weapons[currentWeaponIndex].GetComponent<weapon>().bulletPrefab; // Obtener el prefab de bala desde el arma actual
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = firePoint.forward * bulletSpeed;
        }
    }

<<<<<<< HEAD
    // Método para centrar la cámara en la rotación personalizada
=======
>>>>>>> parent of 5cff16e (Revert "conflicto resueltoe en Scene_Rover.unity manteniendo cambio remoto")
    void CenterCamera()
    {
        if (gyroScript != null)
        {
            gyroScript.CenterToCustomRotation();
        }
    }
<<<<<<< HEAD
}
=======

    void SwitchWeapon()
    {
        // Ocultar el arma actual
        weapons[currentWeaponIndex].SetActive(false);

        // Cambiar al siguiente índice de arma
        currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

        // Activar la nueva arma
        weapons[currentWeaponIndex].SetActive(true);
    }
}
>>>>>>> parent of 5cff16e (Revert "conflicto resueltoe en Scene_Rover.unity manteniendo cambio remoto")
