using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	public enum GameStates	{
		PLAYING,
		PAUSED,
		INTRO,
		GAMEOVER
	};

	public GameObject gameHUD;
	public GameObject storyUI;
	public GameObject pauseUI;

	public float[] distances;

	public Slider moraleBar;

	public Transform landmarks;

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

	/// <summary>
	/// Ends the game.
	/// </summary>
	public void GameOver()	{

	}

	public GameStates GetGameState()	{
		return gameState;
	}

	public void NextWave()	{
		if (wave % waveBreak == 0)
			Debug.Log("HELP");

		wave++;
		

	}

	/// <summary>
	/// Starts the game.
	/// </summary>
	public void StartGame()	{
		gameState = GameStates.PLAYING;

		storyUI.SetActive (false);
		gameHUD.SetActive (true);
	}
}
