using UnityEngine;
using System.Collections;

public class Landmark : MonoBehaviour {
	public GameController gameController;

	[Range(0, 100)]
	public int health = 100;

	int tierSize = 33;
	int depleted = 0;

	public GameController.LandmarkType type;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider c)	{
		if (c.tag.Equals ("EBlast")) {
			Debug.Log ("Hit");
			health--;
			depleted++;
			if (depleted >= tierSize)	{
				gameController.LandmarkTierDown(type);
				depleted = 0;

				if (health < tierSize)
					Destroy(gameObject);
			}
		}
	}

}
