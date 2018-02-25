using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class EnemyDestroyed : GM.Event
{
    public readonly GameObject enemy;
    public EnemyDestroyed(GameObject e)
    {
        enemy = e;
    }
}

public class EnemyBehavior : MonoBehaviour
{
    [Header("Attributes")]
    public float detectionRange = 5f;
    public float speed = 2f;
    public float patternRadius = 1f;
    public float bulletSpeed = 10f;
    public float fireCD = 2f;

    [Header("Debug")]
    [SerializeField]
    protected GameObject player;
    protected Transform shipTrans;
    protected EnemyControl enemyController;

    float fireCDTimer = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDestroy()
    {
        EventManager.Instance.QueueEvent(new EnemyDestroyed(gameObject));
        EventManager.Instance.Unregister<EnemyDestroyed>(OnEnemyDestroyed);
    }

    protected void Init()
    {
        shipTrans = transform.GetChild(0).transform;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyController = GameObject.FindGameObjectWithTag("GameController").GetComponent<EnemyControl>();

        EventManager.Instance.Register<EnemyDestroyed>(OnEnemyDestroyed);
    }

    protected void MoveTo(Vector3 des)
    {
        Vector3 dir = des - transform.position;
        Vector3 mov = dir.magnitude * dir.normalized;
        transform.Translate(mov * Time.deltaTime * speed);
    }

    protected void MoveToPlayer()
    {
        MoveTo(player.transform.position);
    }

    protected bool PlayerDetected()
    {
        Vector3 dir = player.transform.position - transform.position;
        Debug.Log(dir.magnitude);
        if (dir.magnitude < detectionRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void Fire()
    {
        if (fireCDTimer <= 0)
        {
            enemyController.FireAt(MainControl.BulletRef.ENEMY_NORMAL, shipTrans.transform.position, player.transform.position, bulletSpeed);
            fireCDTimer = fireCD;
        }
        else
        if (fireCDTimer > 0)
        {
            fireCDTimer -= Time.deltaTime;
        }
    }

    protected void PlaySound(MainControl.SoundsRef idx)
    {
        enemyController.PlaySound(idx);
    }

    protected virtual void MoveAsPattern()
    {
    }

    protected virtual void MovementUpdate()
    {
    }

    protected virtual void OnEnemyDestroyed(GM.Event e)
    {

    }

    public virtual void ReceiveDamage()
    {
    }
}