using UnityEngine;

public class CamerLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public float smoothTime = 0.05f; // Higher values = slower but smoother movement
    public Transform playerBody;

    private float xRotation = 0f;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get raw mouse input
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Smoothly interpolate between the current and target mouse delta
        currentMouseDelta = Vector2.SmoothDamp(
            currentMouseDelta,
            targetMouseDelta,
            ref currentMouseDeltaVelocity,
            smoothTime
        );

        float mouseX = currentMouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = currentMouseDelta.y * mouseSensitivity * Time.deltaTime;

        // Adjust vertical rotation (look up/down)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotations
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}