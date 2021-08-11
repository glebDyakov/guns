using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAttack : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerEnemy") {
			Destroy (other.gameObject, 0f);
		}
	}
}
