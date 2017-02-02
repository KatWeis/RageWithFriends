using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum GameState
    {
        Build,
        MainMenu,
        Test,
        Pause,
        Play,
        GameOver
    }

    public enum Tool
    {
        Place,
        Remove,
        ColorChanger,
        Move,
        None
    }

    //fields
    public GameState gameState = GameState.MainMenu; //set default to be main menu ---public for now, likely change later
    public Tool currentTool = Tool.None; //set default to none ---public for now, likely change later

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //update the gamestate based on input
        UpdateState();
        //only allow switching of tools in build mode
        if(gameState == GameState.Build)
        {
            UpdateTool();
        }
        else //ensure no tools are equipped when in other modes
        {
            currentTool = Tool.None;
        }
	}

    private void UpdateState()
    {
        //allow switching between states, start with it being triggered by keyboard input
        switch (gameState)
        {
            case GameState.MainMenu:
                {
                    if(Input.GetKeyDown(KeyCode.P))
                    {
                        gameState = GameState.Play;
                    }
                    else if(Input.GetKeyDown(KeyCode.B))
                    {
                        gameState = GameState.Build;
                    }
                }
                break;
            case GameState.Build:
                {
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        gameState = GameState.Test;
                    }
                    else if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        gameState = GameState.MainMenu;
                    }
                }
                break;
            case GameState.Play:
                {
                    if (Input.GetKeyDown(KeyCode.O))////////////////////placeholder for dying/losing all lives
                    {
                        gameState = GameState.GameOver;
                    }
                    else if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        gameState = GameState.Pause;
                    }
                }
                break;
            case GameState.Pause:
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        gameState = GameState.Play;
                    }
                    else if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        gameState = GameState.MainMenu;
                    }
                }
                break;
            case GameState.Test:
                {
                    if (Input.GetKeyDown(KeyCode.B))//////////////no sure exactly how this would work, ability to go from test to build, but should it also go to main menu or play?
                    {
                        gameState = GameState.Build;
                    }
                    /*else if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        gameState = GameState.MainMenu;
                    }*/
                }
                break;
            case GameState.GameOver:
                {
                    if (Input.GetKeyDown(KeyCode.P))//resets level (i assume)
                    {
                        gameState = GameState.Play;
                    }
                    else if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        gameState = GameState.MainMenu;
                    }
                }
                break;
        }
    }

    private void UpdateTool()
    {
        //allow the user to switch to different tools with hotkeys -- will change later, this is just for time being
        if(Input.GetKeyDown(KeyCode.M))
        {
            currentTool = Tool.Move;
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            currentTool = Tool.ColorChanger;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            currentTool = Tool.Place;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            currentTool = Tool.Remove;
        }
    }

    public void OnGUI()
    {
        //may be using canvas UI stuff instead...
    }
}
