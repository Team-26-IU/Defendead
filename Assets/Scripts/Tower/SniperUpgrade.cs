﻿using UnityEngine;

public class SniperUpgrade : Tower
{
    [SerializeField] private GameObject bulletType1Prefab; 

    protected override void InitializeAttributes()
    {
        attackRadius = 500f;
        attackInterval = 1.5f;
        damage = 35;
        diffPosition = new Vector2(0f, 120f);
        bulletPrefab = bulletType1Prefab;
        sellPrice = 50;
    }

    protected override void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        float closestDistance = attackRadius;
        Transform closestTarget = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("DefaultEnemy"))
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
    
    protected override void Attack()
    {
        if (target)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.Initialize(damage, target);
        }
    }
}