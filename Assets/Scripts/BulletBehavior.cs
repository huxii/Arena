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
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
    }

    protected void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainControl>();
    }

    public void SetDirection(Vector3 dir, float speed)
    {
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }
}
