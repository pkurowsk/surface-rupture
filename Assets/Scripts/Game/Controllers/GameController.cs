using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	enum GameStates	{
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

	/// <summary>
	/// Starts the game.
	/// </summary>
	public void StartGame()	{
		gameState = GameStates.PLAYING;

		storyUI.SetActive (false);
		gameHUD.SetActive (true);
	}
}
