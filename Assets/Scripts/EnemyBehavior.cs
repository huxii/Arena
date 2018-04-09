using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using BehaviorTree;

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
    protected MainControl gameController;
    protected GameObject player;
    protected Transform shipTrans;
    protected Tree<EnemyBehavior> btree;
    
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
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainControl>();
        shipTrans = transform.GetChild(0).transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected void InitBehaviorTree()
    {
        // We define the tree and use a selector at the root to pick the high level behavior (i.e. fight, flight or idle)
        btree = new Tree<EnemyBehavior>(new Selector<EnemyBehavior>(

            // (highest priority)
            // Flee Behavior
            new Sequence<EnemyBehavior>( // We use a sequence here since this is effectively a checklist...
                                         // Sequences fail as soon as a child fails so they're a good way to check
                                         // a bunch of conditions before doing something
                new IsInDanger(), // If the enemy has taken a lot of damage AND...
                new IsPlayerInRange(), // the player is in range...
                new Flee() // then run away
            ),

            // Fight Behavior
            // If we don't need to run then fight...
            new Sequence<EnemyBehavior>( // Another sequence to check pre-conditions
                new IsPlayerInRange(), // If the player is in range...
                new Attack() // Attack
            ),

            // (lowest priority)
            // Idle behavior
            // The idle behavior is on the bottom of list so if everything else fails we'll end up here
            new Idle()
        ));
    }

    protected void InitBossBehaviorTree()
    {
        // We define the tree and use a selector at the root to pick the high level behavior (i.e. fight, flight or idle)
        btree = new Tree<EnemyBehavior>(new Selector<EnemyBehavior>(

            // (highest priority)
            // Flee Behavior
            new Sequence<EnemyBehavior>( // We use a sequence here since this is effectively a checklist...
                                         // Sequences fail as soon as a child fails so they're a good way to check
                                         // a bunch of conditions before doing something
                new IsInDanger(), // If the enemy has taken a lot of damage AND...
                new IsPlayerInRange(), // the player is in range...
                new Flee() // then run away
            ),

            // Fight Behavior
            // If we don't need to run then fight...
            new Sequence<EnemyBehavior>( // Another sequence to check pre-conditions
                new IsPlayerInRange(), // If the player is in range...
                new Attack() // Attack
            ),

            // (lowest priority)
            // Idle behavior
            // The idle behavior is on the bottom of list so if everything else fails we'll end up here
            new Idle()
        ));
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
            gameController.FireAt(MainControl.BulletRef.ENEMY_NORMAL, shipTrans.transform.position, player.transform.position, bulletSpeed);
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
        gameController.PlaySound(idx);
    }

    protected virtual void MoveAsPattern()
    {
    }

    protected void MoveTowardsPlayer()
    {
        MoveTo(player.transform.position);
        /*
        var playerDirection = (player.transform.position - transform.position).normalized;
        var body = GetComponent<Rigidbody2D>();
        body.AddForce(playerDirection * speed, ForceMode2D.Impulse);
        */
    }

    protected void MoveAwayFromPlayer()
    {
        var fleeDirection = (transform.position - player.transform.position).normalized;
        var body = GetComponent<Rigidbody2D>();
        body.AddForce(fleeDirection * speed, ForceMode2D.Impulse);
    }

    protected virtual void MovementUpdate()
    {
    }

    public virtual void ReceiveDamage()
    {
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // NODES
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ////////////////////
    // Conditions
    ////////////////////
    private class IsInDanger : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            //return enemy._health < MaxHealth / 4;
            return false;
        }
    }

    private class IsPlayerInRange : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            var playerPos = enemy.player.transform.position;
            var enemyPos = enemy.transform.position;
            return Vector3.Distance(playerPos, enemyPos) < enemy.detectionRange;
        }
    }

    ///////////////////
    /// Actions
    ///////////////////
    private class Flee : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            //enemy.SetColor(Color.yellow);
            enemy.MoveAwayFromPlayer();
            return true;
        }
    }

    private class Attack : Node<EnemyBehavior>
    {
        public override bool Update(EnemyBehavior enemy)
        {
            //enemy.SetColor(Color.red);
            enemy.MoveTowardsPlayer();
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