using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishyBehavior : EnemyBehavior
{
    float angle = 0;

    // Use this for initialization
    void Start()
    {
        Init();
        InitBehaviorTree();
        PlaySound(SoundsControl.SoundsRef.FISHY_CREATE); 
    }

    // Update is called once per frame
    void Update()
    {
        btree.Update(this);
        //MoveToPlayer();
        //MoveAsPattern();
        //Fire();
    }

    void OnDestroy()
    {
        Services.events.QueueEvent(new EnemyDestroyed(gameObject));
    }

    protected override void MoveAsPattern()
    {
        Vector3 pos = shipTrans.localPosition;
        if (pos.x <= 1)
        {
            shipTrans.localPosition = new Vector3(Mathf.PingPong(Time.time, patternRadius * Random.Range(0.8f, 1.2f)), Mathf.PingPong(Time.time, patternRadius), 0);
        }
        else
        {
            shipTrans.localPosition = new Vector3(Mathf.PingPong(Time.time, patternRadius * Random.Range(0.8f, 1.2f)), -Mathf.PingPong(Time.time, patternRadius), 0);
        }
    }

    protected override void MovementUpdate()
    {

    }

    public override void ReceiveDamage()
    {
        PlaySound(SoundsControl.SoundsRef.FISHY_DESTROY);
        --hp;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        sleepCDTimer = -1;
    }
}
