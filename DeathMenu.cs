using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public HealthManager healthManager;
    public GameObject loseUI;

    void Update()
    {
        if ( healthManager.health == 0 )
        {
           loseUI.SetActive(true); 
        }
    }
}
