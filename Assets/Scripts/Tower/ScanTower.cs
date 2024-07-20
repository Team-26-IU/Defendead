using UnityEngine;

public class ScanTower : Tower
{
    [SerializeField] private GameObject bulletType2Prefab; 

    protected override void InitializeAttributes()
    {
        attackRadius = 350f;
        attackInterval = 1.5f;
        damage = 20;
        diffPosition = new Vector2(2f, 140f);
        bulletPrefab = bulletType2Prefab;
        price = 75;
        sellPrice = 40;
        upgradePrice = 45;
    }

    protected override void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        float closestDistance = attackRadius;
        Transform closestTarget = null;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("StealthEnemy") || collider.CompareTag("DefaultEnemy"))
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
            Vector3 offset = new Vector3(10f, 80, 0); 
            Vector3 newPos = transform.position + offset; 
            GameObject bulletObject = Instantiate(bulletPrefab, newPos, Quaternion.identity);
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.Initialize(damage, target);
        }
    }
}