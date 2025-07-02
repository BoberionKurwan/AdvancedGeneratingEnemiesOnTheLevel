using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 3f;

    private Waypoint _currentWaypoint;
    List<Waypoint> _allWaypoints = new List<Waypoint>();
    private float _distanceToTarget = 0.1f;

    public bool IsTargetReached()
    {
        return transform.position.IsEnoughClose(_currentWaypoint.transform.position, _distanceToTarget);
    }

    private void Start()
    {
        _allWaypoints = FindObjectsByType<Waypoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
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

    private Waypoint SelectRandomWaypoint()
    {
        List<Waypoint> waypoints = _allWaypoints;
        return waypoints.Count > 0 ? waypoints[Random.Range(0, waypoints.Count)] : null;
    }
}
