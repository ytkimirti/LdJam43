using System.Collections;
using System.Collections.Generic;
using System.Timers;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool isDed;
	
	public float pickupRadius;
	public LayerMask pickupsLayer;

	public float juice;
	public float maxJuice = 100;
	public HealthBar healthBar;
	
	[Header("Movement")]
	public float accelerationSpeed;
	public float maxSpeed;
	
	[Header("References")]
	
	public ChargerController charger;

	public HeaterController heater;
	
	public Transform pickupPoint;

	public Transform spriteTrans;

	public Transform currLoad;
	
	[Header("Visual")]
	
	public Animator bodyAnimator;

	public Animator faceAnimator;
	
	Rigidbody2D rb;
	Vector2 inp;

	public static PlayerController main;
	
	void Start ()
	{

		main = this;
		
		rb = GetComponent<Rigidbody2D>();

		juice = maxJuice;
	}
	
	void FixedUpdate ()
	{
		if (isDed)
			return;
		
		UpdateInput();
		
		Vector2 force = inp * accelerationSpeed;
		
		bodyAnimator.SetBool("IsMoving",inp.magnitude != 0);

		rb.AddForce(force);
        
		rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);

		if (inp.x != 0)
		{
			spriteTrans.transform.localEulerAngles = new Vector3(0,inp.x > 0 ? 0 : 180,0);
			
			//spriteTrans.transform.localScale = new Vector3(inp.x > 0 ? 1 : -1,1,1);
		}
	}

	public void Die()
	{
		if (isDed)
			return;

		isDed = true;

		healthBar.transform.DOScale(Vector3.zero, 1f);
		
		faceAnimator.SetTrigger("die");
	}

	Collider2D FindNearCol()
	{
		Collider2D[] col = Physics2D.OverlapCircleAll(pickupPoint.position, pickupRadius,pickupsLayer);

		bool isLoaded = currLoad;
		
		foreach (Collider2D c in col)
		{
			if ((!isLoaded && (c.tag == "battery" || c.tag == "ginion" || c.tag == "scrap")) ||
			    (isLoaded && (c.tag == "charger" || c.tag == "grinder" || c.tag == "heater" || c.tag == "ship" || c.tag == "shipcharger")))
			{
				return c;
			}
		}

		return null;
	}
	
	void Update()
	{

		if (isDed)
			return;
		
		faceAnimator.SetBool("isCharging", charger.isCharging);
		
		if (charger.isCharging)
		{
			juice += Time.deltaTime * 5;
			if (juice > maxJuice)
			{
				juice = maxJuice;
			}
		}
		else
		{
			juice -= Time.deltaTime * 1.5f;	
		}
		
		if (juice < 0)
		{
			juice = 0;
			Die();
		}

		healthBar.currHealth = juice / maxJuice;


		Collider2D col = FindNearCol();
		
		if (!currLoad && col && (col.gameObject.tag == "ginion" || col.gameObject.tag == "battery" || col.gameObject.tag == "scrap"))
		{
			col.gameObject.GetComponent<SpriteOutline>().isOutlineActive = true;
			
			if (Input.GetKeyDown(KeyCode.Space))
			{
				currLoad = col.gameObject.transform;

				if (charger.currBattery && currLoad == charger.currBattery.transform)
				{
					charger.currBattery = null;
				}
				
				if (heater.currBattery && currLoad == heater.currBattery.transform)
				{
					heater.currBattery = null;
				}

				currLoad.DOKill();
				
				currLoad.transform.parent = pickupPoint;
				currLoad.transform.localPosition = Vector3.zero;
				
				currLoad.gameObject.layer = 0;
			}
		}
		else if (currLoad && col && (col.gameObject.tag == "grinder" || col.gameObject.tag == "batterymount" ||
		                             col.gameObject.tag == "charger" || col.gameObject.tag == "heater" ||
		                             col.gameObject.tag == "shipcharger" || col.gameObject.tag == "ship"))
		{
			if (currLoad.gameObject.tag == "ginion" && col.gameObject.tag == "grinder")
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					col.gameObject.GetComponent<Grinder>().MountSomething(currLoad);
					Unload();
				}
			}else if (col.gameObject.tag == "charger" && currLoad.gameObject.tag == "battery")
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					col.gameObject.GetComponent<ChargerController>().MountBattery(currLoad);
					Unload();
				}
			}else if (col.gameObject.tag == "heater" && currLoad.gameObject.tag == "battery")
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					col.gameObject.GetComponent<HeaterController>().MountBattery(currLoad);
					Unload();
				}
			}else if (col.gameObject.tag == "ship" && currLoad.gameObject.tag == "scrap")
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					//col.gameObject.GetComponent<HeaterController>().MountBattery(currLoad);
					print("Loading scrap to the spaceship!");
					col.gameObject.GetComponent<ShipController>().MountScrap(currLoad);
					Unload();
				}
			}else if (col.gameObject.tag == "shipcharger" && currLoad.gameObject.tag == "battery")
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					col.gameObject.GetComponent<ShipChargerController>().MountBattery(currLoad);
					Unload();
				}
			}
			else
			{
				return;
			}
			
			col.gameObject.GetComponent<SpriteOutline>().isOutlineActive = true;

		}else if (currLoad)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Unload();
			}
		}
	}

	public void Unload()
	{
		currLoad.gameObject.layer = 8;
		
		currLoad.parent = null;

		currLoad.localScale = Vector3.one;
		
		currLoad = null;
	}

	void UpdateInput()
	{
		inp = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

		if (GameManager.main.stopPlayer)
			inp = Vector2.zero;
	}
}
