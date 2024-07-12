using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBuilding : MonoBehaviour
{
    public int maxHealth = 500;
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
        SceneManager.LoadScene(1);
    }
}