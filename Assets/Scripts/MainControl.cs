using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class MainControl : MonoBehaviour
{
    public enum ControlScheme
    {
        KEYBOARD = 0,
        MOUSE = 1,
    };

    public enum SoundsRef
    {
        FISHY_CREATE = 0,
        FISHY_DESTROY = 1,
        SHARKY_CREATE = 2,
        SHARKY_DESTROY = 3,
        CRIKY_CREATE = 4,
        CRIKY_DESTROY = 5,
    };

    public enum BulletRef
    {
        PLAYER_NORMAL = 0,
        ENEMY_NORMAL = 1,
    };

    [Header("Prefabs")]
    public List<GameObject> enemyPrefabs;
    public List<GameObject> bulletPrefabs;

    [Header("Attributes")]
    public ControlScheme controlScheme = ControlScheme.KEYBOARD;
    public int maxEnemyNum = 5;
    public float spawnX = 8f;
    public float spawnY = 5f;

    SoundsControl soundsController;
    float enemyNum; 

    // Use this for initialization
    void Start ()
    {
        soundsController = GameObject.FindGameObjectWithTag("Sounds").GetComponent<SoundsControl>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        EventManager.Instance.ProcessQueuedEvents();

        enemyNum = GameObject.FindGameObjectsWithTag("Enemy").Length;
        SpawnEnemy();
    }

    public void FireAt(BulletRef bulletIdx, Vector3 pos, Vector3 dir, float bulletSpeed)
    {
        GameObject bullet = Instantiate(bulletPrefabs[(int)bulletIdx], pos, Quaternion.identity);
        bullet.GetComponent<BulletBehavior>().SetDirection(dir, bulletSpeed);
    }

    public void PlaySound(SoundsRef idx)
    {
        soundsController.Play((int)idx);
    }

    public void SpawnEnemy()
    {
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
    /*
    public void SpawnEnemy()
    {

    }
    */
}



/*
 * 
 *                       MainControl
 *                 /         |          \                   \
 *      EnemyControl     ShipControl    BulletControl   SoundsControl
 *            |  |                            |
 *      EnemyBehavior                   BulletBehavior
 *      /           \
 * SharkyBehavior  FishyBehavior
 */
