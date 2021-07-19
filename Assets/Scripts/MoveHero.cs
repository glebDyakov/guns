using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHero : MonoBehaviour {

	public ShootCore gun;
	KeyCode keyboardShoot;
	public List<AudioClip> clips;

	public GameObject duloOfGun;
	public bool killed = false;
	public float speed = 300f / 3f / 20f;
	public float playerNumber;
	KeyCode keyboardRight;
	KeyCode keyboardLeft;
	KeyCode keyboardJump;
	KeyCode keyboardTurnUp;
	KeyCode keyboardTurnDown;
	public bool jump = true;
	Rigidbody2D rb;
	public GameObject healthBar;
	public GameObject controlPoint;
	AudioSource audio;
	public AudioClip moveHero;
	public AudioClip jumpHero;
	public AudioClip ouchHero;
	public AudioClip victoryHero;
	public AudioClip changeLightHero;
	public AudioClip grabeLightHero;
	public bool withLight = false;
	public static float hp = 100f;

	void Awake(){
		audio = GetComponent<AudioSource> ();
	}

	void Start(){
		
		if (playerNumber == 1f) {
			keyboardLeft = KeyCode.A;
			keyboardRight =  KeyCode.D;
			keyboardJump = KeyCode.Space;
			keyboardTurnUp = KeyCode.W;
			keyboardTurnDown = KeyCode.S;
		} else if(playerNumber == 2f){
			keyboardRight =  KeyCode.L;
			keyboardLeft = KeyCode.J;
			keyboardJump = KeyCode.O;
			keyboardTurnUp = KeyCode.I;
			keyboardTurnDown = KeyCode.K;
		} else if(playerNumber == 3f){
			keyboardRight =  KeyCode.RightArrow;
			keyboardLeft = KeyCode.LeftArrow;
			keyboardJump = KeyCode.LeftShift;
			keyboardTurnUp = KeyCode.UpArrow;
			keyboardTurnDown = KeyCode.DownArrow;
		} else if(playerNumber == 4f){
			keyboardRight =  KeyCode.PageUp;
			keyboardLeft = KeyCode.PageDown;
			keyboardJump = KeyCode.Home;
			keyboardTurnUp = KeyCode.Asterisk;
			keyboardTurnDown = KeyCode.Slash;
		}

		if (playerNumber == 1f) {
			keyboardShoot = KeyCode.E;
		} else if(playerNumber == 2f){
			keyboardShoot = KeyCode.N;
		} else if(playerNumber == 3f){
			keyboardShoot = KeyCode.KeypadEnter;
		} else if(playerNumber == 4f){
			keyboardShoot = KeyCode.KeypadPlus;
		}

	}

	/*
	void Update(){
		if (Input.GetKeyDown (KeyCode.E) && withLight) {
			movableLight.transform.parent = allLight.transform;
		}
	}
	*/

	void FixedUpdate(){
		rb = GetComponent<Rigidbody2D> ();
		if (Input.GetKeyUp (keyboardLeft)) {
			rb.velocity = new Vector2(0f, -1.8f);
		}else if (Input.GetKeyUp (keyboardRight)) {
			rb.velocity = new Vector2(0f, -1.8f);
		}
		if (Input.GetKey (keyboardLeft)) {
			GetComponent <SpriteRenderer> ().flipX = GetComponent <SpriteRenderer> ().flipX ? false : false;
			//rb.MovePosition (transform.position - transform.right * speed * Time.fixedDeltaTime);
			float fallVelocity = rb.velocity.y < 0 ? rb.velocity.y * 1.13f : rb.velocity.y;
			rb.velocity = new Vector2(-5f, fallVelocity);
		} else if(Input.GetKey (keyboardRight)) {
			GetComponent <SpriteRenderer> ().flipX = !GetComponent <SpriteRenderer> ().flipX ? true : true;
			//rb.MovePosition (transform.position + transform.right * speed * Time.fixedDeltaTime);
			float fallVelocity = rb.velocity.y < 0 ? rb.velocity.y * 1.13f : rb.velocity.y;
			rb.velocity = new Vector2(5f, fallVelocity);
		} else if(Input.GetKey (keyboardTurnUp) && gun.gameObject.GetComponent<Rigidbody2D>().velocity.x <= 0) {
			//rb.MovePosition (transform.position + transform.right * speed * Time.fixedDeltaTime);
			duloOfGun = gun.gameObject.transform.GetChild(0).gameObject;
			Rigidbody2D rbdog = duloOfGun.GetComponent<Rigidbody2D> ();
			//float angle = playerNumber == 1f || playerNumber == 2f ? 10f : -10f ;
			if (playerNumber == 1f || playerNumber == 2f) {
				if (rbdog.GetComponent<Rigidbody2D> ().rotation < 35f - 10f) {
					rbdog.MoveRotation (rbdog.rotation + 10f * Time.fixedDeltaTime);
				}
			} else if (playerNumber == 3f || playerNumber == 4f) {
				if(rbdog.GetComponent<Rigidbody2D>().rotation > -35f + 10f){
					rbdog.MoveRotation (rbdog.rotation - 10f * Time.fixedDeltaTime);
				}
			}
		} else if(Input.GetKey (keyboardTurnDown) && gun.gameObject.GetComponent<Rigidbody2D>().velocity.x <= 0) {
			//rb.MovePosition (transform.position + transform.right * speed * Time.fixedDeltaTime);
			Rigidbody2D rbdog = duloOfGun.GetComponent<Rigidbody2D> ();
			//float angle = playerNumber == 3f || playerNumber == 4f ? -10f : 10f ;
			if (playerNumber == 3f || playerNumber == 4f) {
				if (rbdog.GetComponent<Rigidbody2D> ().rotation < 15f - 10f) {
					rbdog.MoveRotation (rbdog.rotation + 10f * Time.fixedDeltaTime);
				}
			} else if(playerNumber == 1f || playerNumber == 2f){
				if (rbdog.GetComponent<Rigidbody2D> ().rotation > -15f + 10f) {	
					rbdog.MoveRotation (rbdog.rotation - 10f * Time.fixedDeltaTime);
				}
			}
		}
		if (Input.GetKeyUp (keyboardJump)) {
			Invoke ("StopJump", 0.01f);
			if (jump) {
				rb.AddRelativeForce (new Vector2 (gameObject.transform.localPosition.x, 175f * 100f), ForceMode2D.Force);
			}
		}
	}

	void StopJump(){
		jump = false;
	}

	void OnCollisionEnter2D(Collision2D other){
		
	}

	void OnTriggerEnter2D(Collider2D other){
		
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.tag == "Platform"){
			jump = true;
		}
	}

	void OnCollisionStay2D(Collision2D other){
		if(other.gameObject.tag == "Platform"){
			jump = true;
		}
	}

	public bool ShootFromGun(){
		if (Input.GetKeyUp (keyboardShoot)) {
			// проверяем заряжена ли пушка хотя бы 1 патроном
			if(gun.bullets.Count >= 1){
				//if(!gun.gameObject.name.Contains("Rod")){
					return true;
				//} else if(gun.gameObject.name.Contains("Rod")){
				//	gameObject.GetComponent<AudioSource> ().clip = clips [1];
				//	gameObject.GetComponent<AudioSource> ().Play ();
				//	return false;
				//}

			} else if (gun.bullets.Count <= 0) {
				gameObject.GetComponent<AudioSource> ().clip = clips [1];
				gameObject.GetComponent<AudioSource> ().Play ();
			}
		}
		return false;
	}

}
