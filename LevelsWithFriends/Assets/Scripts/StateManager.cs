using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    PlaceFloor,
    PlacePlat,
    PlaceStart,
    PlaceEnd,
    Remove,
    ColorChanger,
    Move,
    None
}

public class StateManager : MonoBehaviour
{


    //fields
    private GameState gameState; 
    private Tool currentTool;

    //properties
    public GameState GState
    {
        get { return gameState; }
    }
    public Tool CurrentTool
    {
        get { return currentTool; }
    }

    // Use this for initialization
    void Start ()
    {
        gameState = GameState.MainMenu; //set default to be main menu
        currentTool = Tool.None; //set default to none
    }
	
	// Update is called once per frame
	void Update ()
    {
        ////////////////////////////////temp
        gameState = GameState.Build;

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
            currentTool = Tool.PlacePlat;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            currentTool = Tool.Remove;
        }
    }

    public void ToolChange(int toUse)
    {
        switch(toUse)
        {
            case 1: currentTool = Tool.Move;
                break;
            case 2:
                currentTool = Tool.PlaceFloor;
                break;
            case 3:
                currentTool = Tool.PlaceEnd;
                break;
            case 4:
                currentTool = Tool.PlacePlat;
                break;
            case 5:
                currentTool = Tool.PlaceStart;
                break;
            case 6:
                currentTool = Tool.Remove;
                break;
            case 7:
                currentTool = Tool.ColorChanger;
                break;
        }
    }

    public void StateChange(int state)
    {
        switch (state)
        {
            case 1: gameState = GameState.Play;
                break;
            
        }
    }
}
