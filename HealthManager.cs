using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{   
    public static HealthManager healthInstance;

    public int health = 15;
    public TextMeshProUGUI healthText;

    private void Awake()
    {
        healthInstance = this;
        Updatehealth(0);
    }

    public void Updatehealth(int changeAmount )
    {
        health += changeAmount;
        healthText.text = health.ToString();

        if ( health <= 0 )
        {
            Time.timeScale = 1f;
            Enemy.activeCount = 0;
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
    }

}
