using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene2");
    }
}
