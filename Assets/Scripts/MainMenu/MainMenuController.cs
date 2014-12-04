using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	public Transform main;
	public Transform levelSelect;

	public RectTransform mainTitle;
	public RectTransform lvlTitle;

	public GameObject planets;

	public Text scores;

	Vector3 targetMain;
	Vector3 targetLvlSel;

	bool isTransition = false;

	public Animator shipAnim;

	// Use this for initialization
	void Start () {
		mainTitle.position = new Vector3 (mainTitle.position.x, Screen.height - mainTitle.rect.height / 2 - 10f, mainTitle.position.z);
		lvlTitle.position = new Vector3 (lvlTitle.position.x, Screen.height - lvlTitle.rect.height / 2 - 10f, lvlTitle.position.z);

		targetMain = new Vector3 (-levelSelect.position.x, 0, 0);
		targetLvlSel = Vector3.zero;

		if (!PlayerPrefs.HasKey ("scores")) {
			PlayerPrefs.SetString ("scores", "0 0 0 0 0 0 0 0 0 0");
			PlayerPrefs.Save();
		}

		// Load scores
		string []scoresString = PlayerPrefs.GetString ("scores").Split (' ');

		scores.text = "";
		for (int i = 0; i < 10; i++)	{
			if (i < 9)
				scores.text += (i+1) + ".\t\t\t" + scoresString [i] + "\n";
			else 
				scores.text += (i+1) + ".\t\t" + scoresString [i] + "\n";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isTransition) {
			main.localPosition = Vector3.Lerp(main.localPosition, targetMain, Time.deltaTime * 10);
			levelSelect.localPosition = Vector3.Lerp(levelSelect.localPosition, targetLvlSel, Time.deltaTime * 10f);
			planets.transform.localPosition = Vector3.Lerp(planets.transform.localPosition, targetLvlSel, Time.deltaTime * 15f);

			if (Vector3.Distance(main.localPosition, targetMain) <= 0.01)	{
				isTransition = false;

				main.localPosition = targetMain;
				levelSelect.localPosition = targetLvlSel;
				planets.transform.localPosition = targetLvlSel;
			}
		}
	}

	public void OnBackPress()	{
		shipAnim.Play ("ShipFlyLeft");
		isTransition = true;

		targetLvlSel = new Vector3(-main.position.x, 0, 0);
		targetMain = Vector3.zero;

		for (int i = 0; i < planets.transform.childCount; i++) 
			planets.transform.GetChild(i).GetComponentInChildren<Animator>().enabled = false;
		GameControllerSingleton.planet = null;
	}

	public void OnLevelSelectPress()	{
		shipAnim.Play ("ShipFlyRight");
		isTransition = true;

		targetMain = new Vector3 (-levelSelect.position.x, 0, 0);
		targetLvlSel = Vector3.zero;

		planets.SetActive (true);
	}

	public void OnQuitPress()	{
		Application.Quit ();
	}

	public void OnStartPress()	{
		if (GameControllerSingleton.planet != null)
			Application.LoadLevel(1);
	}
}
