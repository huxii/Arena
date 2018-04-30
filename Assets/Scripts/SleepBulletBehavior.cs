using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepBulletBehavior : BulletBehavior
{
    public float sleeptDuration = 2f;

    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.transform.parent.gameObject.GetComponent<EnemyBehavior>().BeSlept(sleeptDuration);
            Destroy(gameObject);
        }
    }
}