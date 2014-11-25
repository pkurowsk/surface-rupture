using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Transform target;

	public Transform leftGun;
	public Transform rightGun;

	public ShipMove ship;

	public GameObject blastPrefab;

	float lastShot;
	
	float fireRate = 0.2f;

	float attackDist = 20f;

	int health = 2;

	// Use this for initialization
	void Start () {
		GetTarget ();
		lastShot = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float dot = Vector3.Dot (-transform.right, target.position - transform.position);

		if (Mathf.Abs (dot) > 0.01f) {
			if (dot < 0)
				ship.TiltSideWays (1);
			else if (dot > 0)
				ship.TiltSideWays (-1);
		}

		if (Vector3.Distance (transform.position, target.position) < attackDist)
			Fire ();
	}

	void Fire()	{
		if (Time.time - lastShot > fireRate) {
			lastShot = Time.time;

			GameObject b = Instantiate (blastPrefab, transform.position - transform.up * 5f, transform.rotation) as GameObject;
			b.rigidbody.AddForce ((target.position - transform.position).normalized * 10000f);
			Destroy (b.gameObject, 1.5f);
		}
	}

	void GetTarget()	{

	}

	void OnTriggerEnter(Collider c)	{
		if (c.tag.Equals ("PBlast")) {
			Debug.Log ("OW FUAARK");
			health--;
			if (health == 0)	{
				Debug.Log ("lol dead");
				Destroy(gameObject);
			}

		}
	}
}
