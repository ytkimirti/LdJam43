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
	public GlowLoop glowLoop;
	public Transform spriteTrans;

	public bool isShipReady;
	
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
		
		if (!isShipReady && scrapCount >= 5 && currJuice >= maxJuice)
		{
			isShipReady = true;

			glowLoop.enabled = true;
			
			PlayerController.main.spaceshipTutorial.CompletedTutorial();

			StartCoroutine(GetEveryone());
			
			FindObjectOfType<CanvasController>().EndGamePanel(true);

		}
	}

	IEnumerator GetEveryone()
	{
		GinionController[] ginions = FindObjectsOfType<GinionController>();

		foreach (GinionController g in ginions)
		{
			if (!g)
				continue;

			yield return new WaitForSeconds(0.2f);

			if (!g || g.transform.position.magnitude > 10 || g.isDed)
				continue;

			g.healthBar.gameObject.SetActive(false);

			g.transform.parent = spriteTrans;
			
			g.transform.DOMove(scrapTarget.position,0.4f).SetEase(Ease.InOutQuad);

			GameManager.main.ginionsSaved++;

		}
	}

	public void PlayAnimation()
	{
		glowLoop.enabled = false;
		spriteTrans.GetComponent<SpriteRenderer>().color = Color.white;
		GetComponent<Animator>().SetTrigger("fly");
		Invoke("roll", 6);
	}

	void roll()
	{
		CanvasController.main.RollCredits();
	}
	
	public void MountScrap(Transform scrap)
	{
		if (!scrap || scrap.gameObject.tag != "scrap") 
			return;
		
		scrap.DOMove(scrapTarget.position,1f).SetEase(Ease.InOutQuart).OnComplete(OnCompleted);
			
		scrapCount++;

		if (!isShipReady && scrapCount >= 5 && currJuice >= maxJuice)
		{
			isShipReady = true;

			glowLoop.enabled = true;
			
			PlayerController.main.spaceshipTutorial.CompletedTutorial();

			StartCoroutine(GetEveryone());
			
			FindObjectOfType<CanvasController>().EndGamePanel(true);

		}else if (scrapCount >= 5)
		{
			PlayerController.main.spaceshipTutorial.showText = "You need to charge this ship";
		}else
		{
			PlayerController.main.spaceshipTutorial.showText =
				"You need " + (5 - scrapCount).ToString() + " more scraps for the spaceship to work";
		}
	}

	void OnCompleted()
	{
		GameManager.main.PopNotification(PlayerController.main.transform.position + Vector3.up * 2f,"Scrap Added",Color.green);
		AudioManager.main.Play("powerup");
	}
}
