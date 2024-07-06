using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;
    [SerializeField] private Image healthBar;
    public float CurrentHealth { get; set; }
    private Enemy _enemy;
    private EnemyFX _enemyFX;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemyFX = GetComponent<EnemyFX>();
        CurrentHealth = _enemy.MaxHealth;
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
        Destroy(gameObject);
    }
}