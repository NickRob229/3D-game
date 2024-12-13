using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI enemyCountText; // Reference to the TextMeshPro UI element
    public TextMeshProUGUI killCountText;  // Reference to the TextMeshPro UI element for kills

    [Header("Update Settings")]
    public float updateInterval = 0.5f; // How often to update the enemy count (in seconds)

    private int killCount = 0; // Tracks the number of enemies killed

    public static EnemyCounter Instance { get; private set; } // Singleton instance

    void Awake()
    {
        // Ensure there is only one instance of EnemyCounter
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Start the update routine
        StartCoroutine(UpdateEnemyCountRoutine());
    }

    IEnumerator UpdateEnemyCountRoutine()
    {
        while (true)
        {
            UpdateEnemyCount();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    void UpdateEnemyCount()
    {
        // Find all GameObjects with the tag "Enemy"
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // Update the enemy count UI
        if (enemyCountText != null)
        {
            enemyCountText.text = $"Enemies: {enemyCount}";
        }

        // Update the kill count UI
        if (killCountText != null)
        {
            killCountText.text = $"Kills: {killCount}";
        }
    }

    public void IncrementKillCount()
    {
        killCount++;
    }
}
