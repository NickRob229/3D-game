using UnityEngine;

public class OverheadCameraFollow : MonoBehaviour
{
    public Transform player;            // The player's transform to follow
    public Vector3 offset;             // The offset (position relative to player)
    public float smoothSpeed = 0.125f;  // Smoothness of the camera follow
    public float rotationSpeed = 5f;    // Speed of camera rotation based on mouse X movement

    private float currentRotationX = 0f; // Current rotation around the X-axis (left/right)

    private void LateUpdate()
    {
        // Get mouse movement on the X axis
        float mouseX = Input.GetAxis("Mouse X");

        // Update the current rotation based on mouse X input
        currentRotationX += mouseX * rotationSpeed;

        // Clamp rotation to a specific range (optional)
        currentRotationX = Mathf.Clamp(currentRotationX, -50f, 50f);  // You can adjust these limits as needed

        // Calculate the desired position based on the player's position and the fixed offset
        Vector3 desiredPosition = player.position + offset;

        // Move the camera on the X-axis based on mouse movement, maintaining top-down view
        desiredPosition.x += mouseX * rotationSpeed;

        // Smoothly interpolate between the current camera position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;

        // Apply the rotation to the camera on the Y-axis (around the vertical axis)
        transform.rotation = Quaternion.Euler(currentRotationX, 0, 0); // This will keep the camera's top-down view

        // Optionally, you can make the camera look at the player to keep them centered
        transform.LookAt(player);
    }
}
