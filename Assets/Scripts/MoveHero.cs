using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(PhotonView))]
// [RequireComponent(typeof(PhotonAnimatorView))]

public class MoveHero : MonoBehaviour {

	public bool leftArrow = false;
	public bool rightArrow = false;
	public bool rotateClockwise = false;
	public bool rotateUnclockwise = false;
	public bool jumpButton = false;
	public bool grabButton = false;
	public bool shootButton = false;

	public ShootCore gun;
	KeyCode keyboardShoot;
	KeyCode keyboardGrab;
	public bool grab = false;
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

	PhotonView photonView;
	public PhotonView photonViewOfGun;
	public PhotonView photonViewOfDuloGun;

	public SetBackground gameSettings;

	void Awake(){
		audio = GetComponent<AudioSource> ();
	}

	void Start(){

		PhotonNetwork.OnEventCall += OnEvent;
		photonView = PhotonView.Get(this);
		
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

		if (playerNumber == 1f) {
			keyboardGrab = KeyCode.Q;
		} else if(playerNumber == 2f){
			keyboardGrab = KeyCode.U;
		} else if(playerNumber == 3f){
			keyboardGrab = KeyCode.P;
		} else if(playerNumber == 4f){
			keyboardGrab = KeyCode.M;
		}

	}

	/*
	void Update(){
		if (Input.GetKeyDown (KeyCode.E) && withLight) {
			movableLight.transform.parent = allLight.transform;
		}
	}
	*/

	[PunRPC]
	void MoveOtherPlayer(int playerIndex, string side, Rigidbody2D rb, SpriteRenderer sr){
		if(side.Contains("left")){
			sr.flipX = sr.flipX ? false : false;
			float fallVelocity = rb.velocity.y < 0 ? rb.velocity.y * 1.13f : rb.velocity.y;
			rb.velocity = new Vector2(-5f, fallVelocity);
		} else if(side.Contains("right")){
			sr.flipX = !sr.flipX ? true : true;
			float fallVelocity = rb.velocity.y < 0 ? rb.velocity.y * 1.13f : rb.velocity.y;
			rb.velocity = new Vector2(5f, fallVelocity);
		}
	}

