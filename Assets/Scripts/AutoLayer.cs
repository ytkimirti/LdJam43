using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AutoLayer : MonoBehaviour {

    SortingGroup spriteGroup;
    SpriteRenderer spriteRenderer;
    int memPos;

	bool isEmpty;
	bool isSpriteRen;
	
	void Start () {
        spriteGroup = GetComponent<SortingGroup>();

		spriteRenderer = GetComponent<SpriteRenderer>();

		isSpriteRen = true;
		if (spriteGroup)
			isSpriteRen = false;
		else if (!spriteRenderer && !spriteGroup)
		{
			isEmpty = true;
		}
	}
	
	void Update ()
	{
		if (isEmpty)
			return;
		
        int currPos = Mathf.RoundToInt(transform.position.y * -10f);

        if (currPos != memPos)
        {
	        if (isSpriteRen)
		        spriteRenderer.sortingOrder = currPos;
            else
		        spriteGroup.sortingOrder = currPos;
        }

        memPos = currPos;
    }
}
