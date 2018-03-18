using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombyBehavior : EnemyBehavior
{
    enum BossState
    {
        APPEAR,
        SPAWN,
        FIRE,
        CHASE,
    }

    readonly TaskManager taskManager = new TaskManager();

    BossState state = BossState.APPEAR;
    Vector3 location = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start ()
    {
        taskManager.Do(new Scale(gameObject, new Vector3(0.01f, 0.01f, 0.01f), new Vector3(1f, 1f, 1f), 0.5f));
    }
	
	// Update is called once per frame
	void Update ()
    {
        taskManager.Update();

        switch (state)
        {
            case BossState.APPEAR:
                if (transform.localScale.x >= 1f)
                {
                    state = BossState.SPAWN;
                }
                break;
            case BossState.SPAWN:
                break;
            case BossState.FIRE:
                break;
            case BossState.CHASE:
                break;
        }
	}
}
