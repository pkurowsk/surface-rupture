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

	Vector3 startPos = Vector3.zero;

	// Update is called once per frame
	void Update () {
	
	}

	public void OnDrag(RectTransform rTrans)	{
		float y = Input.mousePosition.y;

		if (y > 212 + 77.5f)	
			y = 212 + 77.5f;
		else if(y < 212 - 77.5f)
			y = 212 - 77.5f;

		if (startPos.Equals(Vector3.zero))
			startPos = rTrans.position;

		rTrans.position = new Vector3(rTrans.position.x, y, rTrans.position.z);
	}

	public void OnDrop(RectTransform rTrans)	{
		float y = Input.mousePosition.y;

		if (y > 212 + 77.5f - 38.75) {
			y = 212 + 77.5f;
		} 
		else if (y < 212 - 77.5f + 38.75) {
			y = 212 - 77.5f;

		}
		else {
			y = 212;
		}

		if (mil.position.y == y && !rTrans.gameObject.name.Equals("Military"))
			mil.position = startPos;
		else if (spr.position.y == y && !rTrans.gameObject.name.Equals("Spiritual"))
			spr.position = startPos;
		else if (eco.position.y == y && !rTrans.gameObject.name.Equals("Economic"))
			eco.position = startPos;

		startPos = Vector3.zero;

		rTrans.position = new Vector3(rTrans.position.x, y, rTrans.position.z);
		
		gmil.localPosition = new Vector3 (gmil.localPosition.x, mil.localPosition.y / 2, gmil.localPosition.y);
		gspr.localPosition = new Vector3 (gspr.localPosition.x, spr.localPosition.y / 2, gspr.localPosition.y);
		geco.localPosition = new Vector3 (geco.localPosition.x, eco.localPosition.y / 2, geco.localPosition.y);
	}
}
