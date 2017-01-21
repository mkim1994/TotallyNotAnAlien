﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmManager : MonoBehaviour {

	private Vector3 target;

	public bool waving;
	public float waveThreshold;

	UIManager uimanager;
	GameManager gm;

	bool armMovedLeft;
	bool armMovedRight;

	GameObject hand;
	GameObject[] fingers;
	bool[] fingerState;


	// Use this for initialization
	void Start () {
		uimanager = GameObject.FindWithTag ("UIManager").GetComponent<UIManager> ();
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();

		hand = this.gameObject.transform.GetChild(1).gameObject;
		fingers = new GameObject[5];
		fingerState = new bool[5];
		for (int i = 0; i < fingers.Length; i++) {
			fingers [i] = hand.transform.GetChild (i).gameObject;
			fingerState [i] = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!gm.isArrested) {
			FollowMouse ();
			CheckWave ();
			FingerKeys ();

			//GameObject[] gos = GameObject.FindGameObjectsWithTag ("NPC");
			GameObject closestNPC = GetClosestNPC ();
			closestNPC.GetComponent<NPC> ().selectNPC (true);
			foreach (GameObject npc in gm.NPCs) {
				if (npc != closestNPC) {
					npc.GetComponent<NPC> ().selectNPC (false);
				}
			}
			
		}
	}

	GameObject GetClosestNPC(){
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		//int max = gm.NPCs [0].GetComponent<SpriteRenderer> ().sortingOrder;
		foreach (GameObject go in gm.NPCs) {
			//if(

			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

	void FollowMouse(){
		target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		target = new Vector3 (target.x, target.y + 3, 10);
		if(Input.GetMouseButtonDown(0)){
			iTween.MoveTo (this.gameObject, target, 0.2f);
		} else if (Input.GetMouseButton (0)) {
			transform.position = target;
		} 
	}

	void CheckWave(){
		if (Input.GetMouseButton (0)) {
			if (waving) {
				gm.currentSuspicion = 0f;
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

	void CheckGesture(){

	}

	void FingerKeys(){
		if (Input.GetKeyDown (KeyCode.Tab)) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y);
		}

		if (Input.GetKeyDown (KeyCode.A)) { //thumb
			if (fingers [0].activeSelf) {
				fingers [0].SetActive (false);
				fingerState [0] = false;
			} else {
				fingers [0].SetActive (true);
				fingerState [0] = true;
			}
		}

		if (Input.GetKeyDown (KeyCode.S)) { //index
			if (fingers [1].activeSelf) {
				fingers [1].SetActive (false);
				fingerState [1] = false;
			} else {
				fingers [1].SetActive (true);
				fingerState [1] = true;
			}
		}

		if (Input.GetKeyDown (KeyCode.D)) { //middle
			if (fingers [2].activeSelf) {
				fingers [2].SetActive (false);
				fingerState [2] = false;
			} else {
				fingers [2].SetActive (true);
				fingerState [2] = true;
			}
		}

		if (Input.GetKeyDown (KeyCode.F)) { //ring
			if (fingers [3].activeSelf) {
				fingers [3].SetActive (false);
				fingerState [3] = false;
			} else {
				fingers [3].SetActive (true);
				fingerState [3] = true;
			}
		}

		if (Input.GetKeyDown (KeyCode.G)) { //pinky
			if (fingers [4].activeSelf) {
				fingers [4].SetActive (false);
				fingerState [4] = false;
			} else {
				fingers [4].SetActive (true);
				fingerState [4] = true;
			}
		}

	}


}
