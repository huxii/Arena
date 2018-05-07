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

    public void FireAt(BulletRef bulletIdx, Vector3 pos, Vector3 dir, float bulletSpeed)
    {
        GameObject bullet = Instantiate(bulletPrefabs[(int)bulletIdx], pos, Quaternion.identity);
        bullet.GetComponent<BulletBehavior>().SetDirection(dir, bulletSpeed);
    }

    public void FireAround(BulletRef bulletIdx, Vector3 pos, float bulletSpeed = 3f, float angleRange = 360f)
    {
        float angle = 90 - angleRange * 0.5f;
        for (int i = 0; i < 6; ++i)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle);
            float y = Mathf.Sin(Mathf.Deg2Rad * angle);
            GameObject bullet = Instantiate(bulletPrefabs[(int)bulletIdx], pos, Quaternion.identity);
            bullet.GetComponent<BulletBehavior>().SetDirection(new Vector3(x, y, 0), bulletSpeed);
            angle += angleRange / 6;
        }
    }
}
