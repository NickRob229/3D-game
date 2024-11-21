using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public ParticleSystem slashVFXPrefab; // Drag your Particle System prefab here
    public Transform slashOrigin;  // The position and rotation where the slash starts (e.g., near the sword)
    public float slashDuration = 1f;  // Duration for the slash effect to play (in seconds)
    public float slashDamage = 10f; // Damage dealt by the slash
    public float attackCooldown = 0.5f; // Cooldown duration between attacks (in seconds)
    private float attackCooldownTimer = 0f; // Timer to track cooldown

    private ParticleSystem currentSlash; // To store the currently active slash
    private Collider slashCollider; // Reference to the collider on the slash clone
    private ParticleSystem slashClone; // Declare slashClone at the class level

    private bool isAttacking = false; // Flag to track if the player is already attacking
    private bool canAttack = true; // Flag to track if the player can attack (after cooldown)

    private void Update()
    {
        // Decrease the cooldown timer as time passes
        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        // Only allow attacking if the cooldown has passed, and the player is allowed to attack
        if (Input.GetMouseButtonDown(0) && canAttack && !isAttacking)
        {
            PerformAttack();
            attackCooldownTimer = attackCooldown; // Reset the cooldown timer after an attack
            isAttacking = true; // Flag that an attack is ongoing
            canAttack = false; // Prevent further attacks until the cooldown is over
        }

        // If the cooldown timer is finished, allow for another attack
        if (attackCooldownTimer <= 0f)
        {
            canAttack = true;
        }

        // If the slash is playing, update its position to match the player's position
        if (currentSlash != null && currentSlash.isPlaying)
        {
            currentSlash.transform.position = slashOrigin.position;
            currentSlash.transform.rotation = slashOrigin.rotation;
        }
    }

    private void PerformAttack()
    {
        if (slashVFXPrefab != null)
        {
            // Create a clone of the slash effect at the slash origin position
            slashClone = Instantiate(slashVFXPrefab, slashOrigin.position, slashOrigin.rotation);

            // Stop it immediately to prevent automatic looping (if Play On Awake is enabled in the prefab)
            slashClone.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            // Play the effect for the specified duration
            slashClone.Play();

            // Add a collider to the slash clone to detect collisions
            slashCollider = slashClone.gameObject.AddComponent<BoxCollider>();
            slashCollider.isTrigger = true; // Make it a trigger to detect collisions without physics

            // Set the collider size and position to match the slash area
            slashCollider.transform.position = slashOrigin.position;
            slashCollider.transform.rotation = slashOrigin.rotation;

            // Destroy the clone after the slash duration to stop it from lingering
            Destroy(slashClone.gameObject, slashDuration);

            // Disable the collider once the slash effect is done
            Invoke("DisableCollider", slashDuration);
        }
        else
        {
            Debug.LogWarning("Slash VFX Prefab is not assigned in the PlayerAttack script!");
        }

        // Store the current slash to track its position
        currentSlash = slashClone;
    }

    private void DisableCollider()
    {
        // Disable the collider after the slash effect is done
        if (slashCollider != null)
        {
            slashCollider.enabled = false;
        }

        // Reset the attacking flag so that the player can attack again
        isAttacking = false;
    }

    // Collision detection method
    private void OnTriggerEnter(Collider other)
    {
        // If the slash collides with the enemy
        if (other.CompareTag("Enemy")) // Make sure your enemy has the "Enemy" tag
        {
            // Debug log to confirm collision with the enemy
            Debug.Log("Slash hit an enemy: " + other.name);

            // Call the TakeDamage method on the enemy (make sure your enemy has this script)
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(slashDamage); // Apply damage to the enemy
            }
        }
    }
}
