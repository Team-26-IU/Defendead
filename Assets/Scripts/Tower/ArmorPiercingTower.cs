using UnityEngine;

public class ArmorPiercingTower : Tower
{
    protected override void InitializeAttributes()
    {
        attackRadius = 350f;
        attackInterval = 3f;
        damage = 50;
        diffPosition = new Vector2(15f, 130f);
    }

    protected override void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        float closestDistance = attackRadius;
        Transform closestTarget = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("HeavyEnemy"))
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