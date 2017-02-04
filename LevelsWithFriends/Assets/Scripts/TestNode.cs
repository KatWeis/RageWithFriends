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

	// does this tile have something on it?
	bool filled;

    //is this node the spawn point?
    bool isSpawn;

    //is this tile the level goal?
    bool isGoal;

	// Properties
	public float Width { get { return width; } }

	public float Height { get { return height; } }

	public List<GameObject> Divisions { get { return divisions; } }

	public bool Filled {
		get { return filled; } 
		set { filled = value; }
	}

    public bool IsSpawn
    {
        get { return isSpawn;}
        set { isSpawn = value; }
    }

    public bool IsGoal
    {
        get { return isGoal; }
        set { isGoal = value; }
    }

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

		filled = false;
        isGoal = false;
        isSpawn = false;
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

	public GameObject GetContainingBox(Vector3 mousePos)
	{
		// is the mouse in the AABB
		if (mousePos.x < transform.position.x + (width / 2f) && mousePos.x > transform.position.x - (width / 2f)) 
		{
			if (mousePos.y > transform.position.y - (height / 2f) && mousePos.y < transform.position.y + (height / 2f)) 
			{
				// inside this box, check for divisions
				if (divisions.Count > 0) 
				{
					// loop through each division
					for (int i = 0; i < divisions.Count; i++) 
					{
						GameObject result = divisions [i].GetComponent<TestNode> ().GetContainingBox (mousePos);

						if (result != null) {
							return result;
						}
					}
				}

				// no divisions or doesn't fit
				return gameObject;
			}
		}

		// doesn't fit
		return null;
	}

	public List<GameObject> GetAllBoxes()
	{
		List<GameObject> boxes = new List<GameObject> ();

		// get the subdivisions
		if (divisions.Count > 0) 
		{
			for (int i = 0; i < divisions.Count; i++)
			{
				boxes.AddRange (divisions [i].GetComponent<TestNode> ().GetAllBoxes ());
			}
		}

		// add this box
		boxes.Add(gameObject);

		return boxes;
	}
}
