using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    protected float attackRadius;
    protected float attackInterval;
    protected int damage;
    protected int price;
    protected int sellPrice;
    protected int upgradePrice;
    protected Vector2 diffPosition;

    private float attackTimer;
    protected Transform target;
    private bool isUpgraded;

    protected GameObject bulletPrefab;
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

    protected abstract void Attack();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    protected abstract void InitializeAttributes();

    public Vector2 DiffPos => diffPosition;

    public int Price => price;

    public int Sell => sellPrice;

    public int Upgrade => upgradePrice;
}
