using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class SharkyBehavior : EnemyBehavior
{
    float angle = 0;

    // Use this for initialization
    void Start()
    {
        Init();
        PlaySound(MainControl.SoundsRef.SHARKY_CREATE);
    }
	
	// Update is called once per frame
	void Update()
    {
        MoveToPlayer();
        MoveAsPattern();
        Fire();
    }

    void OnDestroy()
    {
        EventManager.Instance.QueueEvent(new EnemyDestroyed(gameObject));
    }

    protected override void MoveAsPattern()
    {
        angle += Time.deltaTime * patternRadius;

        float r = 1f;
        float x = Mathf.Cos(angle) * r;
        float y = Mathf.Sin(angle) * r;
        float z = 0;

        shipTrans.localPosition = new Vector3(x, y, z);
    }

    protected override void MovementUpdate()
    {

    }

    public override void ReceiveDamage()
    {
        PlaySound(MainControl.SoundsRef.SHARKY_DESTROY);
        Destroy(gameObject);
    }
}
