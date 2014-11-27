using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public enum LandmarkType	{
		Military,
		Spiritual,
		Economic
	};

	public enum GameStates	{
		PLAYING,
		PAUSED,
		INTRO,
		GAMEOVER
	};

	public Transform player;
	public Transform oppositePlayer;

	public Transform enemies;
	
	public GameObject gameHUD;
	public GameObject storyUI;
	public GameObject pauseUI;
	public GameObject gameOverUI;

	public Text shotsFiredText;
	public Text shotsHitText;
	public Text healthLostText;
	public Text moraleLostText;
	public Text wavesText;
	public Text deathText;
	public Text wavesUIText;

	public Text landmarkStatusPrefab;

	public Transform miniMap;

	public float[] distances;

	public Slider moraleBar;
	
	public Transform landmarks;
	public Transform eSpawners;

	public LandmarkType[] planetRanking;
	public int[] moraleValues;

	GameStates gameState;

	// The current wave that the player is on
	int wave = 0;
	int shotsFired = 0;
	int shotsHit = 0;
	int healthLost = 0;
	int moraleLost = 0;

	// The number of waves it takes to get power ups
	const int waveBreak = 10;

	// Use this for initialization
	void Start () {
		gameState = GameStates.INTRO;
	}

	public void AddToMiniMap(Transform icon)	{
		icon.SetParent (miniMap);
	}

	public void CheckEndOfWave()	{
		if (enemies.childCount == 1) {
			StartCoroutine(NextWave ());
		}
	}

	public Transform FindTarget(Vector3 position)	{
		if (landmarks.childCount == 0) {
			GameOver();
			return player;
		}

		if (Random.Range (0, 100) > 75) {
			return landmarks.GetChild(Random.Range(0, landmarks.childCount - 1));
		}

		Transform closest = landmarks.GetChild (0);
		for (int i = 1; i < landmarks.childCount; i++) {
			if (Vector3.Distance(position, closest.position) > Vector3.Distance(position, landmarks.GetChild(i).position))
				closest = landmarks.GetChild(i);
		}
		
		return closest;
	}

	/// <summary>
	/// Ends the game.
	/// </summary>
	public void GameOver()	{
		gameState = GameStates.GAMEOVER;

		shotsFiredText.text = "" + shotsFired;
		shotsHitText.text = "" + shotsHit;
		healthLostText.text = "" + healthLost;
		moraleLostText.text = "" + moraleLost;
		wavesText.text = "WAVES " + wave;

		if (moraleBar.value == 0)
			deathText.text = "MORALE LOST";

		gameOverUI.SetActive (true);
		gameHUD.SetActive (false);

		string[] scores = PlayerPrefs.GetString ("scores").Split(' ');
		ArrayList list = new ArrayList ();
		list.Add (wave);
		for (int i = 0; i < 10; i++) {
			list.Add (int.Parse (scores [i]));
		}
		list.Sort ();

		string scoreString = "";
		for (int i = 10; i >= 1; i--)
			scoreString += list [i] + " ";

		PlayerPrefs.SetString ("scores", scoreString.Trim());
		PlayerPrefs.Save ();
	}

	public GameStates GetGameState()	{
		return gameState;
	}

	public void IncHealthLost()	{
		healthLost++;
	}

	public void IncShotsFired()	{
		shotsFired++;
	}

	public void IncShotsHits()	{
		shotsHit++;
	}

	public Text NewLandmarkStatus()	{
		Text landmarkStatus = Text.Instantiate (GameControllerSingleton.GetInstance ().landmarkStatusPrefab) as Text;

		Transform statuses = GameObject.Find ("Landmark Statuses").transform;
		landmarkStatus.rectTransform.parent = statuses;
		landmarkStatus.rectTransform.localPosition = new Vector3 (0, 6 - 8 * (statuses.childCount - 1), landmarkStatus.rectTransform.localPosition.z);

		return landmarkStatus;
	}

	IEnumerator NextWave()	{
		yield return new WaitForSeconds (5f);
		if (wave % waveBreak == 0 && wave != 0) {
			HelpWave();
		}

		wave++;
		GameOver ();
		wavesUIText.text = "WAVE " + wave;
		wavesUIText.gameObject.GetComponent<Animator>().Play ("NewWave");
		yield return new WaitForSeconds (1f);

		for (int i = 0; i < eSpawners.childCount; i++) {
			eSpawners.GetChild(i).GetComponent<EnemySpawner>().spawnWave(wave);
		}

	}

	public void LandmarkTierDown(LandmarkType type)	{
		if (planetRanking [0] == type) {
			moraleBar.value = moraleBar.value - moraleValues[0];
			moraleLost += moraleValues[0];
		}
		else if (planetRanking [1] == type) {
			moraleBar.value = moraleBar.value - moraleValues[1];
			moraleLost += moraleValues[1];
		}
		else if (planetRanking [2] == type) {
			moraleBar.value = moraleBar.value - moraleValues[2];
			moraleLost += moraleValues[2];
		}

		if (moraleBar.value == 0)
			GameOver ();
	}

	void HelpWave()	{
		Debug.Log ("Help");
	}

	/// <summary>
	/// Starts the game.
	/// </summary>
	public void StartGame()	{
		gameState = GameStates.PLAYING;

		storyUI.SetActive (false);
		gameHUD.SetActive (true);

		landmarks.gameObject.SetActive (true);
		eSpawners.gameObject.SetActive (true);

		StartCoroutine(NextWave ());
	}
}
