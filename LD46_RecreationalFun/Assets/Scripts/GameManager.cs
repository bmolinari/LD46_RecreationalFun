using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Combo Counting")]
    public int comboCount;
    public List<int> comboCountsPerLevel = new List<int>();

    [Header("Enemy Management")]
    public int enemyKillCount;
    public List<GameObject> enemiesLeftPerLevel = new List<GameObject>();

    [Header("Level Management")]
    public bool isLevelClear;

    [Header("Shop Management")]
    public GameObject shopkeeper;
    public GameObject saleCounter;

    [Header("Player Management")]
    public GameObject player;
    public int coinCount;
    public int targetCoinCount;
    public float rollUpDelay = 0.0025f;

    public int CurrentCombo
    {
        get { return comboCount; }
    }

    public int CurrentCoinCount
    {
        get { return coinCount; }
    }

    private void Awake()
    {
        if (instance == null) // Doesn't exist!
        {
            instance = this;
        }
        else if (instance != this) // Already exists!
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void IncreaseComboCount()
    {
        comboCount++;
    }

    public void ResetComboCount()
    {
        if (comboCount <= 1)
            return;

        comboCountsPerLevel.Add(comboCount);
        comboCount = 0;
    }

    public int GetHighestCurrentCombo()
    {
        if (comboCountsPerLevel.Count > 0)
        {
            comboCountsPerLevel.Sort();
            return comboCountsPerLevel[comboCountsPerLevel.Count - 1];
        }

        return 1;
    }

    public void RemoveTrackedEnemy(GameObject enemy)
    {
        bool removed = enemiesLeftPerLevel.Remove(enemy);
        if (removed)
        {
            enemyKillCount++;

            if (enemiesLeftPerLevel.Count <= 0)
            {
                isLevelClear = true;
                PayoutPlayer();
                ActivateShop();
            }
        }
    }

    public void ActivateShop()
    {
        shopkeeper.SetActive(true);
        shopkeeper.transform.localPosition = Vector3.zero;
        saleCounter.SetActive(true);
    }

    public void PayoutPlayer()
    {
        int payoutAmount = (GetHighestCurrentCombo() * enemyKillCount) + (int)(player.GetComponent<PlayerToxicity>().CurrentToxcicity); // 10);

        Debug.Log($"Highest Combo: {GetHighestCurrentCombo().ToString()}");
        Debug.Log($"enemyKillCount: {enemyKillCount}");
        Debug.Log($"Toxicity: {player.GetComponent<PlayerToxicity>().CurrentToxcicity}");
        Debug.Log($"Payout: {payoutAmount}");
        targetCoinCount = coinCount + payoutAmount;
        StartCoroutine(ScoreUpdater());
    }


    public void PurchaseSubstance(Substance substance)
    {
        if (coinCount >= substance.cost)
        {
            targetCoinCount -= substance.cost;
            player.GetComponent<PlayerToxicity>().IngestSubstance(substance.intoxicationAmount);
            StartCoroutine(ScoreUpdater());
        }
    }

    private IEnumerator ScoreUpdater()
    {
        while (true)
        {
            if (coinCount < targetCoinCount)
            {
                coinCount++; //Increment the display score by 1
            }
            else if(coinCount > targetCoinCount)
            {
                coinCount--;
            }

            yield return new WaitForSeconds(rollUpDelay); // I used .2 secs but you can update it as fast as you want
        }
    }
}
