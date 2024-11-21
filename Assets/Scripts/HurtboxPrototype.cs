using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Hurtbox : MonoBehaviour
{
    public CharacterStats characterStats; // Reference to the character's health, etc.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hitbox"))
        {
            Hitbox hitbox = other.GetComponent<Hitbox>();
            if (hitbox != null)
            {
                characterStats.TakeDamage(hitbox.damage);
            }
        }
    }
}
