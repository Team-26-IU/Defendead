using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public MainBuilding mainBuilding;
    public Image Bar;

    void Start()
    {
        if (mainBuilding != null)
        {
            mainBuilding.onHealthChanged += UpdateHealthBar;
        }
    }

    void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        Bar.fillAmount = healthPercentage;
    }

    private void OnDestroy()
    {
        if (mainBuilding != null)
        {
            mainBuilding.onHealthChanged -= UpdateHealthBar;
        }
    }
}