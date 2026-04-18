using UnityEngine;

public class FinishedRounds : MonoBehaviour
{
    public WaveManager waveManager;
    public GameObject winUI;

    private bool hasWon = false;

    void Start()
    {
        if (waveManager == null)
            waveManager = FindObjectOfType<WaveManager>();
    }

    void Update()
    {
        if (!hasWon && waveManager.currentWaveIndex == 5)
        {
            hasWon = true;
            winUI.SetActive(true);
        }
    }
}
