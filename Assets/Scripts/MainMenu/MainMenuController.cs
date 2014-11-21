using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	public Transform main;
	public Transform levelSelect;

	Vector3 targetMain;
	Vector3 targetLvlSel;

	bool isTransition = false;

	// Use this for initialization
	void Start () {
		targetMain = new Vector3 (-700, 0, 0);
		targetLvlSel = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (isTransition) {
			main.localPosition = Vector3.Lerp(main.localPosition, targetMain, Time.deltaTime * 10);
			levelSelect.localPosition = Vector3.Lerp(levelSelect.localPosition, targetLvlSel, Time.deltaTime * 10f);

			if (Vector3.Distance(main.localPosition, targetMain) <= 0.01)	{
				isTransition = false;

				main.localPosition = targetMain;
				levelSelect.localPosition = targetLvlSel;
			}
		}
	}

	public void OnBackPress()	{
		isTransition = true;

		targetMain = Vector3.zero;
		targetLvlSel = new Vector3(700, 0, 0);
	}

	public void OnLevelSelectPress()	{
		isTransition = true;

		targetMain = new Vector3 (-700, 0, 0);
		targetLvlSel = Vector3.zero;
	}

	public void OnQuitPress()	{
		Application.Quit ();
	}

	public void OnStartPress()	{
		Application.LoadLevel(1);
	}
}
