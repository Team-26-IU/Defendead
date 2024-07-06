using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public static Action<Enemy> OnEndReached;
    public MainBuilding mainBuilding;
    protected float moveSpeed;
    protected int deathCoinReward;
    protected int damage;
    protected float maxHealth;
    protected bool hide;
    protected bool heavy;

    public int DeathCoinReward { get; protected set; }
    public int Damage{ get; protected set; }
    public float MoveSpeed { get; set; }
    public Waypoint Waypoint;
    public EnemyHealth EnemyHealth { get; set; }

    public Vector3 CurrentPointPosition => Waypoint.GetWaypointPosition(_currentWaypointIndex);

    private int _currentWaypointIndex;
    private Vector3 _lastPointPosition;

    private SpriteRenderer _spriteRenderer;

    protected virtual void Start()
    {
        mainBuilding = FindObjectOfType<MainBuilding>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        EnemyHealth = GetComponent<EnemyHealth>();

        _currentWaypointIndex = 0;
        MoveSpeed = moveSpeed;
        _lastPointPosition = transform.position;
        DeathCoinReward = deathCoinReward;
        Rotate();
    }

    protected virtual void Awake()
    {
        InitializeAttributes();
    }

    private void Update()
    {
        Move();
        Rotate();
        if (CurrentPointPositionReached())
        {
            UpdateCurrentPointIndex();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        MoveSpeed = 0f;
    }

    public void ResumeMovement()
    {
        MoveSpeed = moveSpeed;
    }

    private void Rotate()
    {
        if (CurrentPointPosition.x > _lastPointPosition.x)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            _lastPointPosition = transform.position;
            return true;
        }
        return false;
    }

    public void UpdateCurrentPointIndex()
    {
        int lastWaypointIndex = Waypoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
        else
        {
            EndPointReached();
        }
    }

    private void EndPointReached()
    {
        if (mainBuilding)
        {
            mainBuilding.TakeDamage(damage);
        }

        OnEndReached?.Invoke(this);
        EnemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }

    public float MaxHealth => maxHealth;

    public bool Hide => hide;

    public bool Heavy => heavy;

    protected abstract void InitializeAttributes();
}
