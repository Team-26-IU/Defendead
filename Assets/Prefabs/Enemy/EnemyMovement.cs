using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Waypoint waypoint;
    [SerializeField] private float speed = 10000f;
    private int currentWaypointIndex = 0;
    private Vector3 targetPosition;

    private void Start()
    {
        if (waypoint != null && waypoint.Points.Length > 0)
        {
            targetPosition = waypoint.GetWaypointPosition(currentWaypointIndex);
        }
    }

    private void Update()
    {
        if (waypoint == null || waypoint.Points.Length == 0)
            return;

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) < 200f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex < waypoint.Points.Length)
            {
                targetPosition = waypoint.GetWaypointPosition(currentWaypointIndex);
            }
            else
            {
                OnPathComplete();
            }
        }
    }

    private void OnPathComplete()
    {
        Destroy(gameObject);
    }
}