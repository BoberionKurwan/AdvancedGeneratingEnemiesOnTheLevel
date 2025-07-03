using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private List<Waypoint> _waypoints;

    private Waypoint _currentWaypoint;
    private float _distanceToTarget = 0.1f;

    private void Start()
    {
        if (_waypoints.Count > 0)
            _currentWaypoint = SelectRandomWaypoint();
        else
            throw new System.Exception("Waypoints list is empty");

    }

    private void Update()
    {
        if (_currentWaypoint == null)
            _currentWaypoint = SelectRandomWaypoint();

        transform.position = Vector3.MoveTowards(
            transform.position,
            _currentWaypoint.transform.position,
            _movementSpeed * Time.deltaTime);

        if (IsTargetReached())
        {
            _currentWaypoint = SelectRandomWaypoint();
        }
    }

    private bool IsTargetReached()
    {
        return transform.position.IsEnoughClose(_currentWaypoint.transform.position, _distanceToTarget);
    }

    private Waypoint SelectRandomWaypoint()
    {
        return _waypoints[Random.Range(0, _waypoints.Count)];
    }
}
