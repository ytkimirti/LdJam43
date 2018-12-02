using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class FastNotification : MonoBehaviour
{
	private TextMeshPro textMesh;
	
	void Start ()
	{
		
		//play("Empty text");
	}

	public void play(string text,Color col)
	{
		textMesh = GetComponentInChildren<TextMeshPro>();
		
		textMesh.text = text;
		
		transform.localScale = Vector3.zero;
		
		transform.DOMoveY(transform.position.y + 1,3f).SetEase(Ease.Linear);

		transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutElastic);

		textMesh.DOColor(col, 0.4f).SetLoops(2, LoopType.Yoyo).OnComplete(ShockComplete);
	}

	void ShockComplete()
	{
		textMesh.DOColor(Color.clear, 3f).OnComplete(Destroy);
	}

	void Destroy()
	{
		Destroy(gameObject);
	}
}
