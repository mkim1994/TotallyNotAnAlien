using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	AudioManager audiomanager;
	ArmManager armmanager;

	Spawner spawner;
	float walkspeed;
	GameManager gm;
	UIManager uimanager;

	//AudioSource audio;
	new AudioSource audio;
	new Animator anim;

	public GameObject arrow;
	// Use this for initialization

	public bool selected;
	//bool selected;

	bool reacted;
	bool paused;

	Vector3 reactionLeft;
	Vector3 reactionRight;


	void Start () {

		spawner = GameObject.FindWithTag ("Spawner").GetComponent<Spawner> ();
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		audiomanager = GameObject.FindWithTag ("AudioManager").GetComponent<AudioManager> ();
		armmanager = GameObject.FindWithTag ("ArmManager").GetComponent<ArmManager> ();
		uimanager = GameObject.FindWithTag ("UIManager").GetComponent<UIManager> ();
		audio = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();

		walkspeed = Random.Range (gm.walkspeedMin,gm.walkspeedMax);


		reactionLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0.08f, 0.5f, 10.0f));
		reactionRight = Camera.main.ViewportToWorldPoint (new Vector3 (0.92f, 0.5f, 10.0f));
	}
	
	// Update is called once per frame
	void Update () {
		if (!gm.isArrested || !gm.win) {
			if (!paused) {
				if (transform.localScale.x > 0) { //facing left
					transform.position -= new Vector3 (walkspeed, 0f);
				} else { //facing right
					transform.position += new Vector3 (walkspeed, 0f);
				}
			}

			if (transform.position.x > spawner.npcSpawnPoint1.x || transform.position.x < spawner.npcSpawnPoint2.x) {
				gm.NPCs.Remove (this.gameObject);
				Destroy (this.gameObject);
			}

			if (selected) {
				
				if (!reacted && (transform.position.x < reactionRight.x && transform.position.x > reactionLeft.x)) {
					if (armmanager.waveGesture && armmanager.waving) {
						paused = true;
						anim.SetTrigger ("npcWaves");

						gm.currentSuspicion *= gm.suspicionDown;
						reacted = true;
						arrow.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.3f);
						audio.clip = audiomanager.transform.GetChild (2).gameObject.GetComponent<AudioSource> ().clip;
						audio.Play ();

					} else if (armmanager.fuckGesture) {
						paused = true;
						anim.SetTrigger ("npcAngers");

						gm.numFucks++;
						if (!uimanager.fucksQuotaUI.gameObject.activeSelf) {
							uimanager.fucksQuotaUI.gameObject.SetActive (true);
						}

						gm.currentSuspicion *= gm.suspicionSpike;
						reacted = true;
						arrow.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.3f);
						audio.clip = audiomanager.transform.GetChild (3).gameObject.GetComponent<AudioSource> ().clip;
						audio.Play ();

					} else if(armmanager.victoryGesture){
						gm.numVictories++;
						if (!uimanager.victoryQuotaUI.gameObject.activeSelf) {
							uimanager.victoryQuotaUI.gameObject.SetActive (true);
						}
						//gm.currentSuspicion = gm.startingSuspicion;

						//just a passive quota meet

						reacted = true;
						arrow.GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, 0.3f);
						//audio.clip = audiomanager.transform.GetChild (2).gameObject.GetComponent<AudioSource> ().clip;
						//audio.Play ();
					}
				}
			}
		}
	}

	public void selectNPC(bool closest){
		selected = closest;
		arrow.SetActive (closest);
	}

	public void resumeNPCHappyWalk(){
		paused = false;
		anim.SetTrigger ("npcHappyWalks");
	}

	public void resumeNPCAngerWalk(){
		paused = false;
		anim.SetTrigger ("npcAngerWalks");
	}

}
