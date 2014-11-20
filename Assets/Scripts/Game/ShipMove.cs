using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {
	public Transform planet;

	[Range(0, 100f)]
	public float maxSpeed = 40f;

	[Range(0, 100f)]
	public float brakeSpeed = 20f;

	[Range(0, 100f)]
	public float moveSpeed = 40f;

	[Range(0, 300f)]
	public float turnSpeed = 200f;

	public GameController gameController;

	public int targetLayer = 0;

	bool isLerping = false;
	Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.RotateAround(planet.position, transform.right, Time.fixedDeltaTime * moveSpeed);

		if (isLerping) {
			targetPosition = transform.position.normalized * gameController.distances[targetLayer];
			transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed / 2 * Time.fixedDeltaTime);

			if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
				isLerping = false;
		}
	}

	public void Brake()	{
		moveSpeed = brakeSpeed;
	}

	public void LayerDown()	{
		if (isLerping)
			return;
		
		targetLayer = targetLayer <= 0 ? 0 : targetLayer - 1;
		isLerping = true;
	}

	public void LayerUp()	{
		if (isLerping)
			return;

		targetLayer = targetLayer >= 2 ? 2 : targetLayer + 1;
		isLerping = true;
	}

	public void NormalSpeed()	{
		moveSpeed = maxSpeed;
	}

	public void TiltSideWays(int direction)	{
		transform.RotateAround (transform.position, transform.forward, Time.fixedDeltaTime * turnSpeed * direction);
	}


}
