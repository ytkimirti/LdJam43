using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public Transform target;

	public Vector3 offset;

	public float inpOffset;
	
	public float lerpSpeed;
	
	void Start () {
		
	}
	
	void LateUpdate ()
	{
		//Vector2 inp = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
		
		
		
		//transform.position = Vector3.Lerp(transform.position, target.position + offset + (Vector3)(inp * inpOffset), lerpSpeed * Time.deltaTime);


		transform.position = target.position + offset;
	}
}
