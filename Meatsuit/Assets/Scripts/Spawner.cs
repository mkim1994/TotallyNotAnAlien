using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	GameManager gm;

	public GameObject[] NPCPrefabs;

	public Vector3 npcSpawnPoint1;
	public Vector3 npcSpawnPoint2;

/*	public float timeSpawnMin;
	public float timeSpawnMax;*/

	private float timeUntilSpawn;

	private float timeBetweenSpawns;

	public void Start()
	{
		npcSpawnPoint1 = Camera.main.ViewportToWorldPoint (new Vector3 (1.2f, 0.4f, 10.0f));
		npcSpawnPoint2 = Camera.main.ViewportToWorldPoint (new Vector3 (-0.3f, 0.4f, 10.0f));

		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();

		InvokeRepeating ("spawnrateIncrease",gm.spawnrateIncreaseInterval,gm.spawnrateIncreaseInterval);
	}

	public void Update()
	{
		
		timeBetweenSpawns = (Random.Range (gm.timeSpawnMin,gm.timeSpawnMax)) + gm.spawnrate;
		//Time.delaTime is how much time has occured since the last update. 
		//We subtract it from time until spawn every frame
		timeUntilSpawn -= Time.deltaTime;
		//Once timeUntilSpawn is less than 0, we spawn a new NPC
		if (timeUntilSpawn <= 0)
		{
			SpawnThings();
			//then we reset timeUntilSpawn to the timeBetweenSpawns & start all over again
			timeUntilSpawn = timeBetweenSpawns;
		}
	}

	void spawnrateIncrease(){
		gm.spawnrate += gm.spawnrateIncrease;
	}

	private void SpawnThings()
	{

		Vector3 newPos;
		Vector3 newScale;
		float possibility = Random.Range (0f, 10f);
		int randomScale = Random.Range (0,7);
		float actualScale = 0.7f;

		switch (randomScale) {
		case 0:
			actualScale = 0.5f;
			break;
		case 1:
			actualScale = 0.6f;
			break;
		case 2:
			actualScale = 0.7f;
			break;
		case 3:
			actualScale = 0.8f;
			break;
		case 4:
			actualScale = 0.9f;
			break;
		case 5:
			actualScale = 1.0f;
			break;
		case 6:
			actualScale = 1.1f;
			break;
		}

		int order = randomScale;
		if (possibility < 5f) {
			newPos = npcSpawnPoint1;
			newScale = new Vector3 (actualScale,actualScale);

		}  else {
			newPos = npcSpawnPoint2;
			newScale = new Vector3 (-1f*actualScale, actualScale);
		}

		int r = Random.Range (0, NPCPrefabs.Length);
		GameObject npc = Instantiate (NPCPrefabs[r], newPos, Quaternion.identity) as GameObject;
		npc.transform.localScale = newScale;
		npc.GetComponent<SpriteRenderer> ().sortingOrder = order;

		gm.NPCs.Add (npc);

	}
}


