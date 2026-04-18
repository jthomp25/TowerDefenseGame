using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    void Awake()
    {
        instance = this;
    }

    public void SaveGame()
    {
        GameSaveData data = new GameSaveData
        {
            coins = CoinManager.instance.coins,
            health = HealthManager.healthInstance.health,
            waveIndex = FindObjectOfType<WaveManager>().currentWaveIndex,
            sceneIndex = SceneManager.GetActiveScene().buildIndex, // save which level we're on
            towers = TowerPlacer.instance.GetTowerSaveData() // ADD THIS
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
        Debug.Log("Game Saved: " + json);
    }

    public void ApplyPendingLoad()
    {
        Debug.Log("ApplyPendingLoad called, PendingLoad = " + PlayerPrefs.GetInt("PendingLoad", 0));
        if (PlayerPrefs.GetInt("PendingLoad", 0) != 1) return;

        PlayerPrefs.DeleteKey("PendingLoad");

        if (!PlayerPrefs.HasKey("SaveData"))
        {
            Debug.LogWarning("PendingLoad was set but no SaveData found!");
            return;
        }

        string json = PlayerPrefs.GetString("SaveData");
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);

        CoinManager.instance.coins = 0;
        CoinManager.instance.UpdateCoins(data.coins);

        HealthManager.healthInstance.health = data.health;
        HealthManager.healthInstance.healthText.text = data.health.ToString();

        WaveManager wm = FindObjectOfType<WaveManager>();
        wm.currentWaveIndex = data.waveIndex;
        wm.waveText.text = (data.waveIndex + 1).ToString();

        TowerPlacer.instance.RestoreTowers(data.towers); // ADD THIS

        Debug.Log("Game Loaded: " + json);
    }

    public static int GetSavedSceneIndex()
    {
        if (!PlayerPrefs.HasKey("SaveData")) return -1;
 
        string json = PlayerPrefs.GetString("SaveData");
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);
        return data.sceneIndex;
    }

    public bool HasSave()
    {
        return PlayerPrefs.HasKey("SaveData");
    }
}