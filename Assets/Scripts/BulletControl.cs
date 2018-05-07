using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public enum BulletRef
    {
        PLAYER_NORMAL = 0,
        ENEMY_NORMAL = 1,
        PLAYER_SLEEP = 2,
    };

    public List<GameObject> bulletPrefabs;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void FireAt(BulletControl.BulletRef bulletIdx, Vector3 pos, Vector3 dir, float bulletSpeed)
    {
        GameObject bullet = Instantiate(bulletPrefabs[(int)bulletIdx], pos, Quaternion.identity);
        bullet.GetComponent<BulletBehavior>().SetDirection(dir, bulletSpeed);
    }
}
