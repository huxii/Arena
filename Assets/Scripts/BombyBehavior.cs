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

    [SerializeField]
    BossState state = BossState.APPEAR;
    Vector3 location = new Vector3(0, 0, 0);

    float timer = 0;

	// Use this for initialization
	void Start ()
    {
        Init();

        Services.tasks.Do(new Scale(gameObject, new Vector3(0.01f, 0.01f, 0.01f), new Vector3(1f, 1f, 1f), 1f));
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(hp);
        StateCheck();
        MoveAsPattern();
    }

    void StateCheck()
    {
        switch (state)
        {
            case BossState.APPEAR:
                if (transform.localScale.x >= 1f)
                {
                    state = BossState.SPAWN;
                }
                break;
            case BossState.SPAWN:
                Services.gameController.SpawnEnemy(transform.position);
                break;
            case BossState.FIRE:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Services.gameController.SpawnBullet(transform.position);
                    timer = 2f;
                }
                break;
            case BossState.CHASE:
                MoveTowardsPlayer();
                Services.gameController.SpawnEnemy(transform.position);
                break;
        }
    }

    protected override void MoveAsPattern()
    {
        Vector3 pos = shipTrans.localPosition;
        if (pos.x <= 1)
        {
            shipTrans.localPosition = new Vector3(Mathf.PingPong(Time.time, patternRadius), 0, 0);
        }
        else
        {
            shipTrans.localPosition = new Vector3(-Mathf.PingPong(Time.time, patternRadius), 0, 0);
        }
    }

    public override void ReceiveDamage()
    {
        --hp;
        if (hp == 0)
        {
            PlaySound(SoundsControl.SoundsRef.FISHY_DESTROY);
            Destroy(gameObject);
        }
        else
        if (hp <= maxHP * 0.15)
        {
            state = BossState.CHASE;
        }
        else
        if (hp <= maxHP * 0.5)
        {
            state = BossState.FIRE;
        }
    }
}
