using System.Collections.Generic;
using UnityEngine;

public class Sc_PoolSpawner : MonoBehaviour
{
    public bool startedWave = false;
    
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField, Min(0.0f)] private float _enemySpawnRate = 0.5f;

    [SerializeField] private Sc_Path _pathList;
    [SerializeField] private List<EnemyWaves> _waves = new List<EnemyWaves>();

    private int _waveNumber;
    private const int preAllocationCount = 50;
    
    private Sc_PoolComponent<Sc_Enemies> _enemyPool;

    private void Awake()
    {
        _enemyPool = new Sc_PoolComponent<Sc_Enemies>(_enemyPrefab, 500, preAllocationCount);
    }

    private void Update()
    {
        if (startedWave == true)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {

        EnemyWaves wave = _waves[_waveNumber];
        for (int i = 0; i < wave.enemyId.Count; i++) 
        {
            for(int j = 0; j < wave.enemyAmount[i]; j++)
            {
                Sc_Enemies enemy = _enemyPool.Get();
                enemy.transform.position = transform.position + Vector3.left * 2 * j;
                enemy.SetTilesMap(_pathList.pathList);
            }
        }
        startedWave = false;
        _waveNumber++;
    }
}

[System.Serializable]
public class EnemyWaves
{
    public List<int> enemyId;
    public List<int> enemyAmount;
}