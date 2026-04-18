using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

[System.Serializable]

public class WaveData
{
    public float duration = 10f;
    public int easyEnemies = 5;
    public int hardEnemies = 2;
}

public class WaveManager : MonoBehaviour
{
    public WaveData[] waves;
    public Button startWaveButton;

    public GameObject easyEnemyPrefab;
    public GameObject hardEnemyPrefab;
    
    public Transform[] wayPoints;

    public TextMeshProUGUI waveText;

    public int currentWaveIndex = 0;
    private bool waveRunning = false;

    void Start()
    {
        startWaveButton.onClick.AddListener(StartWave);
    }

    public void StartWave()
    {
        if ( waveRunning ) return;
        if (currentWaveIndex >= waves.Length ) return;

        StartCoroutine(RunWave());
    }

    IEnumerator RunWave()
    {
        waveRunning = true;
        startWaveButton.interactable = false;

        WaveData wave = waves[currentWaveIndex];

        int numEasy = wave.easyEnemies; 
        int numHard = wave.hardEnemies;
        int largerEnemies = 0;

        if ( numEasy >= numHard ) {
            largerEnemies = numEasy;
        }
        else {
            largerEnemies = numHard;
        }

        for ( int i = 0; i < largerEnemies; i++ )
        {
            if (numEasy > 0) {
                SpawnEnemy(easyEnemyPrefab);
                numEasy--;
            }
            if (numHard > 0) {
                SpawnEnemy(hardEnemyPrefab);
                numHard--;
            }
            yield return new WaitForSeconds( 1 );
        }


        yield return new WaitUntil(() => Enemy.activeCount <= 0 );

        waveRunning = false;
        startWaveButton.interactable = true;
        currentWaveIndex++;
        waveText.text = (currentWaveIndex + 1).ToString();
    }

    void SpawnEnemy( GameObject prefab )
    {
        GameObject e = Instantiate(prefab, wayPoints[0].position, Quaternion.identity);
        Enemy enemy = e.GetComponent<Enemy>();
        enemy.waypoints = wayPoints;
    }

}
