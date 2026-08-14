using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;

    public float distance = 10f;
    public float height = 8f;

    public float rotationSpeed = 5f;
    public float returnSpeed = 3f;

    //private Vector3 offset;
    //private Vector3 originalOffset;

    private float rotacionHorizontal = 0f;
    private float rotacionVertical = 0f;

    void Start()
    {
        //offset = new Vector3(0, height, -distance);
        //originalOffset = offset;

        rotacionHorizontal = 0f;
        rotacionVertical = 35f;

        MovimientoCamara();
    }

    void LateUpdate()
    {
        // Si el juego está pausado, no procesamos rotaciones ni actualización
        if (Time.timeScale == 0f) return;

        if (target == null)
        {
            Debug.Log("no esta el dev", target);
            return;
        }

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            rotacionHorizontal += mouseX * rotationSpeed;
            rotacionVertical -= mouseY * rotationSpeed;
            rotacionVertical = Mathf.Clamp(rotacionVertical, 10f, 80f);
        }

        MovimientoCamara();
    }

    void MovimientoCamara()
    {
        Quaternion rotacion = Quaternion.Euler(rotacionVertical, rotacionHorizontal, 0);
        Vector3 offset = rotacion * new Vector3(0f, 0f, -distance);

        transform.position = target.position + offset;

        transform.LookAt(target);
    }
}
