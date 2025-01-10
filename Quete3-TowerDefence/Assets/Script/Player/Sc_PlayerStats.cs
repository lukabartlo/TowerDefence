using UnityEngine;

public class Sc_PlayerStats : MonoBehaviour
{
    public int playerHealth = 2000;
    public int playerCash = 450;
    public int currentWave = 0;

    public void IncreasePlayerCash(int amount)
    {
        playerCash += amount;
    }

    public bool SpendPlayerCash(int amount)
    {
        if (amount <= playerCash)
        {
            playerCash -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoseHealth(int amount)
    {
        playerHealth -= amount;
    }
}
