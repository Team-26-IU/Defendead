using UnityEngine;

public class ScanTower : Tower
{
    protected override void InitializeAttributes()
    {
        attackRadius = 350f;
        attackInterval = 1.5f;
        damage = 20;
        diffPosition = new Vector2(2f, 140f);
    }

    protected override void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        float closestDistance = attackRadius;
        Transform closestTarget = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("StealthEnemy"))
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
}