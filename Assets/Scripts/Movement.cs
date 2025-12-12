using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 5f;          // Normal walking speed
    public float sprintSpeed = 9f;        // Speed when holding Shift
    public float mouseSensitivity = 2f;   // Mouse sensitivity for looking around
    public float gravity = -9.81f;        // Gravity for the player
    public float jumpHeight = 1.5f;       // Jump height (can keep or remove)

    public Transform playerCamera;        // Reference to player camera

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private float currentSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor in the middle
    }

    void Update()
    {
        HandleMovement();
        LookAround();
    }

    void HandleMovement()
    {
        // Check if Shift is held for sprinting
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        // Get movement input (WASD)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * x + transform.forward * z;

        // Move the player horizontally
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Apply gravity
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f; // Keep player grounded

        // Jump (optional, can remove if not needed)
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        // Move player vertically with gravity
        controller.Move(velocity * Time.deltaTime);
    }

    void LookAround()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate camera up/down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // Limit camera rotation

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate player left/right
        transform.Rotate(Vector3.up * mouseX);
    }
}