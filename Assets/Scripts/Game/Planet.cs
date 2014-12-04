using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

	public GameController.LandmarkType[] planetRanking;
	public int[] moraleValues;

	public string planetName;
	public string planetStory;

	public Material material;

	// Use this for initialization
	void OnMouseDown()	{
		GameObject planets = GameObject.Find ("Planets");

		for (int i = 0; i < planets.transform.childCount; i++) 
			planets.transform.GetChild(i).GetComponentInChildren<Animator>().enabled = false;
		GetComponent<Animator> ().enabled = true;
		
		GameControllerSingleton.planet = GetComponent<Planet> ();
	}
}
