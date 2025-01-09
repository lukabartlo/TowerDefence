using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _bulletSpeed;

    private Transform _target;

    void FixedUpdate()
    {
        if (!_target)
            return;

        Vector2 dir = (_target.position - transform.position).normalized;
        _rb.velocity = dir * _bulletSpeed;
    }

    public void SetTarget(Transform p_target)
    {
        _target = p_target;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
