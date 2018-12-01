using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;

public class GameManager : MonoBehaviour
{

	public float stormDamage;
	
	public static GameManager main;

	void Awake()
	{
		main = this;
	}
	
	void Start () {
		
	}
	
	void Update () {
		
	}
}
