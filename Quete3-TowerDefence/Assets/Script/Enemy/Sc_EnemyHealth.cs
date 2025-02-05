using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sc_EnemyHealth : MonoBehaviour
{
    private Sc_Enemies _enemy;

    private readonly List<Sc_EnemyStats> _statsList = new List<Sc_EnemyStats>
    {
        new Sc_EnemyStats{id = 0, healthPoints = 150, speedEnemy = 5, scale = 0.5f, cash = 10},
        new Sc_EnemyStats{id = 1, healthPoints = 100, speedEnemy = 7, scale = 0.4f, cash = 10},
        new Sc_EnemyStats{id = 2, healthPoints = 500, speedEnemy = 3, scale = 0.6f, cash = 15},
        new Sc_EnemyStats{id = 3, healthPoints = 2000, speedEnemy = 3, scale = 0.75f, cash = 75},
        new Sc_EnemyStats{id = 4, healthPoints = 2000, speedEnemy = 5, scale = 0.75f, cash = 100},
        new Sc_EnemyStats{id = 5, healthPoints = 20000, speedEnemy = 1, scale = 0.9f, cash = 250},
    };
    private List<Sc_EnemyStats> _statsListCopy = new List<Sc_EnemyStats>();
    private int _indexEnemy = 0;

    private void Awake()
    {
        _enemy = GetComponent<Sc_Enemies>();
    }

    public void OnDisable()
    {
        _statsListCopy = _statsList.ToList();
    }

    public void SetId(int indexEnemy)
    {
        if (indexEnemy < 0 || indexEnemy >= _statsList.Count)
            return;

        _indexEnemy = indexEnemy;
        _enemy.transform.localScale = Vector3.one * _statsList[indexEnemy].scale;
        _enemy.speed =  _statsList[indexEnemy].speedEnemy;
        _enemy.health =  _statsList[indexEnemy].healthPoints;
        _enemy.cashDrop =  _statsList[indexEnemy].cash;
    }
}