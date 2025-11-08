using UnityEngine;

public class Movement : MonoBehaviour
{
    // Public variables allow us to adjust these values in the Unity Inspector.

    // 1. Default movement speed for walking (A good standard value).
    public float walkSpeed = 2.0f;

    // 2. Sprint speed when the Shift key is pressed (Usually double the walk speed).
    public float runSpeed = 5.0f;

    // Update is called once per frame and is used for continuous input checks.
    void Update()
    {
        // Variable to hold the speed that will be applied in the current frame.
        float currentSpeed;

        // --- 3. Determine Current Speed (Walk or Run) ---

        // Input.GetKey returns 'True' as long as the key is being held down.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // If Left Shift is pressed, use the run speed.
            currentSpeed = runSpeed;
        }
        else
        {
            // If Left Shift is NOT pressed, use the walk speed.
            currentSpeed = walkSpeed;
        }

        // --- 4. Read Movement Inputs ---

        // GetAxis reads smoothed input values between -1 and 1.
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow keys
        float verticalInput = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow keys

        // --- 5. Calculate Movement Vector ---

        // Create a direction vector based on inputs (X and Z axes for 3D ground movement).
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);

        // --- 6. Apply Speed and Frame Rate Compensation ---

        // Multiply direction by the chosen speed and Time.deltaTime.
        // Time.deltaTime ensures movement is framerate-independent (consistent across all PCs).
        movement = movement * currentSpeed * Time.deltaTime;

        // --- 7. Move the Object ---

        // transform.Translate moves the GameObject relative to its current position.
        transform.Translate(movement);
    }
}
