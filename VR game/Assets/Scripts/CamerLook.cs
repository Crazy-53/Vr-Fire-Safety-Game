using UnityEngine;

public class CamerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Optional: only used if you want X rotation to affect player height

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical rotation only
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // DO NOT rotate playerBody horizontally
        // playerBody.Rotate(Vector3.up * mouseX);  <-- remove this line
    }
}