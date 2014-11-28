using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipController : MonoBehaviour {
	public ShipMove ship;

	public Text healthText;
	public Slider healthBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Shoot
		if (Input.GetMouseButton (0)) {
			ship.FireH(Input.mousePosition.x, Input.mousePosition.y);
		} 
		else if (Input.GetMouseButton (1)) {
			ship.FireV(Input.mousePosition.x, Input.mousePosition.y);
		}

		// Turn
		if (Input.GetKey (KeyCode.A)) {
			ship.TiltSideWays(1);
		}

		if (Input.GetKey (KeyCode.D)) {
			ship.TiltSideWays(-1);
		}

		// Move between layers
		if (Input.GetKey (KeyCode.I) || Input.GetAxis("Mouse ScrollWheel") < 0) {
			ship.LayerUp();
		}
		if (Input.GetKey (KeyCode.K)|| Input.GetAxis("Mouse ScrollWheel") > 0) {
			ship.LayerDown();
		}

		// Control ship speed
		if (Input.GetKey (KeyCode.W)) {
			ship.Accelerate();
		}
		else if (Input.GetKey (KeyCode.S)) {
			ship.Brake();
		}
		else {
			ship.NormalSpeed();
		}

	}

	void OnTriggerEnter(Collider c)	{
		if (c.tag.Equals ("EBlast")) {
			Destroy(c.gameObject);

			healthBar.value -= 0.75f;
			healthText.text = "HEALTH " + healthBar.value + "%";
			GameControllerSingleton.GetInstance().IncHealthLost();

			if (healthBar.value <= 0)	{
				GameControllerSingleton.GetInstance().GameOver();
			}
		}
	}
}
