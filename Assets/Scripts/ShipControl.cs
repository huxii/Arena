using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    [Header("Attributes")]
    public float moveSpeed = 1f;
    public float rotationSpeed = 1f;
    public float bulletSpeed = 10f;
    public float fireCD = 0.2f;
    public bool canFire = true;

    [Header("Debug")]
    MainControl gameController;
    Rigidbody2D rb;
    float fireCDTimer;

	// Use this for initialization
	void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainControl>();
        rb = GetComponent<Rigidbody2D>();
        fireCDTimer = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (fireCDTimer > 0)
        {
            fireCDTimer -= Time.deltaTime;
        }
        
        switch (gameController.controlScheme)
        {
            case MainControl.ControlScheme.KEYBOARD:
                KeyboardUpdate();
                break;
            case MainControl.ControlScheme.MOUSE:
                MouseUpdate();
                break;
            default:
                break;
        }
	}
    
    void MouseUpdate()
    {
        Vector3 v = rb.velocity;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        v = (mousePos - transform.position).normalized * moveSpeed;

        rb.velocity = v;

        if (canFire)
        {
            if (Input.GetMouseButton(0))
            {
                if (fireCDTimer <= 0)
                {
                    gameController.FireAt(MainControl.BulletRef.PLAYER_NORMAL, transform.position, ShipForwardDirection(), bulletSpeed);
                    fireCDTimer = fireCD;
                }
            }
        }
    }
    
    void KeyboardUpdate()
    {
        Vector3 v = rb.velocity;
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            v.x = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            v.x = -moveSpeed;
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            v.x = moveSpeed;
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
        {
            v.y = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            v.y = moveSpeed;
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            v.y = -moveSpeed;
        }

        rb.velocity = v;

        if (canFire)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (fireCDTimer <= 0)
                {
                    gameController.FireAt(MainControl.BulletRef.PLAYER_NORMAL, transform.position, ShipForwardDirection(), bulletSpeed);
                    fireCDTimer = fireCD;
                }
            }
        }
    }

    Vector3 ShipForwardDirection()
    {
        Vector3 v = new Vector3(0, 1f, 0);
        v = transform.TransformDirection(v);
        v = v.normalized;

        return v;
    }

    Vector3 ShipRightDirection()
    {
        Vector3 v = new Vector3(1f, 0, 0);
        v = transform.TransformDirection(v);
        v = v.normalized;

        return v;
    }
}

