using UnityEngine;

public class OverheadCameraFollow : MonoBehaviour
{
    public Transform player;            // The player's transform to follow
    public Vector3 offset;             // The offset (position relative to player)
    public float smoothSpeed = 0.125f;  // Smoothness of the camera follow

    private void LateUpdate()
    {
        // Calculate the desired position based on the player's position and the fixed offset
        Vector3 desiredPosition = player.position + offset;

        // Smoothly interpolate between the current camera position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;

        // Optionally, you can make the camera look at the player to keep them centered
        transform.LookAt(player);
    }
}