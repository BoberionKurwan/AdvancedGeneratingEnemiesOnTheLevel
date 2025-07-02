using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float _heightOffset = 0.5f;

    private Enemy _enemyPrefab;
    private Target _target;

    public void AssignEnemyPrefab(Enemy prefab) => _enemyPrefab = prefab;
    public void AssignTarget(Target target) => _target = target;

    public Vector3 GetSpawnPosition(float enemyHeight)
    {
        return transform.position + Vector3.up * enemyHeight * _heightOffset;
    }

    public Enemy GetEnemy()
    {        
        Enemy enemyInstance = Instantiate(_enemyPrefab);
        enemyInstance.transform.position = GetSpawnPosition(enemyInstance.GetComponent<Collider>().bounds.size.y);
        enemyInstance.Initialize(_target);

        return enemyInstance;
    }
}