using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipChargerController : MonoBehaviour {
	
	public BatteryController currBattery;
	public Animator animator;
	public Transform batteryPoint;
	public ShipController ship;
	
	public bool isWorking;
	public bool isReady;
	
	void Start () {
		
	}
	
	void Update ()
	{
		isWorking = currBattery;

		isReady = false;
		
		if (isWorking && currBattery.juice > 0)
			isReady = true;
		
		if (isWorking && ship.loadJuice)
		{
			currBattery.juice -= Time.deltaTime * 5;

			if (currBattery.juice < 0)
			{
				isWorking = false;
				currBattery.juice = 0;
			}
		}
		else
		{
			isWorking = false;
		}
		
		animator.SetBool("isCharging",isWorking);
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
}
