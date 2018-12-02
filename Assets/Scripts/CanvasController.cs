using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{

	public bool isPanelOpen;
	public Transform panelTrans;

	public float panelOpenPos;
	float panelClosedPos;

	public TextMeshProUGUI[] texts;
	public string[] upgradeTexts;
	private int currSelected;

	public static CanvasController main;

	private void Awake()
	{
		main = this;
	}

	void Start ()
	{
		panelClosedPos = panelTrans.position.y;
	}
	
	void Update () {
		if (isPanelOpen)
		{
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				currSelected--;
			}
			else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			{
				currSelected++;
			}

			if (currSelected == 3)
				currSelected = 0;
			else if (currSelected == -1)
				currSelected = 2;


			if (Input.GetKeyDown(KeyCode.Space))
			{
				print("Selected " + upgradeTexts[currSelected]);

				MovePanel(false);

				if (currSelected == 0)
				{
					PlayerController.main.maxSpeed += 2.6f;
					
					GameManager.main.PopNotification(PlayerController.main.transform.position + Vector3.up * 2f,"Speed Increased",Color.green);
				}else if (currSelected == 1)
				{
					PlayerController.main.maxJuice += 45;
					
					GameManager.main.PopNotification(PlayerController.main.transform.position + Vector3.up * 2f,"Capacity Increased",Color.green);
					
				}else if (currSelected == 2)
				{
					FindObjectOfType<HeaterController>().IncreaseRadius(1.2f);
					
					GameManager.main.PopNotification(FindObjectOfType<HeaterController>().gameObject.transform.position + Vector3.up * 2f,"Range Increased",Color.green);
				}
			}
			
			for (int i = 0; i < 3; i++)
			{
				if (currSelected == i)
				{
					texts[currSelected].text = upgradeTexts[currSelected] + " (Selected)";
				}
				else
				{
					texts[i].text = upgradeTexts[i];
				}
			}
		}
	}

	public void MovePanel(bool open)
	{
		if (open == isPanelOpen)
			return;

		isPanelOpen = open;

		if (open)
		{
			panelTrans.DOMoveY(panelOpenPos, 1f);
			GameManager.main.stopPlayer = true;
		}
		else
		{			
			panelTrans.DOMoveY(panelClosedPos,1f);
			GameManager.main.stopPlayer = false;
		}
	}
}
