using System.Collections.Generic;
using UnityEngine;

public class Sc_Enemies : MonoBehaviour, Sc_IPooledObject<Sc_Enemies>
{
    [SerializeField, Min(0.0f)] private float _duration;
    [SerializeField, Min(0.0f)] private float _speed;

    private float _timer;
    private Sc_Pool<Sc_Enemies> _pool;

    private int _tileIndex;
    private Transform _tileTarget;
    private List<Transform> _tilesMap = new List<Transform>();

    private void OnEnable()
    {
        _timer = 0.0f;
    }

    private void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        if (_tileTarget == null)
            return;

        if (transform.position == _tileTarget.transform.position)
        {
            _tileIndex++;

            if (_tileIndex >= _tilesMap.Count)
            {
                _pool.Release(this);
                return;
            }

            _tileTarget = _tilesMap[_tileIndex];
        }
        float step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _tileTarget.transform.position, step);
    }

    public void SetTilesMap(List<Transform> p_tilesMap)
    {
        _tilesMap = p_tilesMap;
        _tileTarget = p_tilesMap[0];
        _tileIndex = 0;
    }

    public void SetPool(Sc_Pool<Sc_Enemies> pool)
    {
        if (pool == null) return;

        _pool = pool;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}