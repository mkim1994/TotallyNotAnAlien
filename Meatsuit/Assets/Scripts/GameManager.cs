using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[Header("Quotas (win condition)")]
	public int maxFucks;
	public int maxVictories;

	[Header("Suspicion Meter Control")]
	public float startingSuspicion = 0.001f;
	public float suspicionRate; //0.001;

	[Header("Suspicion Meter Control (acceleration rate is additive, interval is in seconds)")]
	public float suspicionAccelerationRateIncrease;
	public float suspicionAccelerationRateIncreaseInterval;

	[Header("Suspicion Meter Interaction with Gestures")]
	public float suspicionSpike;
	public float suspicionDown;

	[Header("NPC Control (spawn time is random between timeSpawnMin & timeSpawnMax)")]
	public float timeSpawnMin;
	public float timeSpawnMax;
	[Header("NPC Control (spawnrate is additive, interval is in seconds)")]
	public float spawnrateIncrease;
	public float spawnrateIncreaseInterval;
	[Header("NPC Control (walkspeed is random between walkspeedMin & walkspeedMax)")]
	public float walkspeedMin;
	public float walkspeedMax;

	[HideInInspector]
	public float spawnrate, suspicionAccelerationRate, currentSuspicion;
	[HideInInspector]
	public bool isArrested, win;
	[HideInInspector]
	public int numFucks, numVictories;
	[HideInInspector]
	public bool fuckQuotaMet, victoryQuotaMet;
	[HideInInspector]
	public List<GameObject> NPCs;

	AudioManager audiomanager;
	UIManager uimanager;
	ArmManager armmanager;


	void Awake () {
		Time.timeScale = 1f;
		currentSuspicion = startingSuspicion;
		uimanager = GameObject.FindWithTag ("UIManager").GetComponent<UIManager> ();
		audiomanager = GameObject.FindWithTag ("AudioManager").GetComponent<AudioManager>();
		armmanager = GameObject.FindWithTag ("ArmManager").GetComponent<ArmManager> ();

		NPCs = new List<GameObject> ();
	}

	void Start(){

		InvokeRepeating ("suspicionRateIncrease",suspicionAccelerationRateIncreaseInterval,suspicionAccelerationRateIncreaseInterval);

	}
	
	// Update is called once per frame
	void Update () {
		if (!win) {
			if (currentSuspicion >= 1f && !isArrested) {
				Arrested ();
			}

			CheckQuota ();
			if (fuckQuotaMet && victoryQuotaMet) {
				win = true;
			}
				
			currentSuspicion += suspicionRate;
		} else {
			Win ();
		}

		//restart
		if(Input.GetKeyDown(KeyCode.Escape)){
			SceneManager.LoadScene ("main");
		}
		/*//return to main menu
		if*/

	}

	void suspicionRateIncrease(){
		suspicionAccelerationRate += suspicionAccelerationRateIncrease;
		suspicionRate += suspicionAccelerationRate;
	}

	void CheckQuota(){
		if (numFucks >= maxFucks) {
			fuckQuotaMet = true;
		}

		if (numVictories >= maxVictories) {
			victoryQuotaMet = true;
		}

		uimanager.fucksQuotaUI.text = numFucks+"/6";
		uimanager.victoryQuotaUI.text = numVictories + "/6";
	}

	void Arrested(){
		isArrested = true;
		audiomanager.sirensound.Play ();

		uimanager.gameOverText.gameObject.SetActive (true);
	//	Camera.main.GetComponent<CameraShake> ().enabled = true;
	//	Camera.main.GetComponent<CameraShake> ().shakeDuration = 0.5f;
		foreach(GameObject npc in NPCs){
			npc.GetComponent<NPC> ().paused = true;
		}

		Time.timeScale = 0f;
	}

	void Win(){

		foreach(GameObject npc in NPCs){
			npc.GetComponent<NPC> ().paused = true;
		}
		uimanager.winText.gameObject.SetActive (true);
		Time.timeScale = 0f;

	}
}
