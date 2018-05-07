using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class EnemyDestroyed : Event
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
    public float maxHP = 10;
    public float fleeRange = 4f;
    public float attackRange = 6f;
    public float speed = 2f;
    public float patternRadius = 1f;
    public float bulletSpeed = 10f;
    public float fireCD = 2f;
    public bool canFire = true;

    [Header("Debug")]
    [SerializeField]
    protected GameObject player;
    protected Transform shipTrans;
    protected Tree<EnemyBehavior> btree;
    protected float hp = 0;
    protected float sleepCDTimer = -1;

    float fireCDTimer = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected void Init()
    {
        shipTrans = transform.GetChild(0).transform;
        player = GameObject.FindGameObjectWithTag("Player");
        hp = maxHP;
    }

    protected void InitBehaviorTree()
    {
        // We define the tree and use a selector at the root to pick the high level behavior (i.e. fight, flight or idle)
        btree = new Tree<EnemyBehavior>(new Selector<EnemyBehavior>(

            // (highest priority)
            // Fight Behavior
            // If we don't need to run then fight...

            new Sequence<EnemyBehavior>(
                new Slept(),
                new Sleeping()
            ),

            // Flee Behavior
            new Sequence<EnemyBehavior>( // We use a sequence here since this is effectively a checklist...
                                         // Sequences fail as soon as a child fails so they're a good way to check
                                         // a bunch of conditions before doing something
                new ShouldFlee(),
                new Flee()              // then run away
            ),

            new Sequence<EnemyBehavior>(
                new ShouldApproach(),
                new Approach()
            ),

            new Sequence<EnemyBehavior>( // Another sequence to check pre-conditions
                new CanAttack(),        // If the player is in range...
                new Attack()            // Attack
            ),

            // (lowest priority)
            // Idle behavior
            // The idle behavior is on the bottom of list so if everything else fails we'll end up here
            new Idle()
        ));
    }

    /*
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
    */

    protected void MoveTowardsPlayer()
    {
        var playerDirection = (player.transform.position - transform.position).normalized;
        var body = GetComponent<Rigidbody2D>();
        body.MovePosition(transform.position + playerDirection * speed * Time.deltaTime);
    }

    protected void MoveAwayFromPlayer()
    {
        //var fleeDirection = (transform.position - player.transform.position).normalized;
        var fleeDirection = new Vector3(0, 1, 0);
        float angle = Random.Range(-90f, 90f);
        fleeDirection = Quaternion.Euler(0, 0, angle) * fleeDirection;
        var body = GetComponent<Rigidbody2D>();
        body.AddForce(fleeDirection * speed, ForceMode2D.Impulse);
    }

    protected float PlayerDistance()
    {
        Vector3 dir = player.transform.position - transform.position;
        return dir.magnitude;
    }

    protected void Fire()
    {
        if (fireCDTimer <= 0)
        {
            Services.bulletController.FireAt(BulletControl.BulletRef.ENEMY_NORMAL, transform.position, player.transform.position - transform.position, bulletSpeed);
            fireCDTimer = fireCD;
        }
        else
        if (fireCDTimer > 0)
        {
            fireCDTimer -= Time.deltaTime;
        }
    }

    protected void PlaySound(SoundsControl.SoundsRef idx)
    {
        Services.soundController.Play((int)idx);
    }

    protected virtual void MoveAsPattern()
    {
    }

    protected virtual void MovementUpdate()
    {
    }

    public void BeSlept(float duration)
    {
        sleepCDTimer = duration;
    }

    public virtual void ReceiveDamage()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.name);
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // NODES
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////
    // Conditions
    ////////////////////
    private class Slept : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            if (enemy.sleepCDTimer > 0)
            {
                //Debug.Log(enemy.name);
            }
            return enemy.sleepCDTimer > 0;
        }
    }

    private class ShouldFlee : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            return enemy.PlayerDistance() < enemy.fleeRange;
        }
    }

    private class ShouldApproach : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            return enemy.PlayerDistance() > enemy.attackRange;
        }
    }

    private class CanAttack : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            return enemy.PlayerDistance() < enemy.attackRange;
        }
    }

    ///////////////////
    /// Actions
    ///////////////////
    ///

    private class Sleeping : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            enemy.sleepCDTimer -= Time.deltaTime;
            return true;
        }
    }

    private class Flee : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            //Debug.Log("flee");
            //enemy.SetColor(Color.yellow);
            enemy.MoveAwayFromPlayer();
            enemy.MoveAsPattern();
            return true;
        }
    }

    private class Approach : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            //Debug.Log("approach");
            //enemy.SetColor(Color.yellow);
            enemy.MoveTowardsPlayer();
            enemy.MoveAsPattern();
            return true;
        }
    }

    private class Attack : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            //enemy.SetColor(Color.red);
            if (enemy.canFire)
            {
                enemy.Fire();
            }
            enemy.MoveAsPattern();
            return true;
        }
    }

    private class Idle : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            //enemy.SetColor(Color.blue);
            return true;
        }
    }
}