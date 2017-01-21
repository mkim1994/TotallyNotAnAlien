using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public float startingSuspicion = 0f;
	public float currentSuspicion;
	public float suspicionRate; //0.001;

	public bool isArrested;


	void Awake () {
		Time.timeScale = 1f;
		currentSuspicion = startingSuspicion;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentSuspicion >= 1f & !isArrested) {
			Arrested ();
		}
		
	}

	void Arrested(){
		isArrested = true;
		Time.timeScale = 0f;
	}
}
