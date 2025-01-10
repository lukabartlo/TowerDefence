using UnityEngine;

public class Sc_DefenceAttack : MonoBehaviour
{
    [SerializeField] private Sc_DefenceStats _stats; 
    [SerializeField] private LayerMask _ennemyLayer;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firingPoint;

    private bool _isPlaced = false;
    private Transform _target = null;
    private float _timeUntilFire = 0f;
    private float _rotationSpeed = 300f;

    private void Update()
    {
        if (!_isPlaced)
            return;

        if (_target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardTarget();

        if (!CheckTargetIsInRange())
        {
            _target = null;
            return;
        }

        else 
        {
            _timeUntilFire += Time.deltaTime;
            if (_timeUntilFire >= _stats.attackSpeed)
            {
                Shoot();
                _timeUntilFire = 0f;
            }
        }
    }
    
    private void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _stats.range, _ennemyLayer);
        if (hits.Length > 0)
        {
            _target = hits[0].transform;
        }
    }

    private void RotateTowardTarget()
    {
        float angle = Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(_target.position, transform.position) <= _stats.range;
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(_bulletPrefab, _firingPoint.position, Quaternion.identity);
        Sc_Bullet bullet = bulletObj.GetComponent<Sc_Bullet>();
        bullet.SetTarget(_target);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    public void SetIsPlaced(bool value)
    {
        _isPlaced = value;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _stats.range);
        }
    }
}