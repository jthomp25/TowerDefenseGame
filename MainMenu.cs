using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject loadButton;

    void Start()
    {
        if (loadButton != null)
            loadButton.SetActive(PlayerPrefs.HasKey("SaveData"));
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("PendingLoad", 1);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}