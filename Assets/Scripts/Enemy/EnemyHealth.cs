using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;
    [SerializeField] private Image healthBar;
    private float CurrentHealth { get; set; }
    private Enemy _enemy;
    private int _coinDropAmount;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        CurrentHealth = _enemy.MaxHealth;
        _coinDropAmount = _enemy.Coins;
    }

    private void Update()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, CurrentHealth / _enemy.MaxHealth, Time.deltaTime * 10f);
    }

    public void DealDamage(float damageReceived)
    {
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = _enemy.MaxHealth;
        healthBar.fillAmount = 1f;
    }

    private void Die()
    {
        CurrencyManager.instance.AddCoins(_coinDropAmount);
        Destroy(gameObject);
    }
}