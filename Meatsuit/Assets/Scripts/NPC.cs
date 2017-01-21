using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	Spawner spawner;
	float walkspeed;
	GameManager gm;

	public GameObject arrow;
	// Use this for initialization

	public bool selected;
	//bool selected;

	void Start () {
		
		spawner = GameObject.FindWithTag ("Spawner").GetComponent<Spawner> ();
		walkspeed = Random.Range (0.06f, 0.1f);

		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
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
				
			}
		}
	}

	public void selectNPC(bool closest){
		selected = closest;
		arrow.SetActive (closest);
	}

}
