using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugNestController : MonoBehaviour
{
    public List<GameObject> enemyTypes = new List<GameObject>();
    public float health = 50;
    public float maxSpawnDelay = 3f;
    private float currSpawnDelay = 0;

    private void OnEnable()
    {
        maxSpawnDelay = Random.Range(3, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if(currSpawnDelay < maxSpawnDelay)
        {
            currSpawnDelay += Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomSpawnLocation = new Vector3(transform.position.x + Random.Range(-3,3), transform.position.y + Random.Range(-3, 3));
        GameObject newEnemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Count)], randomSpawnLocation, Quaternion.identity);
        newEnemy.GetComponent<EnemyController>().target = GameObject.Find("Player");
        newEnemy.GetComponent<EnemyController>().SetRandomColor();
        GameManager.instance.AddEnemyToTrack(newEnemy);
        currSpawnDelay = 0;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.instance.RemoveBugNest(gameObject);
        Destroy(gameObject);
    }
}
