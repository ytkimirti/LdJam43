using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaterController : MonoBehaviour
{

	public float safezoneRadius;
	
	public BatteryController currBattery;
	public Animator animator;
	public Transform batteryPoint;

	public GameObject lightEffect;
	
	public bool isWorking;
	
	void Start () {
		
	}
	
	void Update ()
	{
		isWorking = currBattery;
		
		if (isWorking)
		{
			currBattery.juice -= Time.deltaTime * 5;

			if (currBattery.juice < 0)
			{
				isWorking = false;
				currBattery.juice = 0;
			}
		}

		lightEffect.SetActive(isWorking);
		
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
