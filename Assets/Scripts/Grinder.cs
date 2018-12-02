using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Grinder : MonoBehaviour
{
	public bool isUpgrader = false;
	
	public GameObject batteryPrefab;
	
	[Space]
	
	public Transform enterTop;
	public Transform enterPoint;
	
	[Space] 
	
	public Transform outPoint;

	public Transform outOffset;

	public ParticleSystem steamParticle;
	public ParticleSystem bloodParticle;

	public AudioSource grinderSound;
	
	void Start () {
		
	}
	
	void Update () {
		
	}

	public void MountSomething(Transform load)
	{
		GinionController ginion = load.GetComponent<GinionController>();

		if (!ginion)
		{
			Debug.LogError("Bu ginion değil, yanlış şey soktunuz bana!");
			return;
		}

		ginion.ReadyGrinding();
		
		StartCoroutine(playAnimation(load,ginion));
	}

	IEnumerator playAnimation(Transform load,GinionController ginion)
	{
		load.DOMove(enterTop.position, 1f).SetEase(Ease.InOutQuart);

		yield return new WaitForSeconds(1.1f);
		
		load.DOMove(enterPoint.position, 1f).SetEase(Ease.OutBounce);

		steamParticle.emissionRate = 20;

		steamParticle.startSpeed = 3f;

		grinderSound.DOFade(1,1);
		grinderSound.DOPitch(1.4f, 1);
		
		yield return new WaitForSeconds(1.1f);

		bloodParticle.Play();
		
		if (ginion.isDed)
		{
			ginion.Destroy();
			
			steamParticle.emissionRate = 3;

			steamParticle.startSpeed = 1f;
			
			
			grinderSound.DOFade(0.2f,1);
			grinderSound.DOPitch(1, 1);
			
			yield break;
		}
		
		ginion.Destroy();

		AudioManager.main.Play("die");

		yield return new WaitForSeconds(0.1f);
		
		AudioManager.main.Play("gore");
	
		GameManager.main.ginionsSacrificed++;
		
		GameManager.main.PopNotification(PlayerController.main.transform.position + Vector3.up * 2f,"Ginion Sacrificed",Color.red);

		if (isUpgrader)
		{
			yield return new WaitForSeconds(1.2f);
			
			CanvasController.main.MovePanel(true);
			
			grinderSound.DOFade(0.2f,1);
			grinderSound.DOPitch(1, 1);
			
			steamParticle.emissionRate = 3;

			steamParticle.startSpeed = 1f;
		}
		else
		{

			yield return new WaitForSeconds(1.5f);

			grinderSound.DOFade(0.2f,1);
			grinderSound.DOPitch(1, 1);
			
			steamParticle.emissionRate = 3;

			steamParticle.startSpeed = 1f;

			GameObject battery = Instantiate(batteryPrefab, outPoint.position, Quaternion.identity);

			battery.transform.DOMove(outOffset.position, 1f).SetEase(Ease.InOutQuart);
		}
	}
}
