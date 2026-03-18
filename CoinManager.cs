using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{

    public static CoinManager instance;

    public int coins;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        instance = this;
        UpdateCoins(0);
    }

    public void UpdateCoins( int changeAmt )
    {
        coins += changeAmt;
        coinText.text = coins.ToString();
    }
}
