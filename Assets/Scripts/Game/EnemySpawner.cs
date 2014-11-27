using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;

	public Transform warpHole;

	public Transform planet;

	public Transform enemiesHolder;

	public Image enemyMapIcon;

	public Image spawnerIconPrefab;
	public RectTransform spawnerIcon;

	// Use this for initialization
	void OnEnable () {
		SetMapIcon ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		warpHole.Rotate(warpHole.up, Time.fixedDeltaTime * -0.5f);
	}

	public void spawnWave(int waveNum)	{
		StartCoroutine(spawnEnemies ((int)(50 * (waveNum / 20f))));
	}

	IEnumerator spawnEnemies(int enemies)	{
		for (int i = 0; i < enemies; i++) {
			GameObject enemy = Instantiate (enemyPrefab, transform.position.normalized * 75.5f, transform.rotation) as GameObject;
			enemy.GetComponent<ShipMove>().planet = planet;
			enemy.GetComponent<Enemy>().target = GameControllerSingleton.GetInstance().FindTarget(transform.position);

			enemy.GetComponent<ShipMove>().shipMapIcon = (Image.Instantiate(enemyMapIcon) as Image).rectTransform;
			if (enemy.GetComponent<ShipMove>().shipMapIcon != null)
				GameControllerSingleton.GetInstance().AddToMiniMap(enemy.GetComponent<ShipMove>().shipMapIcon.transform);

			enemy.transform.parent = enemiesHolder;

			yield return new WaitForSeconds(0.5f);
		}
	}

	void SetMapIcon()	{
		spawnerIcon = (Image.Instantiate (spawnerIconPrefab) as Image).rectTransform;
		
		float theta = Mathf.Acos (transform.position.z / transform.position.magnitude);
		float phi = Mathf.Atan2 (transform.position.y, transform.position.x);
		
		float x = 25 * Mathf.Sin (theta) * Mathf.Cos (phi);
		float y = 25 * Mathf.Sin (theta) * Mathf.Sin (phi) + 25;
		
		if (transform.position.z > 0) {
			y = -y;
		}
		
		spawnerIcon.parent = GameObject.Find ("Mini Map").transform;
		
		spawnerIcon.localPosition = new Vector3 (x, y, spawnerIcon.localPosition.z);
	}
}
