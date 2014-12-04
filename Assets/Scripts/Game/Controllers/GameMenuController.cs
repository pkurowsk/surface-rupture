using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMenuController : MonoBehaviour {
	public RectTransform mil;
	public RectTransform spr;
	public RectTransform eco;

	public RectTransform gmil;
	public RectTransform gspr;
	public RectTransform geco;

	// Formated Game HUD
	public RectTransform healthSlider;
	public RectTransform moraleSlider;
	public RectTransform miniMap;
	public RectTransform gameFeed;

	public Text planetName;
	public Text planetStory;

	Vector3 startPos = Vector3.zero;

	void Start () {
		planetName.text = GameControllerSingleton.planet.planetName;
		planetStory.text = GameControllerSingleton.planet.planetStory;

		healthSlider.position = new Vector3 (healthSlider.position.x, Screen.height - healthSlider.rect.height / 2, healthSlider.position.z);
		moraleSlider.position = new Vector3 (Screen.width - moraleSlider.rect.width / 2 - 30f, moraleSlider.position.y, moraleSlider.position.z);
		miniMap.position = new Vector3(30, Screen.height - miniMap.rect.height / 2, miniMap.position.z);
		gameFeed.position = new Vector3 (Screen.width - gameFeed.rect.width / 2, Screen.height - gameFeed.rect.height / 2, gameFeed.position.z);
	}

	public void OnDrag(RectTransform rTrans)	{
		float y = Input.mousePosition.y;

		if (y > rTrans.parent.parent.parent.transform.position.y + 77.5f)	
				y = 77.5f;
		else if (y < rTrans.parent.parent.parent.transform.position.y - 77.5f)
				y = -77.5f;
		else
				y -= rTrans.parent.transform.position.y;

		if (startPos.Equals (Vector3.zero)) {
						startPos = rTrans.position;
		}

		rTrans.localPosition = new Vector3(rTrans.localPosition.x, y, rTrans.localPosition.z);
	}

	public void OnDrop(RectTransform rTrans)	{
		float y = Input.mousePosition.y;

		if (y > rTrans.parent.parent.parent.transform.position.y + 77.5f - 38.75) {
			y = 77.5f;
		} 
		else if (y < rTrans.parent.parent.parent.transform.position.y - 77.5f + 38.75) {
			y = -77.5f;
		}
		else {
			y = 0;
		}

		if (mil.localPosition.y == y && !rTrans.gameObject.name.Equals("Military"))
			mil.position = startPos;
		else if (spr.localPosition.y == y && !rTrans.gameObject.name.Equals("Spiritual"))
			spr.position = startPos;
		else if (eco.localPosition.y == y && !rTrans.gameObject.name.Equals("Economic"))
			eco.position = startPos;

		startPos = Vector3.zero;

		rTrans.localPosition = new Vector3(rTrans.localPosition.x, y, rTrans.localPosition.z);
		
		gmil.localPosition = new Vector3 (gmil.localPosition.x, mil.localPosition.y / 2, gmil.localPosition.y);
		gspr.localPosition = new Vector3 (gspr.localPosition.x, spr.localPosition.y / 2, gspr.localPosition.y);
		geco.localPosition = new Vector3 (geco.localPosition.x, eco.localPosition.y / 2, geco.localPosition.y);
	}

	public void OnMainMenuButton()	{
		Application.LoadLevel (0);
	}
}
