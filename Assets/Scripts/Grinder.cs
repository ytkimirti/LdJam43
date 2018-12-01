﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Grinder : MonoBehaviour
{
	public GameObject batteryPrefab;
	
	[Space]
	
	public Transform enterTop;
	public Transform enterPoint;
	
	[Space] 
	
	public Transform outPoint;

	public Transform outOffset;

	public ParticleSystem steamParticle;
	public ParticleSystem bloodParticle;
	
	void Start () {
		
	}
	
	void Update () {
		
	}

	public void MountSomething(Transform load)
	{
		GinionController ginion = load.GetComponent<GinionController>();

		if (!ginion)
		{
			Debug.LogError("Bu ginion değil, yanlış şey soktunuz bana!");
			return;
		}

		ginion.ReadyGrinding();
		
		StartCoroutine(playAnimation(load,ginion));
	}

	IEnumerator playAnimation(Transform load,GinionController ginion)
	{
		load.DOMove(enterTop.position, 1f).SetEase(Ease.InOutQuart);

		yield return new WaitForSeconds(1.1f);
		
		load.DOMove(enterPoint.position, 1f).SetEase(Ease.OutBounce);

		steamParticle.emissionRate = 20;

		steamParticle.startSpeed = 3f;
		
		yield return new WaitForSeconds(1.1f);

		bloodParticle.Play();
		
		if (ginion.isDed)
		{
			ginion.Destroy();
			
			steamParticle.emissionRate = 3;

			steamParticle.startSpeed = 1f;
			
			yield break;
		}
		
		ginion.Destroy();
		
		yield return new WaitForSeconds(1.5f);
		
		steamParticle.emissionRate = 3;

		steamParticle.startSpeed = 1f;
		
		GameObject battery = Instantiate(batteryPrefab, outPoint.position, Quaternion.identity);

		battery.transform.DOMove(outOffset.position,1f).SetEase(Ease.InOutQuart);
	}
}