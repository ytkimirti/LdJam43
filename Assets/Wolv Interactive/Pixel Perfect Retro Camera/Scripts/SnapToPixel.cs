using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapToPixel : MonoBehaviour {
	private PixelCamera cam;
	
	float d;

	private bool isCam;
	private Transform child;
	
	void Start() {
		cam = GetComponentInChildren<PixelCamera>();

		if (!cam)
			isCam = false;

		cam = FindObjectOfType<PixelCamera>();

		if(transform.childCount > 0)
			child = transform.GetChild(0);
		
		d = 1f / cam.pixelsPerUnit;
	}

	void LateUpdate()
	{
		if (!child)
			return;
		
		Vector3 pos = transform.position;
		Vector3 camPos = new Vector3 (pos.x - pos.x % d, pos.y - pos.y % d, pos.z);
		if (isCam)
			cam.transform.position = camPos;
		else
			child.position = camPos;
	}
}
