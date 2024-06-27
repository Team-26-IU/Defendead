using UnityEngine;

public class Tower : MonoBehaviour
{
    public float attackRadius = 5f;
    public float attackInterval = 1f;
    public int damage = 10;

    private float attackTimer;
    private Transform target;

    void Start()
    {
        attackTimer = attackInterval;
    }

    void Update()
    {
        FindTarget();
        if (target)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                Attack();
                attackTimer = attackInterval;
            }
        }
    }

    void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        float closestDistance = attackRadius;
        Transform closestTarget = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = collider.transform;
                }
            }
        }

        target = closestTarget;
    }

    void Attack()
    {
        if (target)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy)
            {
                Debug.Log(enemy + " take a damage, now he have: " + enemy.currentHealth);
                enemy.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}