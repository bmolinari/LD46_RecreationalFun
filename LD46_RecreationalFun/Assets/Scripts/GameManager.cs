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
    public List<GameObject> enemyTypes = new List<GameObject>();
    public int minimumEnemyCount = 5;
    public int maximumEnemyCount = 15;
    public float enemySpawnRate = .75f;

    [Header("Level Management")]
    public int currentLevel = 0;
    public bool isLevelClear;

    [Header("Shop Management")]
    public GameObject shopkeeper;
    public GameObject saleCounter;
    public GameObject closeShopButton;

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
    private void Start()
    {
        currentLevel = 0;
        ResetLevel();
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
                OpenShop();
            }
        }
    }

    public void OpenShop()
    {
        shopkeeper.SetActive(true);
        shopkeeper.transform.localPosition = Vector3.zero;
        saleCounter.SetActive(true);
        closeShopButton.SetActive(true);
    }

    public void CloseShop()
    {
        shopkeeper.SetActive(false);
        saleCounter.SetActive(false);
        closeShopButton.SetActive(false);
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
                coinCount++; 
            }
            else if(coinCount > targetCoinCount)
            {
                coinCount--;
            }

            yield return new WaitForSeconds(rollUpDelay); 
        }
    }

    public void StartNextLevel()
    {
        currentLevel++;
        ResetLevel();
    }

    private void ResetLevel()
    {
        CloseShop();
        comboCount = 0;
        comboCountsPerLevel = new List<int>();
        enemyKillCount = 0;

        int enemyCount = Random.Range(minimumEnemyCount + (currentLevel * 5), maximumEnemyCount + (currentLevel * 5));
        enemiesLeftPerLevel = new List<GameObject>();
        StartCoroutine(SpawnEnemies(enemyCount));
    }

    private IEnumerator SpawnEnemies(int enemyTotal)
    {
        while(enemiesLeftPerLevel.Count < enemyTotal)
        {
            Vector3 randomSpawnLocation = new Vector3(Random.Range(-20, 20), Random.Range(-14, 14));
            GameObject newEnemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Count)], randomSpawnLocation, Quaternion.identity);
            newEnemy.GetComponent<EnemyController>().target = player;
            newEnemy.GetComponent<EnemyController>().SetRandomColor();
            enemiesLeftPerLevel.Add(newEnemy);

            yield return new WaitForSeconds(enemySpawnRate);
        }
    }
}
