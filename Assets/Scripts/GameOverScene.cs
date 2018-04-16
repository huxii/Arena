using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene : Scene<TransitionData>
{
    public GameObject scoreText;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void RetartGame()
    {
        Services.scenes.Swap<GameMenuScene>(new TransitionData());
    }

    protected override void OnEnter(TransitionData data)
    {
        scoreText.GetComponent<TextMesh>().text = data.score.ToString();
    }
}
