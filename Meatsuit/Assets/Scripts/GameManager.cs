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
	public float currentSuspicion;
	public float suspicionRate; //0.001;

	[Header("Suspicion Meter Interaction with Gestures")]
	public float suspicionSpike;
	public float suspicionDown;


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


	}
	
	// Update is called once per frame
	void Update () {
		if (!win) {
			if (currentSuspicion >= 1f & !isArrested) {
				Arrested ();
			}

			CheckQuota ();
			if (fuckQuotaMet && victoryQuotaMet) {
				win = true;
			}
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


		Time.timeScale = 0f;
	}

	void Win(){
		uimanager.winText.gameObject.SetActive (true);
		Time.timeScale = 0f;

	}
}
