using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;
    const float speed = 950;
    private Transform target;

    public void Initialize(int damage, Transform target)
    {
        this.damage = damage;
        this.target = target; 
    }

    protected virtual void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 0.1f) 
            {
                OnHitTarget();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnHitTarget()
    {
        if (target != null)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.EnemyHealth.DealDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (target != null && collision.transform == target)
        {
            OnHitTarget();
        }
    }
}