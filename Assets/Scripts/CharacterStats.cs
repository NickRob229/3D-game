using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth; // Initialize health
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0; // Prevent health from dropping below zero
        }

        Debug.Log($"{gameObject.name} took {damage} damage, current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            // Notify Hurtbox (or other scripts) to handle death
            SendMessage("HandleDeath", SendMessageOptions.DontRequireReceiver);
        }
    }
}
