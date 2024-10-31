using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public float speed = 4f;
    public float jumpForce = 5f; // Fuerza de salto
    public float groundCheckDistance = 0.1f; // Distancia para verificar el suelo
    private bool isGrounded = true; // Verifica si el personaje está en el suelo
    Vector2 dir = Vector2.zero;
    public Rigidbody rb;
    public Transform cameraTransform;
    NewInputSystem inputSystem;

    void Awake()
    {
        inputSystem = new NewInputSystem();

        inputSystem.Player.Movement.performed += ctx => dir = ctx.ReadValue<Vector2>();
        inputSystem.Player.Movement.canceled += ctx => dir = Vector2.zero;
        inputSystem.Player.Shoot.performed += ctx => Shoot();
        inputSystem.Player.Jump.performed += ctx => Jump(); // Nueva acción de salto
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
        GroundCheck(); // Verificar si está en el suelo en cada frame
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
            isGrounded = false; // Establece que el personaje está en el aire
        }
    }

    void GroundCheck()
    {
        // Realizar un Raycast hacia abajo desde el personaje para verificar si está en el suelo
        // Esto verifica si hay una superficie sólida bajo el personaje dentro de una pequeña distancia
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    void Shoot()
    {
        // Implementación de disparo
    }
}