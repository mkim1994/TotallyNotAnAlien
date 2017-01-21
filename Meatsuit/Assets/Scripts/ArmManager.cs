using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmManager : MonoBehaviour {

	private Vector3 target;

	bool waving;
	public float waveThreshold;

	UIManager uimanager;

	bool armMovedLeft;
	bool armMovedRight;



	// Use this for initialization
	void Start () {
		uimanager = GameObject.FindWithTag ("UIManager").GetComponent<UIManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0)){
			FollowMouse ();
			CheckWave ();
		}

		FingerKeys ();
	}


	void FingerKeys(){
		

	}

	void FollowMouse(){
		target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target = new Vector3 (target.x, target.y+3, 10);
		transform.position = target;
	}

	void CheckWave(){
		if (waving) {
			uimanager.currentSuspicion = 0f;
			waving = false;
		}
		if (Input.GetAxis ("Mouse X") < -waveThreshold) {
			armMovedLeft = true;
			if (armMovedRight) {
				waving = true;
			}
			armMovedRight = false;
		}
		if (Input.GetAxis ("Mouse X") > waveThreshold) {
			armMovedRight = true;
			if (armMovedLeft) {
				waving = true;
			}
			armMovedLeft = false;
		}
	}
}
