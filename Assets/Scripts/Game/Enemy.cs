using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Transform target;

	public ShipMove ship;

	public GameObject blastPrefab;

	float lastShot;
	
	const float fireRate = 0.2f;

	const float attackDist = 20f;

	int health = 2;

	// Use this for initialization
	void Start () {
		lastShot = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (target == null)
			target = GameControllerSingleton.GetInstance().FindTarget(transform.position);
		if (target == null)
			return;

		float dot = Vector3.Dot(transform.right, transform.position - target.position);
		if (dot > 0.1) {
			ship.TiltSideWays (-1);
		}
		else if (dot < -0.1) {
			ship.TiltSideWays (1);
		}

		if (Vector3.Distance (transform.position, target.position) < attackDist) {
			Fire ();
			ship.Brake ();
		}
		else {
			ship.NormalSpeed();
		}
	}

	Vector3 GetTurnAngle()	{
		// Plane defined by (normal to plane) forward and position 
		Vector3 toTarget = target.position - transform.position;
		float dist = Vector3.Dot (toTarget, transform.position.normalized);
		Vector3 planePoint = target.position - dist * transform.position.normalized;

		return (planePoint - transform.position);
	}

	void Fire()	{
		if (Time.time - lastShot > fireRate) {
			lastShot = Time.time;

			GameObject b = Instantiate (blastPrefab, transform.position - transform.up * 5f, transform.rotation) as GameObject;
			b.rigidbody.AddForce ((target.position - b.transform.position).normalized * 10000f);
			Destroy (b.gameObject, 1.5f);
		}
	}

	void OnTriggerEnter(Collider c)	{
		if (c.tag.Equals ("PBlast")) {
			health--;
			if (health == 0)	{
				Destroy(gameObject);
			}

		}
	}
}
