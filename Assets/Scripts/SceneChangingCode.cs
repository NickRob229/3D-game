using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Method to load a scene by name
    public void ChangeScene(string sceneName)
    {
        // Make sure the scene exists in the build settings
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene " + sceneName + " not found!");
        }
    }

    // Method to load a scene by index (useful if you prefer to use scene index numbers)
    public void ChangeSceneByIndex(int sceneIndex)
    {
        // Make sure the scene index is valid
        if (sceneIndex >= 0 && sceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.LogError("Invalid scene index!");
        }
    }

    // Method to quit the game
    public void QuitGame()
    {
        // If running in the Unity Editor, stop playing the game
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If built game, quit the application
            Application.Quit();
        #endif
    }
}
