using UnityEngine;

public class Sc_PoolSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField, Min(0.0f)] private float _enemySpawnRate = 0.5f;

    [SerializeField] private Sc_Path _pathList;

    private float _enemySpawnTimer;

    private Sc_Pool<Sc_Enemies> _enemyPool;

    private void Awake()
    {
        _enemySpawnTimer = 0.0f;
        _enemyPool = new Sc_Pool<Sc_Enemies>(CreateEnemy, OnGetEnemy, OnReleaseEnemy);
    }

    private Sc_Enemies CreateEnemy()
    {
        GameObject go = Instantiate(_enemyPrefab);
        Sc_Enemies enemy = go.GetComponent<Sc_Enemies>();
        return enemy;
    }

    private void OnGetEnemy(Sc_Enemies enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    private void OnReleaseEnemy(Sc_Enemies enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void Update()
    {
        _enemySpawnTimer += Time.deltaTime;

        if (_enemySpawnTimer >= _enemySpawnRate)
        {
            _enemySpawnTimer = 0.0f;

            Sc_Enemies enemy = _enemyPool.Get();
            enemy.SetTilesMap(_pathList.pathList);
            enemy.transform.position = transform.position;
        }
    }
}