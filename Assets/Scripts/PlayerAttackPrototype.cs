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
    private Collider playerCollider; // Reference to the player's collider
    private ParticleSystem slashClone; // Declare slashClone at the class level

    private void Start()
    {
        // Get the player's collider (assuming it's the parent of the Slash8 GameObject)
        playerCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        // Decrease the cooldown timer as time passes
        if (attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        // Trigger attack on left mouse button click if the cooldown has passed
        if (Input.GetMouseButtonDown(0) && attackCooldownTimer <= 0f)
        {
            PerformAttack();
            attackCooldownTimer = attackCooldown; // Reset the cooldown timer after an attack
        }

        // If the slash is playing, update its position to match the player's position
        if (currentSlash != null && currentSlash.isPlaying)
        {
            // Update the position of the slash clone to match the slash origin
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

            // Destroy the clone after the slash duration to stop it from lingering
            Destroy(slashClone.gameObject, slashDuration);
        }
        else
        {
            Debug.LogWarning("Slash VFX Prefab is not assigned in the PlayerAttack script!");
        }

        // Store the current slash to track its position
        currentSlash = slashClone;
    }

    // Collision detection method (detecting with the player's collider)
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

    // Optional: You can call this method to disable the collider if necessary, but using the player's collider should suffice.
    private void DisableCollider()
    {
        if (playerCollider != null)
        {
            playerCollider.enabled = false; // Temporarily disable the player's collider if needed
        }
    }
}
