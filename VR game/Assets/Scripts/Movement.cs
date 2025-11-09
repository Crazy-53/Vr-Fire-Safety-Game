using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private float currentSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Adjust speed based on whether the player is sprinting or walking
        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = sprintSpeed;
        else
            currentSpeed = walkSpeed;

        // Get movement input (WASD)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Determine movement direction relative to player orientation
        Vector3 move = transform.right * x + transform.forward * z;

        // Apply movement
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Apply gravity to keep the player grounded
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}