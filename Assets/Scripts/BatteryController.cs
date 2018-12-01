using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryController : MonoBehaviour
{

	public float juice;
	public Transform juiceIndicator;
	
	void Start ()
	{
		juice = 100;
	}
	
	void Update () {
		juiceIndicator.localScale = new Vector3(juiceIndicator.localScale.x,juice / 100,1);
	}
}