	void FixedUpdate(){
		if(PlayerPrefs.GetInt("PlayerIndex") == playerNumber) {
			rb = GetComponent<Rigidbody2D> ();
			if(Input.GetKeyUp (keyboardGrab) || grabButton){
				photonView.TransferOwnership(PhotonNetwork.player.ID);
				grab = false;
				GetComponent<FixedJoint2D>().connectedBody = null;
				GetComponent<FixedJoint2D>().enabled = false;
			}
			if (Input.GetKeyUp (keyboardLeft) || leftArrow) {
				photonView.TransferOwnership(PhotonNetwork.player.ID);
				
				photonViewOfGun.TransferOwnership(PhotonNetwork.player.ID);
				photonViewOfDuloGun.TransferOwnership(PhotonNetwork.player.ID);
				
				rb.velocity = new Vector2(0f, -1.8f);
			} else if (Input.GetKeyUp (keyboardRight) || rightArrow) {
				photonView.TransferOwnership(PhotonNetwork.player.ID);
				
				photonViewOfGun.TransferOwnership(PhotonNetwork.player.ID);
				photonViewOfDuloGun.TransferOwnership(PhotonNetwork.player.ID);
				
				rb.velocity = new Vector2(0f, -1.8f);
			}
			if (Input.GetKey (keyboardLeft) || leftArrow) {
				// GetComponent <SpriteRenderer> ().flipX = GetComponent <SpriteRenderer> ().flipX ? false : false;
				// int flipSide = GetComponent <SpriteRenderer> ().flipX ? 1 : 0;
				 float flipSide = 0f;

				PhotonNetwork.RaiseEvent(199, new object[] { float.Parse((playerNumber - 1).ToString()), flipSide }, true, new RaiseEventOptions { Receivers = ReceiverGroup.All });
				//rb.MovePosition (transform.position - transform.right * speed * Time.fixedDeltaTime);
				float fallVelocity = rb.velocity.y < 0 ? rb.velocity.y * 1.13f : rb.velocity.y;
				rb.velocity = new Vector2(-5f, fallVelocity);

				photonView.TransferOwnership(PhotonNetwork.player.ID);

				photonViewOfGun.TransferOwnership(PhotonNetwork.player.ID);
				photonViewOfDuloGun.TransferOwnership(PhotonNetwork.player.ID);

				// print("PhotonNetwork.playerList: " + PhotonNetwork.playerList.Length.ToString());
				// photonView.RPC("MoveOtherPlayer", PhotonTargets.All, PlayerPrefs.GetInt("PlayerIndex"), "left", rb, GetComponent <SpriteRenderer> ());
				// photonView.RPC("MoveOtherPlayer", PhotonNetwork.playerList[0], PlayerPrefs.GetInt("PlayerIndex"), "left", rb, GetComponent <SpriteRenderer> ());
				// photonView.RPC("MoveOtherPlayer", PhotonNetwork.playerList[1], PlayerPrefs.GetInt("PlayerIndex"), "left", rb, GetComponent <SpriteRenderer> ());
				// photonView.RPC("MoveOtherPlayer", PhotonNetwork.playerList[2], PlayerPrefs.GetInt("PlayerIndex"), "left", rb, GetComponent <SpriteRenderer> ());
				// photonView.RPC("MoveOtherPlayer", PhotonNetwork.playerList[3], PlayerPrefs.GetInt("PlayerIndex"), "left", rb, GetComponent <SpriteRenderer> ());

			} else if(Input.GetKey (keyboardRight) || rightArrow) {
				// GetComponent <SpriteRenderer> ().flipX = !GetComponent <SpriteRenderer> ().flipX ? true : true;
				// int flipSide = GetComponent <SpriteRenderer> ().flipX ? 1 : 0;
				float flipSide = 1f;

				PhotonNetwork.RaiseEvent(199, new object[] { float.Parse((playerNumber - 1).ToString()), flipSide }, true, new RaiseEventOptions { Receivers = ReceiverGroup.All });
				
				//rb.MovePosition (transform.position + transform.right * speed * Time.fixedDeltaTime);
				float fallVelocity = rb.velocity.y < 0 ? rb.velocity.y * 1.13f : rb.velocity.y;
				rb.velocity = new Vector2(5f, fallVelocity);

				photonView.TransferOwnership(PhotonNetwork.player.ID);

				photonViewOfGun.TransferOwnership(PhotonNetwork.player.ID);
				photonViewOfDuloGun.TransferOwnership(PhotonNetwork.player.ID);

				// print("PhotonNetwork.playerList: " + PhotonNetwork.playerList.Length.ToString());
				// photonView.RPC("MoveOtherPlayer", PhotonTargets.All, PlayerPrefs.GetInt("PlayerIndex"), "left", rb, GetComponent <SpriteRenderer> ());
				// photonView.RPC("MoveOtherPlayer", PhotonNetwork.playerList[0], PlayerPrefs.GetInt("PlayerIndex"), "left", rb, GetComponent <SpriteRenderer> ());
				// photonView.RPC("MoveOtherPlayer", PhotonNetwork.playerList[1], PlayerPrefs.GetInt("PlayerIndex"), "left", rb, GetComponent <SpriteRenderer> ());
				// photonView.RPC("MoveOtherPlayer", PhotonNetwork.playerList[2], PlayerPrefs.GetInt("PlayerIndex"), "left", rb, GetComponent <SpriteRenderer> ());
				// photonView.RPC("MoveOtherPlayer", PhotonNetwork.playerList[3], PlayerPrefs.GetInt("PlayerIndex"), "left", rb, GetComponent <SpriteRenderer> ());

			} else if((Input.GetKey (keyboardTurnUp) || rotateClockwise) && gun.gameObject.GetComponent<Rigidbody2D>().velocity.x <= 0) {
				duloOfGun = gun.gameObject.transform.GetChild(0).gameObject;
				// photonView.TransferOwnership(duloOfGun.GetComponent<PhotonView>().viewID);
				
				// rb.MovePosition (transform.position + transform.right * speed * Time.fixedDeltaTime);
				
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
	
				photonView.TransferOwnership(PhotonNetwork.player.ID);
			
				photonViewOfGun.TransferOwnership(PhotonNetwork.player.ID);
				photonViewOfDuloGun.TransferOwnership(PhotonNetwork.player.ID);

			} else if((Input.GetKey (keyboardTurnDown) || rotateUnclockwise) && gun.gameObject.GetComponent<Rigidbody2D>().velocity.x <= 0) {
				duloOfGun = gun.gameObject.transform.GetChild(0).gameObject;
				// photonView.TransferOwnership(duloOfGun.GetComponent<PhotonView>().viewID);
				
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

				photonView.TransferOwnership(PhotonNetwork.player.ID);

				photonViewOfGun.TransferOwnership(PhotonNetwork.player.ID);
				photonViewOfDuloGun.TransferOwnership(PhotonNetwork.player.ID);
			
			}
			if (Input.GetKeyUp (keyboardJump) || jumpButton) {
				photonView.TransferOwnership(PhotonNetwork.player.ID);
				Invoke ("StopJump", 0.01f);
				if (jump) {
					rb.AddRelativeForce (new Vector2 (gameObject.transform.localPosition.x, 175f * 100f), ForceMode2D.Force);
				}
			}
		}
	}

