using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;

	public GameController gameController;

	public Transform warpHole;

	public Transform planet;

	public Transform landmarks;

	public Image enemyMapIcon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		warpHole.Rotate(warpHole.up, Time.fixedDeltaTime * -0.5f);
	}

	public void spawnWave(int waveNum)	{
		StartCoroutine(spawnEnemies (10));
	}

	IEnumerator spawnEnemies(int enemies)	{
		for (int i = 0; i < enemies; i++) {
			GameObject enemy = Instantiate (enemyPrefab, transform.position.normalized * 75.5f, enemyPrefab.transform.rotation) as GameObject;
			enemy.GetComponent<ShipMove>().planet = planet;
			enemy.GetComponent<Enemy>().target = FindTarget();

			enemy.GetComponent<ShipMove>().shipMapIcon = (Image.Instantiate(enemyMapIcon) as Image).rectTransform;
			if (enemy.GetComponent<ShipMove>().shipMapIcon != null)
				gameController.AddToMiniMap(enemy.GetComponent<ShipMove>().shipMapIcon.transform);

			yield return new WaitForSeconds(0.5f);
		}
	}

	Transform FindTarget()	{
		Transform closest = landmarks.GetChild (0);
		for (int i = 1; i < landmarks.childCount; i++) {
			if (Vector3.Distance(transform.position, closest.position) > Vector3.Distance(transform.position, landmarks.GetChild(i).position))
				closest = landmarks.GetChild(i);
		}

		return closest;
	}
}
