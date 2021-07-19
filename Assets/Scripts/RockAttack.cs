using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerEnemy") {
			Destroy (other.gameObject, 0f);
		}
	}
}
