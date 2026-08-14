using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(LineRenderer))]
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //variable para que de forma rapida ajustar la velocidad en la interfaz
    public float VelocidadRotatacion = 100f;

    Vector3 velocity;
    bool isGrounded;

    private LineRenderer line;
    private List<Vector3> points = new List<Vector3>();

    public float minDistance = 0.5f;
    public int maxPoints = 100;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        line = GetComponent<LineRenderer>();
    }

    void Start()
    {
        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck faltante");
            enabled = false;
        }

        if (line == null)
        {
            Debug.LogError("LineRenderer faltante");
            enabled = false;
        }
    }

    void Update()
    {
        // Si está pausado, no procesamos movimiento ni físicas
        if (Time.timeScale == 0f) return;

        HandleGround();
        HandleMovement();
        HandleJump();
        ApplyGravity();
        DrawTrail();
    }

    void HandleGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //rota el jugador tomando encuenta la variable de velocidad de rotacion
        transform.Rotate(Vector3.up * x * VelocidadRotatacion * Time.deltaTime);

        Vector3 move = transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void DrawTrail()
    {
        if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], transform.position) > minDistance)
        {
            points.Add(transform.position);

            if (points.Count > maxPoints)
            {
                points.RemoveAt(0);
            }

            line.positionCount = points.Count;
            line.SetPositions(points.ToArray());
        }
    }
}
