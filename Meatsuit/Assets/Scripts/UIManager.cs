using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public Slider suspicionSlider;
	public Text gameOverText;
	public Text winText;
	public Text fucksQuotaUI;
	public Text victoryQuotaUI;

	//public Image 

	GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		suspicionClimb ();
	}

	void suspicionClimb(){
		gm.currentSuspicion += gm.suspicionRate;
		suspicionSlider.value = gm.currentSuspicion;
	}
}
