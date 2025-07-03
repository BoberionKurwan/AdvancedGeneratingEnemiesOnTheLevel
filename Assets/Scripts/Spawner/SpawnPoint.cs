using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private float _heightOffset = 0.5f;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Target _target;

    public Vector3 GetSpawnPosition(float enemyHeight)
    {
        return transform.position + Vector3.up * enemyHeight * _heightOffset;
    }

    public Enemy GetEnemy()
    {        
        Enemy enemyInstance = Instantiate(_enemyPrefab);

        if (enemyInstance.TryGetComponent<Collider>(out _))
        {
            enemyInstance.transform.position = GetSpawnPosition(enemyInstance.GetComponent<Collider>().bounds.size.y);
        }

        enemyInstance.Initialize(_target);

        return enemyInstance;
    }
}