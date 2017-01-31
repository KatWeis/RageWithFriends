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

	// camera
	public Camera cam;

	// Use this for initialization
	void Start () 
	{
		root = (GameObject)Instantiate (prefab, Vector3.zero, Quaternion.identity);
		rootN = root.GetComponent<TestNode> ();
		rootN.SetValues (80, 80);
		rootN.Divide ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// get the mouse pos
		Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

		// find the containing box
		GameObject current = rootN.GetContainingBox(mousePos);

		// change the color
		if (current != null) 
		{
			current.GetComponent<SpriteRenderer> ().color = Color.red;
		}
		foreach (GameObject box in rootN.GetAllBoxes()) 
		{
			if (box != current) 
			{
				box.GetComponent<SpriteRenderer>().color = Color.white;
			}
		}
	}


}
