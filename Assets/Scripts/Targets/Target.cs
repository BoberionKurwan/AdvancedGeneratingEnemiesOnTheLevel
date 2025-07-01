using UnityEngine;
using System.Collections.Generic;

public class Target : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private float _waypointSearchRadius = 10f;

    private Transform _currentWaypoint;
    private List<Transform> _allWaypoints = new List<Transform>();

    private void Start()
    {
        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (GameObject waypoint in waypointObjects)
        {
            _allWaypoints.Add(waypoint.transform);
        }

        SelectRandomWaypoint();
    }

    private void Update()
    {
        if (_currentWaypoint == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            _currentWaypoint.position,
            _movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _currentWaypoint.position) < 0.1f)
        {
            SelectRandomWaypoint();
        }
    }

    private void SelectRandomWaypoint()
    {
        if (_allWaypoints.Count == 0) return;

        List<Transform> waypointsInRadius = new List<Transform>();
        foreach (Transform waypoint in _allWaypoints)
        {
            if (Vector3.Distance(transform.position, waypoint.position) <= _waypointSearchRadius)
            {
                waypointsInRadius.Add(waypoint);
            }
        }

        if (waypointsInRadius.Count == 0)
        {
            waypointsInRadius = new List<Transform>(_allWaypoints);
        }

        if (_currentWaypoint != null && waypointsInRadius.Contains(_currentWaypoint))
        {
            waypointsInRadius.Remove(_currentWaypoint);
        }

        if (waypointsInRadius.Count > 0)
        {
            _currentWaypoint = waypointsInRadius[Random.Range(0, waypointsInRadius.Count)];
        }
    }
}