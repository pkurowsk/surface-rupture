using UnityEngine;
using System.Collections;

public class ShipMove : MonoBehaviour {
	public Transform planet;

	public GameObject hBlast;
	public GameObject vBlast;

	[Range(0, 100f)]
	public float maxSpeed = 80f;
	[Range(0, 100f)]
	public float normSpeed = 40f;
	[Range(0, 100f)]
	public float brakeSpeed = 20f;

	[Range(0, 100f)]
	public float moveSpeed = 40f;

	[Range(0, 1000f)]
	public float turnSpeed = 200f;

	public GameController gameController;

	public int targetLayer = 0;

	bool isLerping = false;

	Vector3 targetPosition;

	float lastShot;

	float fireRate = 0.15f;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		lastShot = Time.time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (gameController.GetGameState () != GameController.GameStates.PLAYING)
						return;

		transform.RotateAround(planet.position, transform.right, Time.fixedDeltaTime * moveSpeed);

		if (isLerping) {
			targetPosition = transform.position.normalized * gameController.distances[targetLayer];
			transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed / 2 * Time.fixedDeltaTime);

			if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
				isLerping = false;
		}
	}

	public void Accelerate()	{
		moveSpeed = maxSpeed;
	}

	public void Brake()	{
		moveSpeed = brakeSpeed;
	}

	Quaternion GetFireAngle(float x, float y)	{
		y -= Screen.height / 2;
		x -= Screen.width / 2;

		float angle = Mathf.Atan2 (y, x) / Mathf.PI * 180f - 90f;

		Quaternion angleVec = Quaternion.AngleAxis (angle, transform.forward) * transform.rotation;

		return angleVec;
	}

	public void FireH(float x, float y)	{
		if (Time.time - lastShot > fireRate && gameController.GetGameState() == GameController.GameStates.PLAYING) {
			lastShot = Time.time;

			Instantiate(hBlast, transform.position, GetFireAngle(x, y));
		}
	}

	public void FireV(float x, float y)	{
		if (Time.time - lastShot > fireRate && gameController.GetGameState() == GameController.GameStates.PLAYING) {
			lastShot = Time.time;
					
			Instantiate(vBlast, transform.position, GetFireAngle(x, y));
		}
	}

	public void LayerDown()	{
		if (isLerping || gameController.GetGameState() != GameController.GameStates.PLAYING)
			return;
		
		targetLayer = targetLayer <= 0 ? 0 : targetLayer - 1;
		isLerping = true;
	}

	public void LayerUp()	{
		if (isLerping || gameController.GetGameState() != GameController.GameStates.PLAYING)
			return;

		targetLayer = targetLayer >= 2 ? 2 : targetLayer + 1;
		isLerping = true;
	}

	public void NormalSpeed()	{
		moveSpeed = normSpeed;
	}

	public void TiltSideWays(int direction)	{
		if (gameController.GetGameState() == GameController.GameStates.PLAYING)
			transform.RotateAround (transform.position, transform.forward, Time.fixedDeltaTime * turnSpeed * direction);
	}


}
