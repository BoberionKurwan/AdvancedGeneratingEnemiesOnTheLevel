using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyPrefabs;
    [SerializeField] private List<Target> _targets;
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    private Coroutine _spawnCoroutine;

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    private void OnDestroy()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        _spawnPoints.Clear();
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out SpawnPoint spawnPoint))
            {
                _spawnPoints.Add(spawnPoint);
            }
        }
    }
#endif

    private void SpawnEnemies()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            Enemy enemy = spawnPoint.GetEnemy();
            enemy.TargetReached += DestroyEnemyOnTargetReached;
        }
    }

    private void DestroyEnemyOnTargetReached(Enemy enemy)
    {
        enemy.TargetReached -= DestroyEnemyOnTargetReached;
        Destroy(enemy.gameObject);
    }

    private IEnumerator SpawnRoutine()
    {
        WaitForSeconds waitInterval = new WaitForSeconds(_spawnInterval);

        while (enabled)
        {
            yield return waitInterval;
            SpawnEnemies();
        }
    }
}