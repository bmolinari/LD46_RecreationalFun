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
    public int currentLevel = 1;
    public bool isLevelClear;

    [Header("Shop Management")]
    public GameObject shopkeeper;
    public GameObject saleCounter;
    public GameObject closeShopButton;
    public GameObject shopTextInstructions;
    public GameObject shopTooltip;

    [Header("Player Management")]
    public GameObject player;
    public GameObject defaultWeapon;
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
        currentLevel = 1;
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
        shopkeeper.transform.localPosition = new Vector3(0, -.165f, 0);
        saleCounter.SetActive(true);
        foreach(Transform child in saleCounter.transform)
        {
            child.gameObject.SetActive(true);
        }
        closeShopButton.SetActive(true);
        shopTextInstructions.SetActive(true);
    }

    public void CloseShop()
    {
        shopkeeper.SetActive(false);
        saleCounter.SetActive(false);
        closeShopButton.SetActive(false);
        shopTooltip.SetActive(false);
        shopTextInstructions.SetActive(false);
    }

    public void PayoutPlayer()
    {
        int payoutAmount = (enemyKillCount/currentLevel) + enemyKillCount + GetHighestCurrentCombo();

        //Debug.Log($"Highest Combo: {GetHighestCurrentCombo().ToString()}");
        //Debug.Log($"enemyKillCount: {enemyKillCount}");
        //Debug.Log($"Toxicity: {player.GetComponent<PlayerToxicity>().CurrentToxcicity}");
        //Debug.Log($"Payout: {payoutAmount}");
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

    public bool PurchaseWeapon(Weapon weapon)
    {
        if (coinCount >= weapon.cost)
        {
            targetCoinCount -= weapon.cost;

            foreach (Transform child in player.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            Instantiate(weapon, player.transform);

            StartCoroutine(ScoreUpdater());
            return true;
        }

        return false;
    }

    public void ReturnDefaultWeaponToPlayer()
    {
        foreach (Transform child in player.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Instantiate(defaultWeapon, player.transform);
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

        int enemyCount = Random.Range(minimumEnemyCount + currentLevel, maximumEnemyCount + currentLevel);
        enemiesLeftPerLevel = new List<GameObject>();
        StartCoroutine(SpawnEnemies(enemyCount));
    }

    private IEnumerator SpawnEnemies(int enemyTotal)
    {
        while(enemiesLeftPerLevel.Count < enemyTotal)
        {
            Vector3 randomSpawnLocation = new Vector3(Random.Range(-20, 20), Random.Range(-14, 14));


            if(currentLevel == 1)
            {
                GameObject newEnemy = Instantiate(enemyTypes[0], randomSpawnLocation, Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().target = player;
                newEnemy.GetComponent<EnemyController>().SetRandomColor();
                enemiesLeftPerLevel.Add(newEnemy);
            }
            else if (currentLevel == 2)
            {
                GameObject newEnemy = Instantiate(enemyTypes[Random.Range(0, 1)], randomSpawnLocation, Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().target = player;
                newEnemy.GetComponent<EnemyController>().SetRandomColor();
                enemiesLeftPerLevel.Add(newEnemy);
            }
            else
            {
                GameObject newEnemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Count)], randomSpawnLocation, Quaternion.identity);
                newEnemy.GetComponent<EnemyController>().target = player;
                newEnemy.GetComponent<EnemyController>().SetRandomColor();
                enemiesLeftPerLevel.Add(newEnemy);
            }

            yield return new WaitForSeconds(enemySpawnRate);
        }
    }
}
