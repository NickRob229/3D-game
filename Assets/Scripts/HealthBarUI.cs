using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider healthSlider; // Reference to the UI Slider
    public CharacterStats characterStats; // Reference to the player's stats

    private void Start()
    {
        if (characterStats != null)
        {
            healthSlider.maxValue = characterStats.maxHealth;
            healthSlider.value = characterStats.currentHealth;
        }
    }

    private void Update()
    {
        if (characterStats != null)
        {
            // Update the slider value based on current health
            healthSlider.value = characterStats.currentHealth;
        }
    }
}
