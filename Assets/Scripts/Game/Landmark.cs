using UnityEngine;
using System.Collections;

public class Landmark : MonoBehaviour {
	public enum LandmarkType	{
		Military,
		Spiritual,
		Economic
	};

	[Range(0, 100)]
	public int health = 100;

	int tierSize = 33;

	public LandmarkType type;

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider c)	{
		Debug.Log ("Hit");
	}

}
