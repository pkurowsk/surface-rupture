using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipController : MonoBehaviour {
	public ShipMove ship;

	public Slider healthBar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
}
