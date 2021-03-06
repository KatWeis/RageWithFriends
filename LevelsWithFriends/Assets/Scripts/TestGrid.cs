﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestGrid : MonoBehaviour 
{
	// root node
	GameObject root;
	TestNode rootN;

	// prefab
	public GameObject prefab;

	// character
	Player player;

	//sprite
	public Sprite sprite;
	public Sprite block;
    public Sprite floor;//floor sprite
    public Sprite end;//sprite for goal of the level
    public Sprite start;//sprite for where the player starts the level
    //sprite to temporarily store the moved tile
    private Sprite moveTemp;

    //booleans
    private bool moveSelected;//determines if the user has selected a tile to move
    private bool placed;

    // camera
    public Camera cam;

	// mouse position
	Vector3 mousePos;

	// current selected tile
	GameObject currentTile;

    //reference to scenemanager script
    private StateManager sM;

    //current tool to use
    private Tool currentTool;

    //current gamestate / screen the game is on
    private GameState gameState;

	// Use this for initialization
	void Start () 
	{
		root = (GameObject)Instantiate (prefab, Vector3.zero, Quaternion.identity);
		rootN = root.GetComponent<TestNode> ();
		rootN.SetValues (80, 80);
		rootN.Divide ();
		mousePos = Vector3.zero;
		currentTile = null;

        //initialize script
        sM = GameObject.Find("SceneManager").GetComponent<StateManager>();

        //initialize move xelected to false
        moveSelected = false;
        placed = false;

		player = GameObject.Find ("Character").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
        //get current tool
        currentTool = sM.CurrentTool;
        //get current gameState
        gameState = sM.GState;
		AnalyzeState (gameState);
        //update the mouse position as long as the game isn't in play mode
		if (gameState != GameState.Play)
		{
			mousePos = UpdateMouse ();
		}
		currentTile = CurrentBox (mousePos);  //has issue if you click off grid since current tile is undefined
		DrawTile (currentTile);
	}

	// gets the current mouse position
	public Vector3 UpdateMouse()
	{
		// get the mouse pos
		return cam.ScreenToWorldPoint (Input.mousePosition);
	}

	// gets the current hovered box
	public GameObject CurrentBox(Vector3 mouse)
	{
		// find the containing box
		GameObject current = rootN.GetContainingBox(mouse);
        if(currentTool == Tool.Move && Input.GetMouseButtonDown(0))
        {
            if (current != null)//makes sure that it is a node
                Move(current);
        }
        if (Input.GetMouseButtonDown(0))
        {
            switch (currentTool)
            {
                case Tool.PlaceFloor:
                    {
                        //if the current block is already the same as the place tool, remove it
                        if (current.GetComponent<SpriteRenderer>().sprite == floor)
                        {
                            Remove(current); placed = true;
                        }
                    }
                    break;
                case Tool.PlacePlat:
                    {
                        //if the current block is already the same as the place tool, remove it
                        if (current.GetComponent<SpriteRenderer>().sprite == block)
                        {
                            Remove(current); placed = true;
                        }
                    }
                    break;
                case Tool.PlaceEnd:
                    {
                        //if the current block is already the same as the place tool, remove it
                        if (current.GetComponent<SpriteRenderer>().sprite == end)
                        {
                            Remove(current); placed = true;
                        }
                    }
                    break;
                case Tool.PlaceStart:
                    {
                        //if the current block is already the same as the place tool, remove it
                        if (current.GetComponent<SpriteRenderer>().sprite == start)
                        {
                            Remove(current); placed = true;
                        }
                    }
                    break;
            }
        }
        if (Input.GetMouseButton (0) && placed == false) 
		{
            //debugging current tool
            //Debug.Log(currentTool + "");

            //determine what tool to use
            if(currentTool == Tool.Remove)
            {
				if (current != null) 
                Remove(current);
            }
            /*else if(currentTool == Tool.Move)
            {
				if (current != null) 
                Move(current);
            }*/
			else if(currentTool == Tool.PlaceFloor || currentTool == Tool.PlaceEnd || currentTool == Tool.PlaceStart || currentTool == Tool.PlacePlat)
            {
				if (current != null) 
				{
					Place (current);
					current.GetComponent<TestNode> ().Filled = true;
					if(current.tag != "PlayerSpawn")
					current.AddComponent<BoxCollider2D>();
				}
            }
		}
        if(Input.GetMouseButtonUp(0))
        {
            //if the user releases button reset placed
            placed = false;
        }
        

		return current;
	}

	// Displays the correct sprite/color for each tile
	public void DrawTile(GameObject current)
	{
		foreach (GameObject box in rootN.GetAllBoxes()) 
		{
			box.GetComponent<SpriteRenderer> ().color = Color.white;

			// highlight the current tile
			if (box == current) 
			{
				box.GetComponent<SpriteRenderer> ().color = Color.red;
			}
		}
	}

	// places a tile on a box
	public void Place(GameObject current)
	{
        switch(currentTool)
        {
            case Tool.PlaceFloor: current.GetComponent<SpriteRenderer>().sprite = floor;
                break;
            case Tool.PlacePlat: current.GetComponent<SpriteRenderer>().sprite = block;
                break;
            case Tool.PlaceEnd: current.GetComponent<SpriteRenderer>().sprite = end; current.GetComponent<TestNode>().IsGoal = true; current.tag = "LevelGoal";
                break;
            case Tool.PlaceStart: current.GetComponent<SpriteRenderer>().sprite = start; current.GetComponent<TestNode>().IsSpawn = true; current.tag = "PlayerSpawn";
                break;
        }
	}

    public void ClearLevel()
    {
        foreach (GameObject box in rootN.GetAllBoxes())
        {
            //set them all to white / no tint
            box.GetComponent<SpriteRenderer>().color = Color.white;

            //set all sprites to defaults
			box.GetComponent<TestNode>().Reset();
        }

		player.HaveSpawned = false;
    }

    private void Remove(GameObject current)
    {
        //change the tile's sprite to empty
        current.GetComponent<SpriteRenderer>().sprite = sprite;
        //make sure that all tags are removed so that they don't have any special properties
		if (current.tag == "LevelGoal" || current.tag == "PlayerSpawn") 
		{
			current.tag = "Untagged";
		}
        current.GetComponent<TestNode>().Filled = false;
        //get rid of the collider on this node
        Destroy(current.GetComponent<Collider>());
    }

    private void Move(GameObject current)
    {
        //Debug.Log("moved");
        if (moveSelected == true)
        {
            //place down the selected tile
            current.GetComponent<SpriteRenderer>().sprite = moveTemp;
            current.GetComponent<TestNode>().Filled = true;
            current.AddComponent<BoxCollider2D>();
            //reset the bool to track picking a tile to move
            moveSelected = false;
            //make sure the correct tag is implemented
			if (moveTemp == end) 
			{
				current.tag = "LevelGoal";
			}
			if (moveTemp == start) 
			{
				current.tag = "PlayerSpawn";
			}
        }
        else if(current.GetComponent<TestNode>().Filled == true)//make sure that the node isn't empty before moving it
        {
            //store the sprite
            moveTemp = current.GetComponent<SpriteRenderer>().sprite;
            //remove that tile from the current box
            Remove(current);
            //mark that you have selected a tile to move
            moveSelected = true;
        }
    }

	public void AnalyzeState(GameState gs)
	{
		switch (gameState)
		{
		case GameState.MainMenu:
			{
				foreach (GameObject box in rootN.GetAllBoxes()) 
				{
					box.SetActive (false);
				}
			}
			break;
		case GameState.Build:
			{
				foreach (GameObject box in rootN.GetAllBoxes()) 
				{
					box.SetActive (true);
				}
			}
			break;
		case GameState.Play:
			{
				foreach (GameObject box in rootN.GetAllBoxes()) 
				{
					if (box.GetComponent<TestNode> ().Filled == false)
					{
						box.SetActive (false);
					} 
					else 
					{
						box.SetActive (true);
					}
				}
			}
			break;
		case GameState.Pause:
			{
				foreach (GameObject box in rootN.GetAllBoxes()) 
				{
					box.SetActive (false);
				}
			}
			break;
		case GameState.GameOver:
			{
				foreach (GameObject box in rootN.GetAllBoxes()) 
				{
					box.SetActive (false);
				}
			}
			break;
		case GameState.WinGame:
		{
			foreach (GameObject box in rootN.GetAllBoxes()) 
			{
				box.SetActive (false);
			}
		}
		break;
	}
	}
}
