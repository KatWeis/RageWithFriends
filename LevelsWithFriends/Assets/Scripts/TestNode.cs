using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestNode : MonoBehaviour 
{
	// Variables
	// rect
	float width;
	float height;

	// divisions in this area
	List<GameObject> divisions;

	// prefab
	GameObject prefab;

	// Properties
	public float Width { get { return width; } }

	public float Height { get { return height; } }

	public List<GameObject> Divisions { get { return divisions; } }

	// Use this for initialization
	void Start () 
	{

	}

	public void SetValues(float w, float h)
	{
		width = w;
		height = h;

		transform.localScale = new Vector3 (w, h, 1f);

		prefab = gameObject;

		divisions = new List<GameObject> (4);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Divide()
	{
		// check to see if divided
		if (divisions.Count > 0) {
			return;
		}

		// divide into four squares
		divisions.Add((GameObject)Instantiate (prefab, new Vector3 (transform.position.x - (width * 0.25f), transform.position.y + (height * 0.25f), 1f), Quaternion.identity));
		divisions.Add((GameObject)Instantiate (prefab, new Vector3 (transform.position.x + (width * 0.25f), transform.position.y + (height * 0.25f), 1f), Quaternion.identity));
		divisions.Add((GameObject)Instantiate (prefab, new Vector3 (transform.position.x - (width * 0.25f), transform.position.y - (height * 0.25f), 1f), Quaternion.identity));
		divisions.Add((GameObject)Instantiate (prefab, new Vector3 (transform.position.x + (width * 0.25f), transform.position.y - (height * 0.25f), 1f), Quaternion.identity));



		for (int i = 0; i < 4; i++) 
		{
			divisions[i].GetComponent<TestNode>().SetValues(width / 2f, height / 2f);
			if(width > 10)
			{
				divisions[i].GetComponent<TestNode>().Divide();
			}

			if(width < 20)
			{
				divisions[i].GetComponent<SpriteRenderer>().sprite = GameObject.Find("Test").GetComponent<TestGrid>().sprite;
			}
		}
	}
}
