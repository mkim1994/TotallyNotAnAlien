using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmManager : MonoBehaviour {

	public Sprite finger;
	//public Sprite fingerBent;

	public Sprite handInside;
	public Sprite handOutside;

	public bool handFlipped;

	private Vector3 target;

	public bool waving;
	public float waveThreshold;

	public bool waveGesture;
	public bool fuckGesture;
	public bool victoryGesture;

	public bool[] gestureFlags;

	//UIManager uimanager;
	GameManager gm;

	bool armMovedLeft;
	bool armMovedRight;

	GameObject hand;
	public GameObject[] fingers;
	public GameObject[] fingersBent;
	bool[] fingerState;

	//Vector3[] fingerPositions;
	//public Vector3[] fingerBentPositions;

	// Use this for initialization
	void Start () {
	//	uimanager = GameObject.FindWithTag ("UIManager").GetComponent<UIManager> ();
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();

		hand = this.gameObject.transform.GetChild(1).gameObject;
		fingerState = new bool[5];
		for (int i = 0; i < fingerState.Length; i++) {
			fingerState [i] = true;
		}

		gestureFlags = new bool[6];
	}
	
	// Update is called once per frame
	void Update () {
		if (!gm.isArrested) {

			//GameObject[] gos = GameObject.FindGameObjectsWithTag ("NPC");
			GameObject closestNPC = GetClosestNPC ();
			closestNPC.GetComponent<NPC> ().selectNPC (true);
			foreach (GameObject npc in gm.NPCs) {
				if (npc != closestNPC) {
					npc.GetComponent<NPC> ().selectNPC (false);
				}
			}
			FollowMouse ();
			CheckWave ();
			FingerKeys ();
			CheckGesture ();
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

		//check what gesture you're doing
		for (int i = 0; i < fingers.Length; i++) {
			gestureFlags [i] = fingers [i].activeSelf;
		}
		gestureFlags [5] = hand.GetComponent<SpriteRenderer> ().sprite == handOutside;

		//match it with the gesture data
		//wave
		if (gestureFlags [0] && gestureFlags [1] && gestureFlags [2] && gestureFlags [3] && gestureFlags [4] && gestureFlags [5]) {
			waveGesture = true;
			fuckGesture = false;
			victoryGesture = false;
		} else if ((gestureFlags [0] || !gestureFlags[0]) && !gestureFlags [1] && gestureFlags [2] && !gestureFlags [3] && !gestureFlags [4] && !gestureFlags [5]) {
			fuckGesture = true;
			waveGesture = false;
			victoryGesture = false;
		} else if (!gestureFlags [0] && gestureFlags [1] && gestureFlags [2] && !gestureFlags [3] && !gestureFlags [4] && gestureFlags [5]) {
			victoryGesture = true;
			fuckGesture = false;
			waveGesture = false;
		} else {
			waveGesture = false;
			fuckGesture = false;
			victoryGesture = false;

		}


	}

	void FingerKeys(){

		//check if fingers should be front of or behind the hand
		if (handFlipped) {
			for (int i = 0; i < fingersBent.Length; i++) {
				if (fingersBent [i].activeSelf) {
					fingersBent [i].GetComponent<SpriteRenderer> ().sortingOrder = 2;
				}
			}
		} else {
			for (int i = 0; i < fingersBent.Length; i++) {
				fingersBent [i].GetComponent<SpriteRenderer> ().sortingOrder = 0;
			}
		}

		//handflip
		if (Input.GetKeyDown (KeyCode.Tab)) {
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y);
			if (!handFlipped) {
				handFlipped = true;
				hand.GetComponent<SpriteRenderer> ().sprite = handInside;

			} else {
				handFlipped = false;
				hand.GetComponent<SpriteRenderer> ().sprite = handOutside;
			}
			
		}

		if (Input.GetKeyDown (KeyCode.A)) { //thumb
			if (fingers [0].activeSelf) {
				fingers [0].SetActive (false);
				fingersBent [0].SetActive (true);
			} else {
				fingersBent [0].SetActive (false);
				fingers [0].SetActive (true);
			}
		}

		if (Input.GetKeyDown (KeyCode.S)) { //index
			if (fingers [1].activeSelf) {
				fingers [1].SetActive (false);
				fingersBent [1].SetActive (true);
			} else {
				fingersBent [1].SetActive (false);
				fingers [1].SetActive (true);
			}
		}

		if (Input.GetKeyDown (KeyCode.D)) { //middle
			if (fingers [2].activeSelf) {
				fingers [2].SetActive (false);
				fingersBent [2].SetActive (true);
			} else {
				fingersBent [2].SetActive (false);
				fingers [2].SetActive (true);
			}
		}

		if (Input.GetKeyDown (KeyCode.F)) { //ring-
			if (fingers [3].activeSelf) {
				fingers [3].SetActive (false);
				fingersBent [3].SetActive (true);
			} else {
				fingersBent [3].SetActive (false);
				fingers [3].SetActive (true);
			}
		}

		if (Input.GetKeyDown (KeyCode.G)) { //pinky
			if (fingers [4].activeSelf) {
				fingers [4].SetActive (false);
				fingersBent [4].SetActive (true);
			} else {
				fingersBent [4].SetActive (false);
				fingers [4].SetActive (true);
			}
		}
	}
}
