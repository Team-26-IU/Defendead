using System;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected float attackRadius;
    protected float attackInterval;
    protected int damage;
    protected Vector2 diffPosition;

    private float attackTimer;
    protected Transform target;

    protected virtual void Start()
    {
        attackTimer = attackInterval;
    }

    protected virtual void Awake()
    {
        InitializeAttributes();
    }

    private void Update()
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

    protected abstract void FindTarget();

    private void Attack()
    {
        if (target)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            {
                enemy.EnemyHealth.DealDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    
    protected abstract void InitializeAttributes();
    
    public Vector2 DiffPos => diffPosition;
}