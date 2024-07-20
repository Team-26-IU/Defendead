using UnityEngine;

public class TurretUpgrade : Tower
{
    [SerializeField] private GameObject armorPiercingBulletPrefab;

    protected override void InitializeAttributes()
    {
        attackRadius = 425f;
        attackInterval = 2f;
        damage = 50;
        diffPosition = new Vector2(5f, 0f);
        price = 100;
        sellPrice = 90;
        bulletPrefab = armorPiercingBulletPrefab; 
    }

    protected override void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        float closestDistance = attackRadius;
        Transform closestTarget = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("HeavyEnemy") || collider.CompareTag("DefaultEnemy"))
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
            Vector3 offset = new Vector3(10f, 65, 0); 
            Vector3 newPos = transform.position + offset; 
            GameObject bulletObject = Instantiate(bulletPrefab, newPos, Quaternion.identity);
            Bullet bullet = bulletObject.GetComponent<Bullet>();

            
            bullet.Initialize(damage, target);
        }
    }
}