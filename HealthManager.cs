using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{   
    public static HealthManager healthInstance;

    public static bool isGameOver = false;

    public int health = 12;
    public TextMeshProUGUI healthText;

    private void Awake()
    {
        healthInstance = this;
        isGameOver = false;
        Updatehealth(0);
    }

    public void Updatehealth(int changeAmount )
    {
        health += changeAmount;
        healthText.text = health.ToString();

        if ( health <= 0 )
        {
            isGameOver = true;
            Time.timeScale = 1f;
            Enemy.activeCount = 0;
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
    }

}
