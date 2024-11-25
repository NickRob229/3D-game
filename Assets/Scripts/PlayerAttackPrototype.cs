using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider hitboxCollider; // Assign the hitbox (CapsuleCollider) in the Inspector
    public ParticleSystem slashVFXPrefab; // Drag your Particle System prefab here
    public Transform slashOrigin; // Position and rotation for the VFX (e.g., near the sword)
    public float attackDuration = 0.5f; // Duration for which the hitbox is active
    public float attackCooldown = 1f; // Cooldown duration between attacks
    private float attackCooldownTimer = 0f;

    private ParticleSystem currentSlash; // To track the active VFX instance

    private void Start()
    {
        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = false; // Ensure the hitbox is initially disabled
        }
    }

    private void Update()
    {
        // Decrease cooldown timer
        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        // Trigger attack if left mouse button is clicked and cooldown is over
        if (Input.GetMouseButtonDown(0) && attackCooldownTimer <= 0f)
        {
            PerformAttack();
            attackCooldownTimer = attackCooldown; // Reset cooldown timer
        }

        // Update VFX position if it's playing
        if (currentSlash != null && currentSlash.isPlaying)
        {
            currentSlash.transform.position = slashOrigin.position;
            currentSlash.transform.rotation = slashOrigin.rotation;
        }
    }

    private void PerformAttack()
    {
        if (hitboxCollider != null)
        {
            // Activate hitbox for the attack duration
            hitboxCollider.enabled = true;
            Invoke(nameof(DisableHitbox), attackDuration);
        }

        // Play slash VFX
        if (slashVFXPrefab != null)
        {
            // Instantiate the VFX at the slash origin
            currentSlash = Instantiate(slashVFXPrefab, slashOrigin.position, slashOrigin.rotation);

            // Stop it immediately to prevent automatic looping (if "Play On Awake" is enabled)
            currentSlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            // Play the VFX for the duration of the attack
            currentSlash.Play();

            // Destroy the VFX after the attack duration
            Destroy(currentSlash.gameObject, attackDuration);
        }
        else
        {
            Debug.LogWarning("Slash VFX Prefab is not assigned in the PlayerAttack script!");
        }
    }

    private void DisableHitbox()
    {
        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = false;
        }
    }
}
