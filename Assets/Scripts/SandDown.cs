using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 2.5f);
	}
	void OnTriggerStay2D (Collider2D other){
		other.transform.position = new Vector2(other.transform.position.x, other.transform.position.y - 0.5f);
	}
}
