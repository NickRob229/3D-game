using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Collider hitboxCollider; // Assign the hitbox (Collider) in the Inspector
    public ParticleSystem slashVFXPrefab; // Drag your Particle System prefab here
    public Transform slashOrigin; // Position and rotation for the VFX (e.g., near the sword)
    public Animator swordAnimator; // Reference to the Animator on the sword
    public float attackDuration = 0.5f; // Duration for which the hitbox is active
    public float attackInterval = 1f; // Interval between automatic attacks
    private float attackTimer = 0f;

    private ParticleSystem currentSlash; // To track the active VFX instance

    private void Start()
    {
        if (swordAnimator == null)
        {
            Debug.LogError("Sword Animator is not assigned! Please assign it in the Inspector.");
        }

        if (hitboxCollider != null)
        {
            hitboxCollider.enabled = false; // Ensure the hitbox is initially disabled
        }
    }

    private void Update()
    {
        // Decrease attack timer
        attackTimer -= Time.deltaTime;

        // If the timer reaches 0 or below, perform an attack
        if (attackTimer <= 0f)
        {
            PerformAttack();
            attackTimer = attackInterval; // Reset attack timer to the attack interval
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
        // Trigger attack animation on the sword
        if (swordAnimator != null)
        {
            swordAnimator.SetTrigger("Attack"); // Assumes an "Attack" trigger is set up in the Animator
        }

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
            Debug.LogWarning("Slash VFX Prefab is not assigned in the EnemyAttack script!");
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
