using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;

    public float distance = 10f;
    public float height = 8f;
    public float rotationSpeed = 5f;
    public float returnSpeed = 3f;

    private Vector3 offset;
    private Vector3 originalOffset;

    void Start()
    {
        offset = new Vector3(0, height, -distance);
        originalOffset = offset;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Quaternion rotation = Quaternion.Euler(-mouseY * rotationSpeed, mouseX * rotationSpeed, 0);
            offset = rotation * offset;
        }
        else
        {
            offset = Vector3.Lerp(offset, originalOffset, returnSpeed * Time.deltaTime);
        }

        transform.position = target.position + offset;
        transform.LookAt(target);

        if (transform.position.y < 2f)
        {
            Vector3 pos = transform.position;
            pos.y = 2f;
            transform.position = pos;
        }
    }
}