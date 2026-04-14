using UnityEngine;
using TMPro;

public class CoinsUI : MonoBehaviour
{
    public static CoinsUI instance;
    public TMP_Text coinsText;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateCoins(int currentCoins)
    {
        if (coinsText != null)
            coinsText.text = "Coins: " + currentCoins;
    }

    private void Start()
    {
        UpdateCoins(GameManager.instance.coinsCollected);
    }
}