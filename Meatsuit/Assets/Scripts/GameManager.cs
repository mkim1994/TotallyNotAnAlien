using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public float startingSuspicion = 0f;
	public float currentSuspicion;
	public float suspicionRate; //0.001;

	public bool isArrested;

	AudioManager audiomanager;
	UIManager uimanager;

	public List<GameObject> NPCs;

	void Awake () {
		Time.timeScale = 1f;
		currentSuspicion = startingSuspicion;
		uimanager = GameObject.FindWithTag ("UIManager").GetComponent<UIManager> ();
		audiomanager = GameObject.FindWithTag ("AudioManager").GetComponent<AudioManager>();

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
		Camera.main.GetComponent<CameraShake> ().enabled = true;
		Camera.main.GetComponent<CameraShake> ().shakeDuration = 0.5f;


		Time.timeScale = 0f;
	}
}
