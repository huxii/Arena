using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : Scene<TransitionData>
{
    public GameObject scoreText;

    int point;

	// Use this for initialization
	void Start ()
    {
        Debug.Log(point);
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnEnemyDestroyed(Event e)
    {
        var enemyDestroyedEvent = e as EnemyDestroyed;
        
        if (scoreText)
        {
            ++point;
            scoreText.GetComponent<TextMesh>().text = point.ToString();

            if (point >= 5)
            {
                ClearEnemy();
                Services.scenes.Swap<GameOverScene>(new TransitionData(point));
            }
        }
    }

    public void ClearEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    protected override void OnEnter(TransitionData data)
    {
        ClearEnemy();  
        Services.events.Register<EnemyDestroyed>(OnEnemyDestroyed);
        
        point = -1;
        scoreText.GetComponent<TextMesh>().text = point.ToString();
    }

    protected override void OnExit()
    {
        Services.events.Unregister<EnemyDestroyed>(OnEnemyDestroyed);
        ClearEnemy();
    }
}
