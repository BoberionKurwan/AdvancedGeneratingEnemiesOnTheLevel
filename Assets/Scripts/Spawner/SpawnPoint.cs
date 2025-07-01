using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float _heightOffset = 0.5f;
    [SerializeField] private float _targetSearchRadius = 10f;

    public Vector3 GetSpawnPosition(float enemyHeight)
    {
        return transform.position + Vector3.up * enemyHeight * _heightOffset;
    }

    public Target GetRandomTarget()
    {
        List<Target> targets = FindNearbyTargets();
        return targets.Count > 0 ? targets[Random.Range(0, targets.Count)] : null;
    }

    private List<Target> FindNearbyTargets()
    {
        List<Target> nearbyTargets = new List<Target>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _targetSearchRadius);

        foreach (var collider in hitColliders)
        {
            if (collider.TryGetComponent(out Target target))
            {
                nearbyTargets.Add(target);
            }
        }

        return nearbyTargets;
    }
}