using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class CrikyBehavior : EnemyBehavior
{
    float angle = 0;

    // Use this for initialization
    void Start()
    {
        Init();
        PlaySound(MainControl.SoundsRef.CRIKY_CREATE);
        EventManager.Instance.Register<EnemyDestroyed>(OnEnemyDestroyed);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
        //MoveAsPattern();
        Fire();
    }

    void OnDestroy()
    {
        EventManager.Instance.QueueEvent(new EnemyDestroyed(gameObject));
        EventManager.Instance.Unregister<EnemyDestroyed>(OnEnemyDestroyed);
    }

    void OnEnemyDestroyed(GM.Event e)
    {
        var enemyDestroyedEvent = e as EnemyDestroyed;
        speed += 0.1f;
    }

    protected override void MoveAsPattern()
    {
        Vector3 pos = shipTrans.localPosition;
        if (pos.x <= 1)
        {
            shipTrans.localPosition = new Vector3(Mathf.PingPong(Time.time, patternRadius), Mathf.PingPong(Time.time, patternRadius * Random.Range(0.8f, 1.2f)), pos.z);
        }
        else
        {
            shipTrans.localPosition = new Vector3(-Mathf.PingPong(Time.time, patternRadius), Mathf.PingPong(Time.time, patternRadius * Random.Range(0.8f, 1.2f)), pos.z);
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
