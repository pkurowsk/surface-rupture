using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Landmark : MonoBehaviour {
	[Range(0, 100)]
	public float health = 100;

	float tierSize = 33;
	float depleted = 0;

	public GameController.LandmarkType type;

	public Image landmarkIconPrefab;

	RectTransform landmarkIcon;

	Text landmarkStatus;

	float sincelastAttack = 0;

	// Use this for initialization
	void OnEnable () {
		SetMapIcon ();
		sincelastAttack = Time.fixedTime;
	}

	void FixedUpdate()	{
		if (Time.fixedTime - sincelastAttack > 2f) {
			landmarkStatus.text = "Landmark " + gameObject.name + ": SAFE";
			landmarkStatus.color = Color.green;
		}

	}

	void OnTriggerEnter(Collider c)	{
		if (c.tag.Equals ("EBlast")) {
			Destroy(c.transform.parent.gameObject);
			landmarkStatus.text = "Landmark " + gameObject.name + ": UNDER ATTACK";
			landmarkStatus.color = Color.red;

			sincelastAttack = Time.fixedTime;

			health -= 0.25f;
			depleted += 0.25f;
			if (depleted >= tierSize)	{
				Debug.Log("BOOM Tier down");

				GameControllerSingleton.GetInstance().LandmarkTierDown(type);
				depleted = 0;

				if (health < tierSize)	{
					landmarkStatus.text = "Landmark " + gameObject.name + ": DESTROYED";
					landmarkStatus.color = Color.gray;
					Destroy(landmarkIcon.gameObject);
					Destroy(gameObject);
				}
			}
		}
	}

	void SetMapIcon()	{
		landmarkStatus = GameControllerSingleton.GetInstance ().NewLandmarkStatus ();
		landmarkStatus.text = "Landmark " + gameObject.name + ": SAFE";

		landmarkIcon = (Image.Instantiate (landmarkIconPrefab) as Image).rectTransform;
		landmarkIcon.transform.GetChild (0).GetComponent<Text> ().text = gameObject.name;

		float theta = Mathf.Acos (transform.position.z / transform.position.magnitude);
		float phi = Mathf.Atan2 (transform.position.y, transform.position.x);
		
		float x = 25 * Mathf.Sin (theta) * Mathf.Cos (phi);
		float y = 25 * Mathf.Sin (theta) * Mathf.Sin (phi) + 25;
		
		if (transform.position.z > 0) {
			y = -y;
		}

		landmarkIcon.parent = GameObject.Find ("Mini Map").transform;
		
		landmarkIcon.localPosition = new Vector3 (x, y, landmarkIcon.localPosition.z);
	}

}
