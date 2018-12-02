using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

	public Transform healthSprite;
	public Transform healthInside;
	
	
	public float currHealth;

	public float aspectRatio;
	public float scale;

	private float lerpedHP;
	public float lerpSpeed;
	
	void Start ()
	{
		lerpedHP = currHealth;
	}
	
	void Update ()
	{
		lerpedHP = Mathf.Lerp(lerpedHP, currHealth, lerpSpeed * Time.timeScale);
		
		healthSprite.localScale = new Vector3(aspectRatio * scale,(1 / aspectRatio) * scale,1);
		
		healthInside.localScale = new Vector3(Mathf.Clamp01(lerpedHP),1,1);
	}
}
