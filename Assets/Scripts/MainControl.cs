using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControl : MonoBehaviour
{
    [Header("Prefabs")]
    public List<GameObject> enemyPrefabs;

    [Header("Attributes")]
    public int maxEnemyNum = 5;
    public float spawnX = 8f;
    public float spawnY = 5f;

    float spawnCD;

    // Use this for initialization
    void Start ()
    {
        spawnCD = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (spawnCD <= 0)
        {
            if (GameObject.FindGameObjectsWithTag("AI").Length < maxEnemyNum)
            {
                SpawnEnemy();
                spawnCD = Random.Range(5, 10);
            }
        }
        else
        if (spawnCD > 0)
        {
            spawnCD -= Time.deltaTime;
        }
	}

    public void Fire(GameObject bulletPrefab, Vector3 pos, Vector3 dir, float bulletSpeed)
    {
        GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
        bullet.GetComponent<BulletBehavior>().SetDirection(dir, bulletSpeed);
    }

    public void SpawnEnemy()
    {
        float x = Random.Range(-spawnX, spawnX);
        float y = spawnY;

        int idx = Random.Range(0, enemyPrefabs.Count);
        GameObject prefab = enemyPrefabs[idx];
        GameObject enemy = Instantiate(prefab, new Vector3(x, y, 0), Quaternion.identity);
    }
}
