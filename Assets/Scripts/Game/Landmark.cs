using UnityEngine;
using System.Collections;

public class Landmark : MonoBehaviour {
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
			Destroy(c.transform.parent.gameObject);
			Debug.Log ("Hit");
			health--;
			depleted++;
			if (depleted >= tierSize)	{
				Debug.Log("BOOM Tier down");

				GameControllerSingleton.GetInstance().LandmarkTierDown(type);
				depleted = 0;

				if (health < tierSize)
					Destroy(gameObject);
			}
		}
	}

}
