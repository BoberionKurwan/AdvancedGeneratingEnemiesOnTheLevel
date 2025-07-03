using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private List<Waypoint> _waypoints;


    private Waypoint _currentWaypoint;
    private float _distanceToTarget = 0.1f;

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

    public bool IsTargetReached()
    {
        return transform.position.IsEnoughClose(_currentWaypoint.transform.position, _distanceToTarget);
    }

    private Waypoint SelectRandomWaypoint()
    {
        List<Waypoint> waypoints = _waypoints;
        return waypoints.Count > 0 ? waypoints[Random.Range(0, waypoints.Count)] : null;
    }
}
