using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int maxEnemyNum = 5;
    public float spawnX = 8f;
    public float spawnY = 5f;
    public List<GameObject> enemyPrefabs;

    float enemyNum;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        enemyNum = GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Boss").Length;
        if (enemyNum == 0)
        {
            for (int i = 0; i < maxEnemyNum; ++i)
            {
                float x = Random.Range(-spawnX, spawnX);
                float y = spawnY;

                int idx = Random.Range(0, enemyPrefabs.Count);
                GameObject prefab = enemyPrefabs[idx];
                GameObject enemy = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }

    public void SpawnEnemy(Vector3 pos)
    {
        enemyNum = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyNum == 0)
        {
            Services.tasks.Do(new Spawn(enemyPrefabs[0], transform.position, 0.25f, 2))
                .Then(new Spawn(enemyPrefabs[1], transform.position, 0.25f, 2))
                .Then(new Spawn(enemyPrefabs[2], transform.position, 0.25f, 2));
        }
    }
}
