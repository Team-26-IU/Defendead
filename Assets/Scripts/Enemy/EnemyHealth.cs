using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;
    private float CurrentHealth { get; set; }
    private Enemy _enemy;
    private EnemyFX _enemyFX;
    
    private void Start()
    {
        CurrentHealth = maxHealth;
        _enemy = GetComponent<Enemy>();
        _enemyFX = GetComponent<EnemyFX>();
    }

    private void Update()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount,CurrentHealth / maxHealth, Time.deltaTime * 10f);
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
    
    
    private void Die()
    {
      Destroy(gameObject);
    }
}