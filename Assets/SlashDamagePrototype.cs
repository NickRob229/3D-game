using UnityEngine;

public class SlashDamage : MonoBehaviour
{
    public int damage = 10;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Ensure enemies are tagged as "Enemy"
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
