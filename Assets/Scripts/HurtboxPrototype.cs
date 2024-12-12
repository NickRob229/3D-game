using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public CharacterStats characterStats; // Reference to the character's health or stats
    private bool canTakeDamage = true; // Prevent multiple damage triggers
    public float damageCooldown = 0.5f; // Cooldown time between damage applications
    public bool isPlayer; // Set to true if this hurtbox belongs to the player
    public SceneChanger sceneChanger; // Reference to SceneChanger for player death handling
    public string deathSceneName = "DeathScreen"; // Name of the death screen scene

    private void OnTriggerEnter(Collider other)
    {
        if (!canTakeDamage) return; // Ignore if damage cooldown is active

        if (other.CompareTag("Hitbox")) // Ensure it's a valid hitbox
        {
            Hitbox hitbox = other.GetComponent<Hitbox>();
            if (hitbox != null && characterStats != null)
            {
                characterStats.TakeDamage(hitbox.damage);

                // Temporarily prevent further damage
                canTakeDamage = false;
                Invoke(nameof(ResetDamage), damageCooldown);

                // Check if character is dead
                if (characterStats.currentHealth <= 0)
                {
                    HandleDeath();
                }
            }
        }
    }

    private void ResetDamage()
    {
        canTakeDamage = true; // Allow damage again after cooldown
    }

    private void HandleDeath()
    {
        Debug.Log($"{gameObject.name} has died!");

        if (isPlayer)
        {
            // Player-specific death handling
            if (sceneChanger != null)
            {
                sceneChanger.ChangeScene(deathSceneName);
            }
            else
            {
                Debug.LogError("SceneChanger not assigned!");
            }
        }
        else
        {
            // Enemy-specific death handling
            Destroy(transform.parent.gameObject); // Destroy the enemy (parent object)
        }
    }
}
