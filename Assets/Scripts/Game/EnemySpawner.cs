using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;

	public GameController gameController;

	public Transform warpHole;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		warpHole.RotateAround (warpHole.up, Time.fixedDeltaTime * -0.5f);
	}
}
