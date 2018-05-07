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

    public GameObject soundManager;
    public GameObject bulletManager;
    public GameObject enemyManager;

    [Header("Prefabs")]
    public List<GameObject> scenePrefabs;

    [Header("Attributes")]
    public ControlScheme controlScheme = ControlScheme.KEYBOARD;

    // Use this for initialization
    void Awake ()
    {
        Services.gameController = this;
        Services.enemyController = enemyManager.GetComponent<EnemyControl>();
        Services.soundController = soundManager.GetComponent<SoundsControl>();
        Services.bulletController = bulletManager.GetComponent<BulletControl>();
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
