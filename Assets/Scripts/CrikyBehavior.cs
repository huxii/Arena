using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrikyBehavior : EnemyBehavior
{
    float angle = 0;

    // Use this for initialization
    void Start()
    {
        Init();
        InitBehaviorTree();
        PlaySound(MainControl.SoundsRef.CRIKY_CREATE);
        Services.events.Register<EnemyDestroyed>(OnEnemyDestroyed);
    }

    // Update is called once per frame
    void Update()
    {
        btree.Update(this);
        //MoveToPlayer();
        MoveAsPattern();
        //Fire();
    }

    void OnDestroy()
    {
        Services.events.QueueEvent(new EnemyDestroyed(gameObject));
        Services.events.Unregister<EnemyDestroyed>(OnEnemyDestroyed);
    }

    void OnEnemyDestroyed(Event e)
    {
        var enemyDestroyedEvent = e as EnemyDestroyed;
        speed += 0.1f;
    }

    protected override void MoveAsPattern()
    {
        Vector3 pos = shipTrans.localPosition;
        if (pos.x <= 1)
        {
            shipTrans.localPosition = new Vector3(Mathf.PingPong(Time.time, patternRadius), Mathf.PingPong(Time.time, patternRadius * Random.Range(0.8f, 1.2f)), 0);
        }
        else
        {
            shipTrans.localPosition = new Vector3(-Mathf.PingPong(Time.time, patternRadius), Mathf.PingPong(Time.time, patternRadius * Random.Range(0.8f, 1.2f)), 0);
        }
    }

    protected override void MovementUpdate()
    {

    }

    public override void ReceiveDamage()
    {
        PlaySound(MainControl.SoundsRef.CRIKY_DESTROY);
        Destroy(gameObject);
    }
}
