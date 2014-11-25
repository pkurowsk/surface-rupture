using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	public Transform main;
	public Transform levelSelect;

	Vector3 targetMain;
	Vector3 targetLvlSel;

	bool isTransition = false;

	public Animator shipAnim;

	// Use this for initialization
	void Start () {
		targetMain = new Vector3 (-levelSelect.position.x, 0, 0);
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
		shipAnim.Play ("ShipFlyLeft");
		isTransition = true;

		targetLvlSel = new Vector3(-main.position.x, 0, 0);
		targetMain = Vector3.zero;
	}

	public void OnLevelSelectPress()	{
		shipAnim.Play ("ShipFlyRight");
		isTransition = true;

		targetMain = new Vector3 (-levelSelect.position.x, 0, 0);
		targetLvlSel = Vector3.zero;
	}

	public void OnQuitPress()	{
		Application.Quit ();
	}

	public void OnStartPress()	{
		Application.LoadLevel(1);
	}
}
