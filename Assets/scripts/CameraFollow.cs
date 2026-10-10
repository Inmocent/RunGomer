using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // your cart

    public float distance = 6f;
    public float height = 2f;
    public float mouseSensitivity = 200f;

    private float yaw = 0f;   // left/right rotation
    private float pitch = 20f; // up/down rotation

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock cursor
    }

    void LateUpdate()
    {
        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;

        // Clamp vertical rotation
        pitch = Mathf.Clamp(pitch, -10f, 60f);

        // Rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Position (offset behind target)
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        Vector3 desiredPosition = target.position + offset + Vector3.up * height;

        transform.position = desiredPosition;

        // Always look at the cart
        transform.LookAt(target.position + Vector3.up * 1f);
    }
}