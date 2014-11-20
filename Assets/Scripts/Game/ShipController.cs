using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {
	public ShipMove ship;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.A)) {
			ship.TiltSideWays(1);
		}

		if (Input.GetKey (KeyCode.D)) {
			ship.TiltSideWays(-1);
		}

		if (Input.GetKey (KeyCode.I) || Input.GetAxis("Mouse ScrollWheel") < 0) {
			ship.LayerUp();
		}
		if (Input.GetKey (KeyCode.K)|| Input.GetAxis("Mouse ScrollWheel") > 0) {
			ship.LayerDown();
		}

		if (Input.GetKey (KeyCode.S)) {
			ship.Brake();
		}
		else {
			ship.NormalSpeed();
		}

	}
}
