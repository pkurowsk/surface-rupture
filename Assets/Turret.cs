using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {
	public GameObject blastPrefab;
	public Transform target;

	float attackRange = 30f;
	float lastShot;
	float fireRate = 0.2f;

	// Use this for initialization
	void OnEnable () {
		lastShot = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null)
			FindTarget();

		if (target != null)	{
			if (Vector3.Distance (target.position, transform.position) <= attackRange)
				Fire ();
			else
				FindTarget();
		}


	}

	void OnTriggerEnter(Collider c)	{
		Debug.Log (c.gameObject.name);
		if (c.gameObject.tag.Equals ("Enemy")) {
			target = c.transform;
		}
	}

	void FindTarget()	{
		Transform enemies = GameObject.Find ("Enemies").transform;
		for (int i = 0; i < enemies.childCount; i++) {
			if (Vector3.Distance (enemies.GetChild (i).position, transform.position) < attackRange) {
				target = enemies.GetChild (i);
				break;
			}
		}
	}

	void Fire()	{
		if (Time.time - lastShot > fireRate) {
			lastShot = Time.time;
			
			GameObject b = Instantiate (blastPrefab, transform.position + transform.up * 2f, transform.rotation) as GameObject;
			b.rigidbody.AddForce ((target.position - b.transform.position).normalized * 2500f);
			Destroy (b.gameObject, 1.5f);
		}
	}


}
