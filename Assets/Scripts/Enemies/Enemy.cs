using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMover _mover;

    private Target _target;

    public event Action<Enemy> CollisionEntered;

    public EnemyMover Movement => _mover;

    public void Initialize(Target target)
    {
        _target = target;
        _mover.SetTarget(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Target>(out _))
        {
            CollisionEntered?.Invoke(this);
        }
    }
}