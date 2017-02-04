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
    GameOver,
    WinGame
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

    //ref to ui for each state
    public GameObject build;
    public GameObject play;
    public GameObject menu;
    public GameObject pause;
    public GameObject lose;
    public GameObject win;

	// character
	private GameObject character;
    //properties
    public GameState GState
    {
        get { return gameState; }
        set { gameState = value; }
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
		character = GameObject.Find ("Character");
    }
	
	// Update is called once per frame
	void Update ()
    {
        ////////////////////////////////temp
        //gameState = GameState.Build;

        //update the gamestate based on input
        UpdateState();

        //make sure that the UI is correct for the given gamestate
        ToggleUI();

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
		if (gameState != GameState.Play)
			character.SetActive (false);
		else
			character.SetActive (true);
        //allow switching between states, start with it being triggered by keyboard input
        switch (gameState)
        {
            case GameState.Build:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        gameState = GameState.MainMenu;
                    }
                }
                break;
            case GameState.Play:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
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
            case GameState.GameOver:
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
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
        else if (Input.GetKeyDown(KeyCode.P))
        {
            currentTool = Tool.PlacePlat;
        }
        else if (Input.GetKeyDown(KeyCode.R))
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
            case 2: gameState = GameState.Build;
                break;
            case 3: gameState = GameState.MainMenu;
                break;
            case 4: gameState = GameState.Pause;
                break;

        }
    }

    private void ToggleUI()
    {
        //set all to false
        build.SetActive(false);
        play.SetActive(false);
        menu.SetActive(false);
        pause.SetActive(false);
        lose.SetActive(false);
        win.SetActive(false);

        //turn on the one corresponding to the current gamestate
        switch (gameState)
        {
            case GameState.Play: play.SetActive(true);
                break;
            case GameState.MainMenu: menu.SetActive(true);
                break;
            case GameState.Build: build.SetActive(true);
                break;
            case GameState.Pause: pause.SetActive(true);
                break;
            case GameState.GameOver: lose.SetActive(true);
                break;
            case GameState.WinGame: win.SetActive(true);
                break;
        }
    }
}
