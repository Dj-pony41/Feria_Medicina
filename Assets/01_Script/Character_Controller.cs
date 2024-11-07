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
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    NewInputSystem inputSystem;

    private Giroscopio gyroScript; 
    void Awake()
    {
        inputSystem = new NewInputSystem();

        inputSystem.Player.Movement.performed += ctx => dir = ctx.ReadValue<Vector2>();
        inputSystem.Player.Movement.canceled += ctx => dir = Vector2.zero;
        inputSystem.Player.Shoot.performed += ctx => Shoot();
        inputSystem.Player.Jump.performed += ctx => Jump();
        inputSystem.Player.CenterCamera.performed += ctx => CenterCamera();     }

    void Start()
    {
        gyroScript = cameraTransform.GetComponent<Giroscopio>();     }

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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = firePoint.forward * bulletSpeed;
        }
    }

        void CenterCamera()
    {
        if (gyroScript != null)
        {
            gyroScript.CenterToCustomRotation();
        }
    }

}