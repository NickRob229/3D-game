using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the inspector
    public float speed = 2f; // Adjust the speed in the inspector

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            // Rotate to face the player
            transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            // Move towards the player
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
