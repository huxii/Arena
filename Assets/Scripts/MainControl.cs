using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControl : MonoBehaviour
{
    public enum ControlScheme
    {
        KEYBOARD = 1,
        MOUSE = 2,
    };

    public enum SoundsRef
    {
        FISHYCREATE = 0,
        FISHYDESTROY = 1,
        SHARKYCREATE = 2,
        SHARKYDESTROY = 3,
    };

    [Header("Prefabs")]
    public GameObject Sounds;

    [Header("Attributes")]
    public ControlScheme controlScheme = ControlScheme.KEYBOARD;

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void Fire(GameObject bulletPrefab, Vector3 pos, Vector3 dir, float bulletSpeed)
    {
        GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
        bullet.GetComponent<BulletBehavior>().SetDirection(dir, bulletSpeed);
    }

    public void PlaySound(SoundsRef idx)
    {
        Sounds.GetComponent<SoundsControl>().Play((int)idx);
    }
}



/*
 * 
 *              MainControl
 *             /            \
 *      EnemyControl     ShipControl
 *            |  |              |
 *            |  -----------------------|           
 *      EnemyBehavior              BulletBehavior
 *      /           \
 * SharkyBehavior  FishyBehavior
 */