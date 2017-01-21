using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SuspicionMeter : MonoBehaviour {

	public float startingSuspicion = 0f;
	public float currentSuspicion;
	public Slider suspicionSlider;
	//public Image 

	public float suspicionRate;

	public bool isArrested;


	// Use this for initialization
	void Start () {
		currentSuspicion = startingSuspicion;
	}
	
	// Update is called once per frame
	void Update () {
		suspicionClimb ();	
	}

	void suspicionClimb(){
		currentSuspicion += suspicionRate;
		suspicionSlider.value = currentSuspicion;

		if (currentSuspicion >= 1f & !isArrested) {
			Arrested ();
		}
	}

	void Arrested(){
		isArrested = true;
		print (isArrested);
	}
}
