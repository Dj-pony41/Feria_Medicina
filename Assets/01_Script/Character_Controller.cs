using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public float speed = 4f;
    Vector2 dir = Vector2.zero;
    public Rigidbody rb;
    NewInputSystem inputSystem;

    void Awake()
    {
        inputSystem = new NewInputSystem();

        inputSystem.Player.Movement.performed += ctx => dir = ctx.ReadValue<Vector2>();
        inputSystem.Player.Movement.canceled += ctx => dir = Vector2.zero;
        inputSystem.Player.Shoot.performed += ctx => Shoot();

        //inputSystem.Player.Shoot.started += ctx => shooting = true;
        //inputSystem.Player.Shoot.canceled += ctx => shooting = false;

    }
    void OnEnable()
    {
        inputSystem.Enable();
    }
    void OnDisable()
    {
        inputSystem.Disable();
    }
    void Start()
    {
        
    }

    void Update()
    {
        Movement();
    }
    void Movement()
    {
        rb.velocity = new Vector3(dir.x * speed, 0, dir.y * speed);
    }
    void Shoot()
    {

    }
}