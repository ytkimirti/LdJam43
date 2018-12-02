using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerController : MonoBehaviour
{

	public BatteryController currBattery;
	public Animator animator;
	public Transform batteryPoint;

	public bool isCharging;
	private bool isInside;
	
	void Start () {
		
	}
	
	void Update () {
		if (currBattery && currBattery.juice != 0 && isInside)
		{
			isCharging = true;
			currBattery.juice -= Time.deltaTime * 7;
			if (currBattery.juice < 0)
			{
				isCharging = false;
				currBattery.juice = 0;
			}
		}
		else
		{
			isCharging = false;
		}
		
		animator.SetBool("isCharging",isCharging);
	}

	public void MountBattery(Transform batteryTrans)
	{
		BatteryController battery = null;
		
		if (batteryTrans)
		{
			battery = batteryTrans.gameObject.GetComponent<BatteryController>();
		}

		if (!battery)
			return;

		battery.transform.position = batteryPoint.position;
		currBattery = battery;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		print(other.tag);

		if (other.tag == "Player")
		{
			isInside = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			isInside = false;
		}
	}
}
