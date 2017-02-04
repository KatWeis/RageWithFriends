using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //fields
    private bool haveSpawned;
    //ref to statemanager script
    private StateManager sM;

	// Use this for initialization
	void Start ()
    {
        sM = GameObject.Find("SceneManager").GetComponent<StateManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //spawn player at the starting point
		if(haveSpawned == false && sM.GState == GameState.Play)
        {
            gameObject.transform.position = GameObject.FindWithTag("PlayerSpawn").transform.position;
            haveSpawned = true;
        }

		if (transform.position.y < -44) 
		{
			sM.GState = GameState.GameOver;
			haveSpawned = false;
		}
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "LevelGoal")
        {
            sM.GState = GameState.WinGame;
            //Debug.Log("win?");
        }
    }
}
