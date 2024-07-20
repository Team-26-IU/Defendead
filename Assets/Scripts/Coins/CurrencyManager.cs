using Unity.Properties;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public int coins = 100;
    public CurrencyDisplay currencyDisplay;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currencyDisplay.UpdateCoinDisplay(coins);
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        currencyDisplay.UpdateCoinDisplay(coins);
    }

    public void SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            currencyDisplay.UpdateCoinDisplay(coins);
        }
    }

    public int Coins => coins;
}