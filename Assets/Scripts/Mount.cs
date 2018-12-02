using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mount : MonoBehaviour {

	
	void Start () {
		
	}

	public virtual void MountSomething(Transform target)
	{
		print("mounting " + target);
	}
}
