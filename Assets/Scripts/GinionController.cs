using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GinionController : MonoBehaviour
{

	public Color liveColor;
	public Color deadColor;
	
	public bool isGrinding;
	public bool isDed;

	public bool isSafe;
	private int timer;

	public Transform spriteTransform;

	public float currHealth;
	public float maxHealth;
	
	public HealthBar healthBar;
	public ParticleSystem steamParticle;
	
	public Sprite happySprite;
	public Sprite sadSprite;
	public Sprite dieSprite;
	public Sprite eyeDieSprite;
	
	public SpriteRenderer bodySpriteRenderer;
	public SpriteRenderer eyeSpriteRenderer;
	public SpriteRenderer mouthSpriteRenderer;

	public Animator bodyAnimator;
	public Animator eyeAnimator;
	
	private Collider2D col;

	private HeaterController heater;

	private Tween cold;
	
	void Start ()
	{
		currHealth = maxHealth;
		
		col = GetComponent<Collider2D>();
		heater = FindObjectOfType<HeaterController>();

		cold = spriteTransform.DOLocalMoveX(0.05f, 0.2f).SetLoops(-1,LoopType.Yoyo);
	}
	
	void Update ()
	{
		if (isDed)
			return;
		
		timer++;

		if (timer > 5)
		{
			timer = 0;
			isSafe = heater.isWorking && Vector2.Distance(transform.position, heater.transform.position) < heater.safezoneRadius;
			
			
			if (!isSafe)
			{
				if(!cold.IsPlaying())
					cold.Play();
			}
			else
			{
				cold.Rewind();
			}
		}

		if (isSafe)
		{
			currHealth += Time.deltaTime * 3;
		}
		else
		{
			currHealth -= Time.deltaTime;
		}

		steamParticle.enableEmission = !isSafe;
		
		currHealth = Mathf.Clamp(currHealth, 0, maxHealth);

		Color col = Color.Lerp(deadColor, liveColor, currHealth / maxHealth);

		bodySpriteRenderer.color = col;

		healthBar.currHealth = currHealth / maxHealth;

		if (currHealth == 0)
		{
			Die();
		}
		
		if (!isGrinding && !isDed)
			mouthSpriteRenderer.sprite = isSafe ? happySprite : sadSprite;
	}

	public void ReadyGrinding()
	{
		if(!isDed)
			mouthSpriteRenderer.sprite = sadSprite;
		
		col.enabled = false;
		healthBar.gameObject.SetActive(false);
	}

	public void Die()
	{
		if (isDed)
			return;

		GameManager.main.ginionsDiedFromCold++;
		
		isDed = true;

		bodyAnimator.enabled = false;

		eyeAnimator.enabled = false;

		eyeSpriteRenderer.sprite = eyeDieSprite;
		
		mouthSpriteRenderer.sprite = dieSprite;
		
		healthBar.gameObject.SetActive(false);
		
		cold.Kill();
	}

	public void Destroy()
	{
		Destroy(gameObject);
	}
}
