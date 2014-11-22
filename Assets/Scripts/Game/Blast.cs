using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour {
	public float moveSpeed = -100f;

	public Transform planet;

	void Start()	{
		planet = GameObject.FindGameObjectWithTag ("Planet").transform;
		Destroy (gameObject, 1.5f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.RotateAround(planet.position, transform.right, Time.fixedDeltaTime * moveSpeed);

	}
}
