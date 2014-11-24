using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Transform target;

	public Transform leftGun;
	public Transform rightGun;

	public ShipMove ship;

	public GameObject blastPrefab;

	// Use this for initialization
	void Start () {
		GetTarget ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float dot = Vector3.Dot (-transform.right, target.position - transform.position);

		if (dot < 0)
		    ship.TiltSideWays (1);
		else if (dot > 0)
	        ship.TiltSideWays (-1);
	}

	void GetTarget()	{

	}

	void OnTriggerEnter(Collider c)	{
		Debug.Log ("OW FUAARK");
	}
}
