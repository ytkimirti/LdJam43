using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking.Match;

public class GameManager : MonoBehaviour
{

	public GameObject notificationPrefab;
	
	public bool stopPlayer;
	
	public GameObject ginionPrefab;
	
	public float stormDamage;

	public int spawnCount;

	public int spawnCountIncrease;

	public float waveDelay;

	public float waveDelayIncrease;

	private float waveTimer;

	public Vector2 spawnArea;

	public float flyOffset;

	[Header("Stats")] 
	
	public int ginionsSacrificed;

	public int ginionsSaved;

	public int ginionsDiedFromCold;
	
	public static GameManager main;

	void Awake()
	{
		main = this;
	}
	
	void Start () {
		
	}
	
	void Update ()
	{
		waveTimer += Time.deltaTime;

		if (waveTimer > waveDelay)
		{
			waveTimer = 0;
			SpawnNextWave();
			IncreaseHardness();
		}
	}

	void SpawnNextWave()
	{
		StartCoroutine(SpawnGinions());
	}

	public void PopNotification(Vector2 pos, string text,Color col)
	{
		GameObject go = Instantiate(notificationPrefab, pos, Quaternion.identity);
		
		go.GetComponent<FastNotification>().play(text,col);
	}
	
	IEnumerator SpawnGinions()
	{
		for (int i = 0; i < spawnCount; i++)
		{			
			yield return new WaitForSeconds(Random.Range(1f,2f));
			
			Vector2 randPos = new Vector2(Random.Range(-spawnArea.x,spawnArea.x),Random.Range(-spawnArea.y,spawnArea.y));

			Transform ginion = SpawnGinion(randPos);

			ginion.position = randPos + (Vector2.up * flyOffset);

			ginion.DOMoveY(randPos.y,1.3f).SetEase(Ease.OutBounce);

			StartCoroutine(OnLandComplete(randPos));
		}
	}

	IEnumerator OnLandComplete(Vector2 pos)
	{
		yield return new WaitForSeconds(0.5f);
		ParticleManager.main.play(pos,0);
		yield return new WaitForSeconds(0.4f);
		ParticleManager.main.play(pos,0);
	}

	Transform SpawnGinion(Vector3 pos)
	{
		GameObject ginion = Instantiate(ginionPrefab, pos, Quaternion.identity);

		return ginion.transform;
	}

	void IncreaseHardness()
	{
		spawnCount += spawnCountIncrease;

		waveDelay += waveDelayIncrease;
		
		
	}
}
