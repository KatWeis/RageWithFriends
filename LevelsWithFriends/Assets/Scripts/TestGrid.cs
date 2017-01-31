using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestGrid : MonoBehaviour 
{
	// root node
	GameObject root;

	// prefab
	public GameObject prefab;

	//sprite
	public Sprite sprite;
	// Use this for initialization
	void Start () 
	{
		root = (GameObject)Instantiate (prefab, Vector3.zero, Quaternion.identity);
		root.GetComponent<TestNode> ().SetValues (80, 80);
		root.GetComponent<TestNode> ().Divide ();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
