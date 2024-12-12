using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage = 10; // Damage value for this hitbox
    public Collider hitboxCollider; // Assign this in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hurtbox"))
        {
            Hurtbox hurtbox = other.GetComponent<Hurtbox>();
            if (hurtbox != null)
            {
                // Apply damage through the Hurtbox's logic.
            }
        }
    }

    public void ActivateHitbox(float duration)
    {
        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = true; // Enable the collider
            Invoke(nameof(DisableHitbox), duration); // Schedule disable
        }
        else
        {
            Debug.LogWarning("Hitbox Collider is not assigned!");
        }
    }

    private void DisableHitbox()
    {
        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = false; // Disable the collider
        }
    }

    private void OnDrawGizmos()
    {
        if (hitboxCollider != null && hitboxCollider is CapsuleCollider capsuleCollider)
        {
            // Set Gizmo color
            Gizmos.color = Color.red;

            // Get the world-space center of the CapsuleCollider
            Vector3 center = capsuleCollider.transform.TransformPoint(capsuleCollider.center);

            // Get radius and height of the capsule
            float radius = capsuleCollider.radius;
            float height = Mathf.Max(0, capsuleCollider.height - 2 * radius);

            // Get the direction of the capsule based on its orientation
            Vector3 upDirection = capsuleCollider.transform.up;

            // Calculate the top and bottom sphere positions
            Vector3 topSphere = center + upDirection * (height / 2);
            Vector3 bottomSphere = center - upDirection * (height / 2);

            // Draw the capsule in Scene view
            Gizmos.DrawWireSphere(topSphere, radius);  // Top sphere
            Gizmos.DrawWireSphere(bottomSphere, radius);  // Bottom sphere

            // Draw lines to simulate the cylindrical body
            Gizmos.DrawLine(topSphere + capsuleCollider.transform.right * radius, bottomSphere + capsuleCollider.transform.right * radius);
            Gizmos.DrawLine(topSphere - capsuleCollider.transform.right * radius, bottomSphere - capsuleCollider.transform.right * radius);
            Gizmos.DrawLine(topSphere + capsuleCollider.transform.forward * radius, bottomSphere + capsuleCollider.transform.forward * radius);
            Gizmos.DrawLine(topSphere - capsuleCollider.transform.forward * radius, bottomSphere - capsuleCollider.transform.forward * radius);
        }
    }
}