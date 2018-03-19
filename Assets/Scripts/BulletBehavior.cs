using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    MainControl gameController;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("Bullet") && (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Boss")))
        {
            other.transform.parent.gameObject.GetComponent<EnemyBehavior>().ReceiveDamage();
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 dir, float speed)
    {
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }
}
