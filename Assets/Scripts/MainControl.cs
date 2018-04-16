using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public List<GameObject> scenePrefabs;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> bulletPrefabs;

    [Header("Attributes")]
    public ControlScheme controlScheme = ControlScheme.KEYBOARD;
    public int maxEnemyNum = 5;
    public float spawnX = 8f;
    public float spawnY = 5f;

    float enemyNum;

    // Use this for initialization
    void Awake ()
    {
        Services.sounds = GameObject.FindGameObjectWithTag("Sounds").GetComponent<SoundsControl>();
        Services.tasks = new TaskManager();
        Services.events = new EventManager();
        Services.scenes = new SceneManager<TransitionData>(gameObject, scenePrefabs);

        Services.scenes.PushScene<GameMenuScene>();
    }

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        Services.events.ProcessQueuedEvents();
        Services.tasks.Update();

        //SpawnEnemy();
    }

    public void FireAt(BulletRef bulletIdx, Vector3 pos, Vector3 dir, float bulletSpeed)
    {
        GameObject bullet = Instantiate(bulletPrefabs[(int)bulletIdx], pos, Quaternion.identity);
        bullet.GetComponent<BulletBehavior>().SetDirection(dir, bulletSpeed);
    }

    public void PlaySound(SoundsRef idx)
    {
        Services.sounds.Play((int)idx);
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

    public void SpawnBullet(Vector3 pos)
    {
        for (int i = 0; i < 6; ++i)
        {
            float angle = Random.Range(0, 360);
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);
            GameObject bullet = Instantiate(bulletPrefabs[(int)BulletRef.ENEMY_NORMAL], pos, Quaternion.identity);
            bullet.GetComponent<BulletBehavior>().SetDirection(new Vector3(x, y, 0), 3f);
            angle += 60;
        }
    }
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
