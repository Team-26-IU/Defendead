using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBuilding : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public delegate void OnHealthChanged(int currentHealth, int maxHealth);
    public event OnHealthChanged onHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
        if (onHealthChanged != null)
        {
            onHealthChanged(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (onHealthChanged != null)
        {
            onHealthChanged(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Главное здание разрушено!");
        // Логика окончания игры или перезапуска уровня
        SceneManager.LoadScene(1);
    }
}