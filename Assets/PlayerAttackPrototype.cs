using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public ParticleSystem slashVFX; // Drag your Particle System here
    public Transform slashOrigin;  // The position and rotation where the slash starts (e.g., near the sword)

    private void Update()
    {
        // Trigger the attack on left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        // Ensure the slash VFX plays from the correct position and rotation
        if (slashVFX != null)
        {
            // Reset the particle effect position and rotation
            slashVFX.transform.position = slashOrigin.position;
            slashVFX.transform.rotation = slashOrigin.rotation;

            // Stop and restart the particle effect to ensure no looping
            if (slashVFX.isPlaying)
            {
                slashVFX.Stop();
            }

            slashVFX.Play();
        }
        else
        {
            Debug.LogWarning("Slash VFX is not assigned in the PlayerAttack script!");
        }
    }
}
