using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public Animator animator;

    private DisparoSonido disparoSonido;

    public GameObject pauseText; // Objeto de texto 3D para indicar "Pausa"
    private bool isPaused = false; // Estado del juego (pausado o no)

    void Awake()
    {
        inputSystem = new NewInputSystem();

        inputSystem.Player.Movement.performed += ctx => dir = ctx.ReadValue<Vector2>();
        inputSystem.Player.Movement.canceled += ctx => dir = Vector2.zero;
        inputSystem.Player.Shoot.performed += ctx => Shoot();
        inputSystem.Player.Jump.performed += ctx => Jump();
        inputSystem.Player.SwitchWeapon.performed += ctx => SwitchWeapon();
        inputSystem.Player.Pause.performed += ctx => TogglePause();
        inputSystem.Player.CenterCamera.performed += ctx => RestartGame(); // Usar el botón para reiniciar el juego

        disparoSonido = GetComponent<DisparoSonido>();
    }

    void Start()
    {
        animator = GetComponent<Animator>(); // Asignar Animator automáticamente

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }

        // Asegurarse de que el texto de pausa está desactivado al inicio
        if (pauseText != null)
        {
            pauseText.SetActive(false);
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
        if (!isPaused)
        {
            Movement();
            GroundCheck();
        }
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
        if (isGrounded && !isPaused)
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
        if (weapons[currentWeaponIndex] == null || isPaused) return;

        GameObject bulletPrefab = weapons[currentWeaponIndex].GetComponent<weapon>().bulletPrefab;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = firePoint.forward * bulletSpeed;
        }

        // Activar animación de disparo
        animator.SetTrigger("Shoot");

        if (disparoSonido != null)
        {
            disparoSonido.Shoot();
        }
    }

    void SwitchWeapon()
    {
        if (!isPaused)
        {
            weapons[currentWeaponIndex].SetActive(false);

            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;

            weapons[currentWeaponIndex].SetActive(true);
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Pausar el juego
            Time.timeScale = 0f;
            if (pauseText != null)
            {
                pauseText.SetActive(true); // Mostrar el texto de pausa
            }
        }
        else
        {
            // Reanudar el juego
            Time.timeScale = 1f;
            if (pauseText != null)
            {
                pauseText.SetActive(false); // Ocultar el texto de pausa
            }
        }
    }

    void RestartGame()
    {
        if (isPaused)
        {
            // Reanudar el juego antes de reiniciar para asegurar que la escala de tiempo esté en 1
            Time.timeScale = 1f;
        }

        // Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
