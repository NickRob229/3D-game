using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    [Header("UI Settings")]
    public TextMeshProUGUI enemyCountText; // Reference to the TextMeshPro UI element

    [Header("Update Settings")]
    public float updateInterval = 0.5f; // How often to update the enemy count (in seconds)

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

        // Update the TextMeshPro UI element
        if (enemyCountText != null)
        {
            enemyCountText.text = $"Enemies: {enemyCount}";
        }
    }
}