	void StopJump(){
		jump = false;
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag.Contains("Gun")){
			
			// PhotonNetwork.RaiseEvent(0, new object[] { new Vector3(10.0f, 2.0f, 5.0f), 1, 2, 5, 10 }, true, new RaiseEventOptions { Receivers = ReceiverGroup.All });
			
			photonView.TransferOwnership(PhotonNetwork.player.ID);
			photonViewOfGun.TransferOwnership(PhotonNetwork.player.ID);
			photonViewOfDuloGun.TransferOwnership(PhotonNetwork.player.ID);

		} else if(other.gameObject.tag.Contains("Core")){
			
			photonView.TransferOwnership(PhotonNetwork.player.ID);
			other.gameObject.GetComponent<CoreAttack>().photonView.TransferOwnership(PhotonNetwork.player.ID);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.tag == "Platform"){
			jump = true;
		} else if(other.gameObject.tag.Contains("Gun") || other.gameObject.tag.Contains("Core")){
			if((Input.GetKey(keyboardGrab) || grabButton) && !grab){
				grab = true;
				GetComponent<FixedJoint2D>().connectedBody = other.gameObject.GetComponent<Rigidbody2D>();
				GetComponent<FixedJoint2D>().enabled = true;
			}
		}
	}

	void OnCollisionStay2D(Collision2D other){
		if(other.gameObject.tag == "Platform"){
			jump = true;
		}
	}

	public bool ShootFromGun(){
		if (Input.GetKeyUp (keyboardShoot) || shootButton) {
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

	void OnGUI() {
		// leftArrow = GUI.RepeatButton (new Rect (25, Screen.height - 150f, 50, 50), "<");
		// rightArrow = GUI.RepeatButton (new Rect (125 - 300f,  Screen.height - 150f, 50, 50), ">");
		// rotateClockwise = GUI.RepeatButton (new Rect (125 - 300f,  Screen.height - 150f, 50, 50), ">");
		// rotateUnclockwise = GUI.RepeatButton (new Rect (125 - 300f,  Screen.height - 150f, 50, 50), ">");
		// jumpButton = GUI.RepeatButton (new Rect (125 - 300f,  Screen.height - 150f, 50, 50), ">");
		// grabButton = GUI.RepeatButton (new Rect (125 - 300f,  Screen.height - 150f, 50, 50), ">");	
	
		GUILayout.BeginArea(new Rect(0f, 0f, Screen.width, Screen.height));
		GUILayout.Width(150);
		GUILayout.Height(50);
		leftArrow = GUILayout.RepeatButton ("<");
		rightArrow = GUILayout.RepeatButton (">");
		rotateClockwise = GUILayout.RepeatButton ("^");
		rotateUnclockwise = GUILayout.RepeatButton ("_");
		jumpButton = GUILayout.RepeatButton ("A");
		grabButton = GUILayout.RepeatButton ("B");	
		shootButton = GUILayout.RepeatButton ("C");	
		GUILayout.EndArea();
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		Debug.Log("OnPhotonSerializeView");
		Debug.Log("stream.isWriting: " + stream.isWriting.ToString());
		Debug.Log("info.timestamp: " + info.timestamp.ToString());
	}

	public void OnEvent(byte eventCode, object content, int senderId) {
		if(eventCode == 199){
			// try {
				object[] data = (object[])content;
					
				float playerIndex = (float)data[0];
				
				// int flipSide = (int)data[data.Length - 1];
				float flipSide = (float)data[1];
				
				int playerNum = int.Parse(playerIndex.ToString());

				Debug.Log("playerIndex" + playerIndex.ToString());
				Debug.Log("flipSide" + flipSide.ToString());
				
				// SetBackground gameSettings = GameObject.Find("GameSettings").GetComponent<SetBackground>();
				
				// gameSettings.players[playerIndex].GetComponent<SpriteRenderer>().flipX = flipSide == 0f ? true : false;
				gameSettings.players[playerNum].GetComponent<SpriteRenderer>().flipX = flipSide == 0f ? false : true;
			
			// } catch (System.InvalidCastException e) {
				// Debug.Log("InvalidCastException поймал 4");
			// }
		}
	}
}
