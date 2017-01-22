using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[Header("Quotas")]
	public int maxFucks;
	public int maxVictories;

	public float startingSuspicion = 0.001f;
	public float currentSuspicion;
	public float suspicionRate; //0.001;

	public float suspicionSpike;
	public float suspicionDown;

	public bool isArrested;

	public int numFucks;
	public int numVictories;
	public bool fuckQuotaMet;
	public bool victoryQuotaMet;

	public bool win;

	AudioManager audiomanager;
	UIManager uimanager;

	ArmManager armmanager;

	public List<GameObject> NPCs;

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
