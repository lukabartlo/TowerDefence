using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_PoolSpawner : MonoBehaviour
{
    public bool startedWave = false;
    
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Button _startWaveButton;
    [SerializeField] private Sc_PlayerStats _playerStats;

    [SerializeField] private Sc_Path _pathList;
    [SerializeField] private List<EnemyWaves> _waves = new List<EnemyWaves>();

    private int _waveNumber;
    private const int preAllocationCount = 50;
    
    private Sc_PoolComponent<Sc_Enemies> _enemyPool;
    private Sc_EnemyHealth _enemyHealth;

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

        if (_enemyPool.AliveObjectCount <= 0)
        {
            _startWaveButton.interactable = true;
        }
    }

    private void SpawnEnemy()
    {
        if (_waves.Count <= _waveNumber)
            return;

        EnemyWaves wave = _waves[_waveNumber];

        for (int i = 0; i < wave.enemyId.Count; i++) 
        {
            
            for (int j = 0; j < wave.enemyAmount[i]; j++)
            {
                Sc_Enemies enemy = _enemyPool.Get();
                enemy.transform.position = transform.position + Vector3.left * 2 * j;
                enemy.SetTilesMap(_pathList.pathList);
                enemy.GetComponent<Sc_EnemyHealth>().SetId(i);
                enemy.onDeath += OnEnemyDeath;
                enemy.exiting += OnEnemyExit;
            }
        }

        _startWaveButton.interactable = false;
        startedWave = false;
        _waveNumber++;
    }

    private void OnEnemyDeath(Sc_Enemies enemy)
    {
        _playerStats.IncreasePlayerCash(enemy.cashDrop);
        enemy.onDeath -= OnEnemyDeath;
        enemy.exiting -= OnEnemyExit;
    }

    private void OnEnemyExit(Sc_Enemies enemy)
    {
        _playerStats.LoseHealth(enemy.health);
        enemy.onDeath -= OnEnemyDeath;
        enemy.exiting -= OnEnemyExit;
    }
}

[System.Serializable]
public class EnemyWaves
{
    public List<int> enemyId;
    public List<int> enemyAmount;
}