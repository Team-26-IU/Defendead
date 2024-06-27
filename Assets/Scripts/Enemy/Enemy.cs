using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public EnemyHealth health; //change here something

    void Start()
    {
        currentHealth = maxHealth;
        health = GetComponent<EnemyHealth>();
        if (health == null) // If there's no EnemyHealth component attached, add one.
        {
            health = gameObject.AddComponent<EnemyHealth>();
        }
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Можете добавить здесь эффекты смерти или анимацию
        Destroy(gameObject);
    }
}