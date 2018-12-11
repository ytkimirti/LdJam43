using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
	public GameObject gameEndBadScene;
	public bool isPanelOpen;
	public Transform panelTrans;
	public Transform endPanel;

	public TextMeshProUGUI sacrificeText;
	public TextMeshProUGUI coldText;
	public TextMeshProUGUI savedText;
	
	public TextMeshProUGUI positiveText;
	public TextMeshProUGUI negativeText;
	private bool isPositiveSelected = true;

	public float panelOpenPos;
	float panelClosedPos;

	public TextMeshProUGUI[] texts;
	public string[] upgradeTexts;
	private int currSelected;

	public static CanvasController main;

	public GameObject creditObject;

	private bool isEndGamePanelOpen;
	public AudioMixer mixer;

	private void Awake()
	{
		main = this;
	}

	void Start ()
	{
		panelClosedPos = panelTrans.position.y;
	}

	public void RollCredits()
	{
		creditObject.SetActive(true);
		mixer.SetFloat("aud", 0);

		coldText.text = GameManager.main.ginionsDiedFromCold.ToString() + " Ginions died from cold";
		sacrificeText.text = "You sacrificed " + GameManager.main.ginionsSacrificed.ToString() + " cute Ginions for batteries and upgrades";
		savedText.text = "You saved " + GameManager.main.ginionsSaved.ToString() + " Ginions";

	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(0);
		}
		
		if (isEndGamePanelOpen)
		{
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				isPositiveSelected = !isPositiveSelected;
			}
			else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
			{
				isPositiveSelected = !isPositiveSelected;
			}

			if (isPositiveSelected)
			{
				positiveText.text = "Yes (Press Enter)";
				negativeText.text = "No, robots don't have hearts you fool";
			}
			else
			{
				positiveText.text = "Yes";
				negativeText.text = "No, robots don't have hearts you fool (Press Enter)";
			}

			if (Input.GetKeyDown(KeyCode.Return))
			{

				if (isPositiveSelected)
				{
					EndGamePanel(false);
					FindObjectOfType<ShipController>().PlayAnimation();
					
					PlayerController.main.Die(true);
				}
				else
				{
					GameManager.main.PopNotification(PlayerController.main.transform.position + Vector3.up * 2f,"I think I have a hearth",Color.red);
				}
				
			}
		}
		
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
				
				AudioManager.main.Play("powerup");
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

	public void EndGamePanel(bool open)
	{
		isEndGamePanelOpen = open;
		
		if (open)
		{
			GameManager.main.stopPlayer = true;
			Time.timeScale = 0.1f;
			endPanel.DOMoveY(panelOpenPos, 0.1f);
		}
		else
		{
			GameManager.main.stopPlayer = true;
			
			Time.timeScale = 1;
			endPanel.DOMoveY(panelClosedPos, 1f);
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
