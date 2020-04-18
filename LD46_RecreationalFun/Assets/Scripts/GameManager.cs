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


    public int CurrentCombo
    {
        get { return comboCount; }
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
}
