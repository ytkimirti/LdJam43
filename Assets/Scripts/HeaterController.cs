using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
			currBattery.juice -= Time.deltaTime * 2;

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

		battery.transform.DOMove(batteryPoint.position, 0.4f).SetEase(Ease.InOutQuart).OnComplete(TickSound);
		currBattery = battery;
	}

	void TickSound()
	{
		AudioManager.main.Play("tick");
	}

	public void IncreaseRadius(float val)
	{
		safezoneRadius += val;
		lightEffect.transform.localScale = Vector3.one * safezoneRadius * 2;
	}
}
