using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyPrefabs;
    [SerializeField] private List<Target> _targets;
    [SerializeField] private float _spawnInterval = 2f;

    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    private Coroutine _spawnCoroutine;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent(out SpawnPoint spawnPoint))
            {
                _spawnPoints.Add(spawnPoint);
            }
        }
    }

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

    private void SpawnEnemies()
    {
        foreach (var spawnPoint in _spawnPoints)
        {
            Enemy enemy = spawnPoint.GetEnemy();
            enemy.CollisionEntered += DestroyEnemyOnCollision;
        }         
    }

    private void DestroyEnemyOnCollision(Enemy enemy)
    {
        enemy.CollisionEntered -= DestroyEnemyOnCollision;
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