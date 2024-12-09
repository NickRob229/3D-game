using UnityEngine;
using UnityEngine.AI;

public class AIFollowPlayer : MonoBehaviour
{
    private NavMeshAgent navAgent;
    public Transform player;  // The target player to follow

    public float rotationSpeed = 5f;  // Speed at which the AI rotates to face the player
    public float stoppingDistance = 2.0f;  // The distance at which the AI stops following the player

    void Start()
    {
        // Get the NavMeshAgent component
        navAgent = GetComponent<NavMeshAgent>();

        // Make sure the agent's rotation is handled by us
        navAgent.updateRotation = false;
    }

    void Update()
    {
        if (player != null)
        {
            // Set the destination to the player's position
            navAgent.SetDestination(player.position);

            // Smoothly rotate to face the player
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;  // Keep the AI on the same height (no tilting up/down)

            if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
            {
                // Rotate smoothly to face the player
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                // Stop moving when close enough
                navAgent.velocity = Vector3.zero;
            }
        }
    }
}
