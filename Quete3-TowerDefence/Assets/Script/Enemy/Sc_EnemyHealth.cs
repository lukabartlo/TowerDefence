using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_EnemyHealth : MonoBehaviour
{
    private readonly List<Sc_EnemyStats> _statsList = new List<Sc_EnemyStats>
    {
        new Sc_EnemyStats{healthPoints = 150, speed = 7},
        new Sc_EnemyStats{healthPoints = 100, speed = 10},
        new Sc_EnemyStats{healthPoints = 500, speed = 5},
    };
}
