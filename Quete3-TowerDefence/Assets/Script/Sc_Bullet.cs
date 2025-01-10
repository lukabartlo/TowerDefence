using UnityEngine;

public class Sc_Bullet : MonoBehaviour
{
    public Sc_DefenceStats towerDamage;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletLifeSpanMax;

    private float _bulletLifeSpan = 0f;
    private Transform _target;

    void FixedUpdate()
    {
        if (!_target)
            return;

        Vector2 dir = (_target.position - transform.position).normalized;
        _rb.velocity = dir * _bulletSpeed;

        DestroyBullet();
    }

    public void SetTarget(Transform p_target)
    {
        _target = p_target;
    }

    private void DestroyBullet()
    {
        _bulletLifeSpan += Time.deltaTime;
        if (_bulletLifeSpan >= _bulletLifeSpanMax)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
