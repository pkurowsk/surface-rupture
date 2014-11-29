using UnityEngine;

public class GameControllerSingleton {

	static GameController _instance;

	public static Planet planet;

	public static GameController GetInstance()	{
		if (_instance == null)
			_instance = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();

		return _instance;
	}
}
