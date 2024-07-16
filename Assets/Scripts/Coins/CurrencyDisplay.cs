using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CurrencyDisplay : MonoBehaviour
{
    public GameObject digitPrefab;
    public RectTransform coinDisplayParent;
    public Sprite[] digitSprites;

    private List<GameObject> digitObjects = new List<GameObject>();

    void Start()
    {
        UpdateCoinDisplay(100);
    }

    public void UpdateCoinDisplay(int amount)
    {
        foreach (GameObject digit in digitObjects)
        {
            Destroy(digit);
        }
        digitObjects.Clear();
        
        string amountString = amount.ToString();
        
        for (int i = 0; i < amountString.Length; i++)
        {
            char c = amountString[i];
            GameObject digitObject = Instantiate(digitPrefab, coinDisplayParent);
            Image digitImage = digitObject.GetComponent<Image>();
            
            int digit = c - '0';
            digitImage.sprite = digitSprites[digit];
            
            RectTransform rectTransform = digitObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(i * 40, 0); // Здесь 50 - расстояние между цифрами. Настройте по необходимости.

            digitObjects.Add(digitObject);
        }
    }
}