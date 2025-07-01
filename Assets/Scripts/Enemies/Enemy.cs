using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMovement _movement;

    private Target _target;

    public event Action<Enemy> DestroyOnCollision;

    public EnemyMovement Movement => _movement;

    public void Initialize(Target target)
    {
        _target = target;
        _movement.SetTarget(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Target>() == _target)
        {
            DestroyOnCollision?.Invoke(this);
        }
    }
}