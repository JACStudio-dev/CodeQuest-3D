using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector2 moveInput;

    private PlayerInput playerInput;
    private InputAction moveAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogWarning("PlayerInput component not found. Add a PlayerInput component or generate the PlayerInputActions class.");
            return;
        }

        // Busca la acción llamada "Move" en el asset asignado al PlayerInput
        moveAction = playerInput.actions?["Move"];
        if (moveAction == null)
        {
            Debug.LogWarning("No se encontro la acción 'Move' en el PlayerInput.actions. Verifica el nombre y el asset de Input Actions.");
            return;
        }

        moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        if (moveAction != null) moveAction.Enable();
    }

    private void OnDisable()
    {
        if (moveAction != null) moveAction.Disable();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (movement != Vector3.zero)
        {
            transform.forward = movement;
        }
    }
}
