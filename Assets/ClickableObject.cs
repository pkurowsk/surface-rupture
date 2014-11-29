using UnityEngine;
using System.Collections;

public class ClickableObject : MonoBehaviour {

	// Use this for initialization
	void OnMouseDown()	{
		Transform planets = GameObject.Find ("Planets").transform;

		for (int i = 0; i < planets.childCount; i++) {
			planets.GetChild(i).GetComponentInChildren<Animator>().enabled = false;
		}

		GetComponent<Animator> ().enabled = true;

		GameControllerSingleton.planet = GetComponent<Planet> ();
	}
}
