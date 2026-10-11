using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCharacter : MonoBehaviour
{


    [SerializeField] private float velocidad = 5f;

    private void OnMouseDrag()
    {
        float movimientoMouse = Mouse.current.delta.ReadValue().x;

        transform.Rotate(
            0f,
            -movimientoMouse * velocidad,
            0f
        );
    }
}
