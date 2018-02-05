using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    [Header("Prefabs")]
    public List<GameObject> enemyPrefabs;

    [Header("Attributes")]
    public int maxEnemyNum = 5;
    public float spawnX = 8f;
    public float spawnY = 5f;

    int enemyNum;
    float spawnCD;

    // Use this for initialization
    void Start ()
    {
        enemyNum = 0;
        spawnCD = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        enemyNum = GameObject.FindGameObjectsWithTag("AI").Length;
        if (spawnCD <= 0)
        {
            if (enemyNum < maxEnemyNum)
            {
                SpawnEnemy();
                spawnCD = Random.Range(2, 5);
            }
        }
        else
        if (spawnCD > 0)
        {
            spawnCD -= Time.deltaTime;
        }
    }

    void SpawnEnemy()
    {
        float x = Random.Range(-spawnX, spawnX);
        float y = spawnY;

        int idx = Random.Range(0, enemyPrefabs.Count);
        GameObject prefab = enemyPrefabs[idx];
        GameObject enemy = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
    }
}
