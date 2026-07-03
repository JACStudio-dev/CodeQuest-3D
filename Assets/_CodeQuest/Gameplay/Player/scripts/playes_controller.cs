using UnityEngine;
using UnityEngine.InputSystem;

public class PlayesController : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 5f;
    public float gravedad = -9.81f;

    private CharacterController characterController;
    private Vector2 movimiento;

    private float velocidadVertical;
    private bool salto;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogWarning("CharacterController no encontrado en el GameObject.");
        }

        // Inicializar velocidad vertical con un pequeño valor negativo para garantizar que el CharacterController
        // detecte correctamente el suelo al inicio y no quede "flotando".
        velocidadVertical = -2f;
    }

    void Update()
    {
        bool isGrounded = characterController.isGrounded;

        if (isGrounded && velocidadVertical <= 0f)
        {
            velocidadVertical = -2f;
        }

        if (salto && isGrounded)
        {
            velocidadVertical = Mathf.Sqrt(fuerzaSalto * -2f * gravedad);
            salto = false;
        }

        velocidadVertical += gravedad * Time.deltaTime;

        Vector3 movimientoHorizontal = new Vector3(movimiento.x, 0f, movimiento.y) * velocidad;

        Vector3 desplazamiento = movimientoHorizontal + Vector3.up * velocidadVertical;

        characterController.Move(desplazamiento * Time.deltaTime);
    }

    public void Movio(InputValue valor)
    {
        movimiento = valor.Get<Vector2>();
    }
    
    public void Salto(InputValue valor)
    {
        if (valor.isPressed)
        {
            salto = true;

        }
    }

    // Compatibilidad con PlayerInput (Send Messages / Invoke Unity Events)
    // Cuando el PlayerInput usa Send Messages invoca métodos llamados "On<ActionName>".
    // Añadimos sobrecargas para InputValue y CallbackContext para cubrir los diferentes modos.
    public void OnMove(InputValue value) => Movio(value);

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            movimiento = ctx.ReadValue<Vector2>();
        else if (ctx.canceled)
            movimiento = Vector2.zero;
    }

    public void OnJump(InputValue value) => Salto(value);

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            salto = true;
    }
}
