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
        FISHY_CREATE = 0,
        FISHY_DESTROY = 1,
        SHARKY_CREATE = 2,
        SHARKY_DESTROY = 3,
    };

    public enum BulletRef
    {
        PLAYER_NORMAL = 0,
        ENEMY_NORMAL = 1,
    };

    [Header("Prefabs")]
    public GameObject soundsManager;
    public GameObject bulletManager;

    [Header("Attributes")]
    public ControlScheme controlScheme = ControlScheme.KEYBOARD;

    SoundsControl soundsController;
    BulletControl bulletController;

    // Use this for initialization
    void Start ()
    {
        soundsController = soundsManager.GetComponent<SoundsControl>();
        bulletController = bulletManager.GetComponent<BulletControl>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void FireAt(BulletRef bulletIdx, Vector3 pos, Vector3 dir, float bulletSpeed)
    {
        bulletController.FireAt(bulletIdx, pos, dir, bulletSpeed);
    }

    public void PlaySound(SoundsRef idx)
    {
        soundsController.Play((int)idx);
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
