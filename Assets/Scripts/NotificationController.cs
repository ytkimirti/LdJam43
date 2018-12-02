using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class NotificationController : MonoBehaviour
{

	public bool isOpen;
	public Transform spriteTrans;
	public string showText;
	public TextMeshPro text;
	public bool isDed;
	
	void Start () {
		
	}
	
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
			Notification(true);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.tag == "Player")
			Notification(false);
	}

	public void Notification(bool open)
	{
		if (isOpen == open || isDed)
			return;

		isOpen = open;
		
		text.text = showText;
		
		if (open)
		{
			spriteTrans.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
		}
		else
		{
			spriteTrans.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutQuart);
		}
	}

	public void CompletedTutorial()
	{
		if (isDed)
			return;
				
		Notification(false);
		
		isDed = true;
	}
}
