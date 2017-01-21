using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmManager : MonoBehaviour {

	private Vector3 target;

	bool waving;

	UIManager uimanager;


	// Use this for initialization
	void Start () {
		uimanager = GameObject.FindWithTag ("UIManager").GetComponent<UIManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		FollowMouse ();
		CheckWave ();
	}

	void FollowMouse(){
		target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target = new Vector3 (target.x, target.y+3, 10);
		//target.z = transform.position.z;
		transform.position = target;
	}

	void CheckWave(){
		if (waving) {
			uimanager.currentSuspicion = 0f;
		}

	}
}
