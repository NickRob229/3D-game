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
        // Visualize the hitbox in the editor
        Gizmos.color = Color.red;
        if (hitboxCollider != null && hitboxCollider is BoxCollider boxCollider)
        {
            Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.size);
        }
    }
}
