using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestGrid : MonoBehaviour 
{
	// root node
	GameObject root;
	TestNode rootN;

	// prefab
	public GameObject prefab;

	//sprite
	public Sprite sprite;
	public Sprite block;
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
	}
	
	// Update is called once per frame
	void Update () 
	{
        //get current tool
        currentTool = sM.CurrentTool;

        mousePos = UpdateMouse ();
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
		if (Input.GetMouseButtonDown (0)) 
		{
            Debug.Log(currentTool + "");
			Place (current);
			current.GetComponent<TestNode> ().Filled = true;
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
		current.GetComponent<SpriteRenderer> ().sprite = block;
	}

}
