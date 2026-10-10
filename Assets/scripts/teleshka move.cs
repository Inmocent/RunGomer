using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class teleshkamove : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 100f;
    public float acceleration = 5f;

    private Rigidbody rb;
    private float currentSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input
        float moveInput = Input.GetAxis("Vertical");   // W/S or Up/Down
        float turnInput = Input.GetAxis("Horizontal"); // A/D or Left/Right

        // Smooth acceleration
        float targetSpeed = moveInput * moveSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);

        // Move forward
        Vector3 move = transform.forward * currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        // Turn
        float turn = turnInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}