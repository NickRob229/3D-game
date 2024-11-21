using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;  // Enemy's health

    // Method to handle taking damage
    public void TakeDamage(float damage)
    {
        health -= damage;  // Subtract damage from health

        if (health <= 0f)
        {
            Die();  // Call die method if health is zero or below
        }
    }

    // Method to handle the enemy's death
    private void Die()
    {
        // You can add any death behavior here (e.g., play death animation, destroy object, etc.)
        Debug.Log("Enemy died!");
        Destroy(gameObject);  // Destroy the enemy object (just for testing)
    }
}
