using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    public enum ControlScheme
    {
        KEYBOARD = 1,
    };

    [Header("Prefabs")]
    public GameObject bulletPrefab;

    [Header("Attributes")]
    public ControlScheme controlScheme = ControlScheme.KEYBOARD;
    public float moveSpeed = 1f;
    public float rotationSpeed = 1f;
    public float bulletSpeed = 10f;
    public float fireCD = 0.2f;

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
        if (controlScheme == ControlScheme.KEYBOARD)
        {
            KeyboardRotationUpdate();
        }

        if (fireCDTimer > 0)
        {
            fireCDTimer -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (fireCDTimer <= 0)
            {
                gameController.Fire(bulletPrefab, transform.position, ShipForwardDirection(), bulletSpeed);
                fireCDTimer = fireCD;
            }
        }
	}
    /*
    void MouseRotationUpdate()
    {
        // rotate 
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = this.transform.position;
        float x = mousePos.x - pos.x;
        float y = mousePos.y - pos.y;
        float z = Mathf.Sqrt(x * x + y * y);
        float angle = Mathf.Round(Mathf.Asin(Mathf.Abs(y / z)) / Mathf.PI * 180);

        if (x < 0)
        {
            if (y > 0)
            {
                angle = 90 - angle;
            }
            else
            {
                angle = angle + 90;
            }
        }
        else
        {
            if (y < 0)
            {
                angle = 270 - angle;
            }
            else
            {
                angle = 270 + angle;
            }
        }

        this.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    */
    void KeyboardRotationUpdate()
    {
        float deg = transform.localEulerAngles.z;
        if (Input.GetKey(KeyCode.A))
        {
            deg += rotationSpeed;
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            deg -= rotationSpeed;
        }
        transform.localEulerAngles = new Vector3(0, 0, deg);

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
        {
            rb.velocity = new Vector2(0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = moveSpeed * ShipForwardDirection();
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -moveSpeed * ShipForwardDirection();
        }
    }

    Vector3 ShipForwardDirection()
    {
        Vector3 v = new Vector3(0, 1f, 0);
        v = transform.TransformDirection(v);
        v = v.normalized;

        return v;
    }
}

