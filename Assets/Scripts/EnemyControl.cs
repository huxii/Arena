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
    public int spawnRate = 1;

    MainControl gameController;
    int enemyNum;
    float spawnCD;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainControl>();
        enemyNum = 0;
        spawnCD = 0;
    }

    // Update is called once per frame
    void Update()
    {
        enemyNum = GameObject.FindGameObjectsWithTag("AI").Length;

        if (enemyNum == 0)
        {
            spawnCD = 0;
        }

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
        for (int i = 0; i < spawnRate; ++i)
        {
            float x = Random.Range(-spawnX, spawnX);
            float y = spawnY;

            int idx = Random.Range(0, enemyPrefabs.Count);
            GameObject prefab = enemyPrefabs[idx];
            GameObject enemy = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
        }
    }

    public void FireAt(MainControl.BulletRef bulletIdx, Vector3 pos, Vector3 des, float bulletSpeed)
    {
        gameController.FireAt(bulletIdx, pos, des, bulletSpeed);
    }

    public void PlaySound(MainControl.SoundsRef idx)
    {
        gameController.PlaySound(idx);
    }
}
