using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public ParticleSystem slashVFX; // Drag your Particle System here
    public Transform slashOrigin;  // The position and rotation where the slash starts (e.g., near the sword)
    private float slashDuration = 2f; // Duration for the slash effect to play
    private float slashTime = 0f; // Timer to track the slash effect

    private void Update()
    {
        // Trigger attack on left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            PerformAttack();
        }

        // Check if the slash is playing and stop it after 2 seconds
        if (slashVFX.isPlaying && slashTime < slashDuration)
        {
            slashTime += Time.deltaTime;
        }
        else if (slashTime >= slashDuration)
        {
            slashVFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            slashTime = 0f; // Reset the timer after the slash effect ends
        }
    }

    private void PerformAttack()
    {
        if (slashVFX != null)
        {
            // Reset particle system position and rotation
            slashVFX.transform.position = slashOrigin.position;
            slashVFX.transform.rotation = slashOrigin.rotation;

            // Force stop the Particle System in case it's still playing from previous slash
            if (slashVFX.isPlaying)
            {
                slashVFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }

            // Play the Particle System for 2 seconds
            slashVFX.Play();
            slashTime = 0f; // Reset the timer
        }
        else
        {
            Debug.LogWarning("Slash VFX is not assigned in the PlayerAttack script!");
        }
    }
}
