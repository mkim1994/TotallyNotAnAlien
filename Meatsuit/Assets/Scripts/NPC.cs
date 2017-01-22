using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	AudioManager audiomanager;
	ArmManager armmanager;

	Spawner spawner;
	float walkspeed;
	GameManager gm;

	AudioSource audio;

	public GameObject arrow;
	// Use this for initialization

	public bool selected;
	//bool selected;

	bool wavedAt;


	void Start () {

		walkspeed = Random.Range (0.06f, 0.1f);
		spawner = GameObject.FindWithTag ("Spawner").GetComponent<Spawner> ();

		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();

		audiomanager = GameObject.FindWithTag ("AudioManager").GetComponent<AudioManager> ();

		armmanager = GameObject.FindWithTag ("ArmManager").GetComponent<ArmManager> ();

		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!gm.isArrested) {
			if (transform.localScale.x > 0) { //facing left
				transform.position -= new Vector3 (walkspeed, 0f);
			} else { //facing right
				transform.position += new Vector3 (walkspeed, 0f);
			}

			if (transform.position.x > spawner.npcSpawnPoint1.x || transform.position.x < spawner.npcSpawnPoint2.x) {
				gm.NPCs.Remove (this.gameObject);
				Destroy (this.gameObject);
			}

			if (selected) {
				if (armmanager.waving && !wavedAt) {
					wavedAt = true;
					arrow.GetComponent<SpriteRenderer> ().color = new Color(1,1,1,0.3f);
					audio.clip = audiomanager.transform.GetChild (2).gameObject.GetComponent<AudioSource> ().clip;
					audio.Play ();
				}
			}
		}
	}

	public void selectNPC(bool closest){
		selected = closest;
		arrow.SetActive (closest);
	}

}
