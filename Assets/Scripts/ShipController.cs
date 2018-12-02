using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShipController : MonoBehaviour
{
	public float currJuice;
	public float maxJuice;
	public int scrapCount;
	public HealthBar bar;
	public Transform scrapTarget;
	
	[Space]
	
	public Animator animator;
	
	public bool loadJuice;

	public ShipChargerController[] chargers;
	
	void Start ()
	{
		currJuice = 0;
	}
	
	void Update ()
	{
		currJuice = Mathf.Clamp(currJuice, 0, maxJuice);
		
		bar.currHealth = currJuice / maxJuice;
		
		bool isCharging = true;

		foreach (ShipChargerController c in chargers)
		{
			if (!c.isReady)
				isCharging = false;
		}

		loadJuice = isCharging;

		if (isCharging)
		{
			currJuice += Time.deltaTime * 15;
		}
	}

	public void MountScrap(Transform scrap)
	{
		if (!scrap || scrap.gameObject.tag != "scrap") 
			return;
		
		scrap.DOMove(scrapTarget.position,1f).SetEase(Ease.InOutQuart);
			
		scrapCount++;
	}
}
