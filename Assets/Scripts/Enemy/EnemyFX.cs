using TMPro;
using UnityEngine;

public class EnemyFX : MonoBehaviour
{
    [SerializeField] private Transform textDamageSpawnPosition;

    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }
}