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
        int savedScene = SaveManager.GetSavedSceneIndex();
        if (savedScene == -1) return; // no save found, do nothing

        PlayerPrefs.SetInt("PendingLoad", 1);
        SceneManager.LoadScene(savedScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}