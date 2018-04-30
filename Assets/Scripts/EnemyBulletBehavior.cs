using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : BulletBehavior
{

	// Use this for initialization
	void Start ()
    {
        Init();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            //other.transform.parent.gameObject.GetComponent<EnemyBehavior>().ReceiveDamage();
            Destroy(gameObject);
        }
    }
}
