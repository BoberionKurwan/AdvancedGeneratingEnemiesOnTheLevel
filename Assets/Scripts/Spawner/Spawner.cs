using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyPrefabs;
    [SerializeField] private float _spawnInterval = 2f;

    private Dictionary<SpawnPoint, Enemy> _spawnAssignments = new Dictionary<SpawnPoint, Enemy>();
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

        AssignEnemiesToSpawnPoints();
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

    private void AssignEnemiesToSpawnPoints()
    {
        List<Enemy> availableEnemies = new List<Enemy>(_enemyPrefabs);
        List<SpawnPoint> availablePoints = new List<SpawnPoint>(_spawnPoints);

        ShuffleList(availableEnemies);
        ShuffleList(availablePoints);

        for (int i = 0; i < availablePoints.Count; i++)
        {
            Enemy assignedEnemy = i < availableEnemies.Count ?
                availableEnemies[i] :
                availableEnemies[Random.Range(0, availableEnemies.Count)];

            _spawnAssignments.Add(availablePoints[i], assignedEnemy);
        }
    }

    private void SpawnEnemies()
    {
        foreach (var assignment in _spawnAssignments)
        {
            SpawnPoint spawnPoint = assignment.Key;
            Enemy enemyPrefab = assignment.Value;
            Target target = spawnPoint.GetRandomTarget();

            if (target != null)
            {
                Enemy newEnemy = Instantiate(enemyPrefab);
                newEnemy.transform.position = spawnPoint.GetSpawnPosition(
                    newEnemy.GetComponent<Collider>().bounds.size.y);
                newEnemy.Initialize(target);
                newEnemy.DestroyOnCollision += OnEnemyReachedTarget;
            }
        }
    }

    private void OnEnemyReachedTarget(Enemy enemy)
    {
        enemy.DestroyOnCollision -= OnEnemyReachedTarget;
        Destroy(enemy.gameObject);
    }

    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    private IEnumerator SpawnRoutine()
    {
        WaitForSeconds waitInterval = new WaitForSeconds(_spawnInterval);

        while (true)
        {
            yield return waitInterval;
            SpawnEnemies();
        }
    }
}