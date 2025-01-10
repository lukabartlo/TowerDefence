using TMPro;
using UnityEngine;

public class Sc_PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private Sc_PlayerStats _playerStats;
    [SerializeField] private TextMeshProUGUI _healthAmount;
    [SerializeField] private TextMeshProUGUI _cashAmount;
    [SerializeField] private TextMeshProUGUI _waveAmount;

    private void Update()
    {
        _healthAmount.text = "health: " + _playerStats.playerHealth.ToString();
        _cashAmount.text = "cash: " + _playerStats.playerCash.ToString(); 
        _waveAmount.text = "Wave: " + _playerStats.currentWave.ToString() + "/20";
    }
}