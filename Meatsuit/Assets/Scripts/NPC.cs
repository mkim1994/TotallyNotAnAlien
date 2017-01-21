using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	Spawner spawner;
	float walkspeed;
	// Use this for initialization
	void Start () {
		spawner = GameObject.FindWithTag ("Spawner").GetComponent<Spawner> ();
		walkspeed = Random.Range (0.03f, 0.08f);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x > 0) { //facing left
			transform.position -= new Vector3(walkspeed,0f);
		} else { //facing right
			transform.position += new Vector3(walkspeed,0f);
		}

		if (transform.position.x > spawner.npcSpawnPoint1.x || transform.position.x < spawner.npcSpawnPoint2.x) {
			Destroy (this.gameObject);
		}
	}
}
