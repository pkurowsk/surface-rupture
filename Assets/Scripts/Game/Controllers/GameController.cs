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

	public GameObject gameHUD;
	public GameObject storyUI;
	public GameObject pauseUI;
	public Transform miniMap;

	public float[] distances;

	public Slider moraleBar;

	public Transform landmarks;

	public LandmarkType[] planetRanking;

	public EnemySpawner[] eSpawners;

	GameStates gameState;

	// The current wave that the player is on
	int wave = 0;

	// The number of waves it takes to get power ups
	const int waveBreak = 10;

	// Use this for initialization
	void Start () {
		gameState = GameStates.INTRO;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddToMiniMap(Transform icon)	{
		icon.SetParent (miniMap);
	}

	IEnumerator FirstWave()	{
		yield return new WaitForSeconds (2f);
		NextWave ();
	}

	/// <summary>
	/// Ends the game.
	/// </summary>
	public void GameOver()	{

	}

	public GameStates GetGameState()	{
		return gameState;
	}

	public void NextWave()	{
		if (wave % waveBreak == 0 && wave != 0) {
			HelpWave();
		}

		wave++;

		for (int i = 0; i < eSpawners.Length; i++) {
			eSpawners[i].spawnWave(wave);
		}

	}

	public void LandmarkTierDown(LandmarkType type)	{
		if (planetRanking [0] == type) {
			moraleBar.value = moraleBar.value - 40f;
		}
		else if (planetRanking [1] == type) {
			moraleBar.value = moraleBar.value - 20f;
		}
		else if (planetRanking [2] == type) {
			moraleBar.value = moraleBar.value - 10f;
		}
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

		StartCoroutine (FirstWave ());
	}
}
