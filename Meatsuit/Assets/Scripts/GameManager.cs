using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		if (currentSuspicion >= 1f & !isArrested) {
			Arrested ();
		}
	}

	void Arrested(){
		isArrested = true;
		audiomanager.sirensound.Play ();

		uimanager.gameOverText.gameObject.SetActive (true);
	//	Camera.main.GetComponent<CameraShake> ().enabled = true;
	//	Camera.main.GetComponent<CameraShake> ().shakeDuration = 0.5f;


		Time.timeScale = 0f;
	}
}
