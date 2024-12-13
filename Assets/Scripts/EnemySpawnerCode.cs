using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject prefabToSpawn; // The prefab to spawn
    public int maxSpawnCount = 10; // Maximum number of objects to spawn
    public float spawnInterval = 2f; // Time interval between spawns

    [Header("Spawn Area Settings")]
    public Vector3 spawnAreaSize = new Vector3(10, 1, 10); // Size of the spawn area
    public bool spawnInRandomPosition = true; // Whether to spawn at random positions within the area

    [Header("Spawn Points")]
    public Transform[] spawnPoints; // Optional specific spawn points

    [Header("References")]
    public Transform player; // Reference to the player for AI to follow

    private int spawnCount = 0; // Counter for spawned objects

    void Start()
    {
        // Start spawning objects
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (spawnCount < maxSpawnCount)
        {
            SpawnObject();
            spawnCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        Vector3 spawnPosition;

        // Determine spawn position
        if (spawnPoints != null && spawnPoints.Length > 0 && !spawnInRandomPosition)
        {
            // Select a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            spawnPosition = spawnPoint.position;
        }
        else
        {
            // Generate a random position within the spawn area
            spawnPosition = transform.position + new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );
        }

        GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // Tag the spawned object as "Enemy"
        spawnedObject.tag = "Enemy";

        // Ensure NavMeshAgent is initialized
        NavMeshAgent agent = spawnedObject.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.Warp(spawnPosition);
        }

        // Assign the player to the AIFollowPlayer script
        AIFollowPlayer aiFollow = spawnedObject.GetComponent<AIFollowPlayer>();
        if (aiFollow != null && player != null)
        {
            aiFollow.player = player;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the spawn area in the editor for visualization
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}