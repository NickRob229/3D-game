using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;       // Normal movement speed
    public float sprintSpeed = 10f;    // Sprinting speed
    public float rotationSpeed = 10f; // Smooth rotation speed
    public float groundCheckDistance = 0.3f;  // Distance for ground checking

    private Rigidbody rb;
    private Camera cam;

    private Vector3 movementDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        // Lock the Rigidbody's rotation to prevent tipping
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Get player input (WASD or arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Calculate movement direction relative to the camera
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        // Remove any Y-axis component of the forward and right vectors to avoid unintended tilt
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Combine the forward and right vectors with player input
        movementDirection = forward * moveZ + right * moveX;

        // Normalize movement direction to prevent faster diagonal movement
        if (movementDirection.magnitude > 1f)
            movementDirection.Normalize();

        // Move the player (velocity-based movement)
        MovePlayer();

        // Rotate the player towards the movement direction
        RotatePlayer();
    }

    private void MovePlayer()
    {
        // Determine speed (whether sprinting or walking)
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        // Update Rigidbody's velocity based on the movement direction and speed
        rb.linearVelocity = new Vector3(movementDirection.x * speed, rb.linearVelocity.y, movementDirection.z * speed);

        // Simple ground check to ensure the player is grounded and prevent falling over
        if (IsGrounded() && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            // Correct the player's rotation to stand upright if on the ground
            if (transform.rotation != Quaternion.identity)
            {
                transform.rotation = Quaternion.identity;
            }
        }
    }

    private void RotatePlayer()
    {
        if (movementDirection.magnitude > 0.1f)
        {
            // Calculate the desired rotation (only on the Y-axis)
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

            // Smoothly rotate the player towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private bool IsGrounded()
    {
        // Perform a simple raycast downward to check if the player is grounded
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}
