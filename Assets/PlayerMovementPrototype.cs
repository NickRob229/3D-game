using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;       // Normal movement speed
    public float sprintSpeed = 10f;   // Sprinting speed
    public float dashSpeed = 20f;     // Dash speed
    public float dashDuration = 0.2f; // Duration of the dash
    public float dashCooldown = 1f;   // Time before the dash can be used again
    public float rotationSpeed = 10f; // Smooth rotation speed
    public float groundCheckDistance = 0.3f; // Distance for ground checking

    private Rigidbody rb;
    private Camera cam;

    private Vector3 movementDirection;
    private Vector3 dashDirection;      // The direction the player dashes towards
    private bool isDashing = false;     // Whether the player is currently dashing
    private float dashTime = 0f;        // Timer to keep track of dash duration
    private float dashCooldownTime = 0f; // Timer to track cooldown between dashes

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        // Lock the Rigidbody's rotation to prevent tipping
        rb.freezeRotation = true;
    }

    private void Update()
    {
        // Handle dash input (space key)
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTime <= 0f && !isDashing)
        {
            StartDash();
        }

        // Handle movement input (WASD or arrow keys)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Calculate movement direction relative to the camera
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        // Remove any Y-axis component of the forward and right vectors
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Combine forward and right vectors with player input
        movementDirection = forward * moveZ + right * moveX;

        // Normalize movement direction to prevent faster diagonal movement
        if (movementDirection.magnitude > 1f)
            movementDirection.Normalize();

        // Move the player (velocity-based movement)
        MovePlayer();

        // Rotate the player to face the mouse pointer
        RotateToMouse();

        // Update dash state
        UpdateDash();
    }

    private void MovePlayer()
    {
        // Determine the current speed (whether sprinting, normal, or dashing)
        float speed = isDashing ? dashSpeed : (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed);

        // If dashing, use the dash direction; otherwise, use movement input
        Vector3 velocityDirection = isDashing ? dashDirection : movementDirection;

        // Update Rigidbody's velocity
        rb.linearVelocity = new Vector3(velocityDirection.x * speed, rb.linearVelocity.y, velocityDirection.z * speed);
    }

    private void RotateToMouse()
    {
        // Cast a ray from the camera to the mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Detect the ground or plane
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 targetPosition = hitInfo.point; // Mouse hit position
            Vector3 direction = (targetPosition - transform.position).normalized;

            // Ignore Y-axis for rotation
            direction.y = 0f;

            if (direction.magnitude > 0.1f)
            {
                // Calculate and smoothly rotate to the target direction
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashTime = dashDuration;

        // Get the direction the player is facing
        dashDirection = transform.forward;

        // Start dash cooldown
        dashCooldownTime = dashCooldown;
    }

    private void UpdateDash()
    {
        if (isDashing)
        {
            dashTime -= Time.deltaTime;

            // End the dash after the duration
            if (dashTime <= 0f)
            {
                isDashing = false;
            }
        }

        if (dashCooldownTime > 0f)
        {
            dashCooldownTime -= Time.deltaTime;
        }
    }

    private bool IsGrounded()
    {
        // Perform a raycast downward to check if the player is grounded
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }
}
