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
        gameController.PlaySound(MainControl.SoundsRef.FISHYCREATE);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
        MoveAsPattern();
        Fire();
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
        gameController.PlaySound(MainControl.SoundsRef.FISHYDESTROY);
        Destroy(gameObject);
    }
}
