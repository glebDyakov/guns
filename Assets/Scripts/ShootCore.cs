using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCore : MonoBehaviour {

	public float shootTimeStamp = 0f;
	public GameObject bulletOrigin;
	public List<AudioClip> clips;

	public List<string> bullets;
	public GameObject duloGun;
	private Rigidbody2D rb2D;
	public float playerNumber;
	private float thrust = 100.0f;
	public float strike;
	public GameObject core;
	public GameObject pointOfCore;
	public GameObject coreInst;
	KeyCode keyboardShoot;

	public string command;

	PhotonView photonView;

	//public float isLeftTeam = 20f;

	/*
	void FixedUpdate () {
		rb2D = gameObject.GetComponent<Rigidbody2D>();
		//rb2D.AddForce(transform.right * 50f * Time.fixedDeltaTime);	
		rb2D.AddRelativeForcew(transform.right * thrust * Time.fixedDeltaTime, ForceMode2D.Impulse);
		rb2D.AddRelativeForce(transform.up * 25f * Time.fixedDeltaTime, ForceMode2D.Impulse);
	}
	*/
	/*
		должно быть пересечение игрока с пушкой а не пушки с ядром
	*/
	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "PlayerEnemy") {
			if (other.gameObject.GetComponent<MoveHero> ().ShootFromGun () && shootTimeStamp + 5 < Time.timeSinceLevelLoad) {
				
				shootTimeStamp = Time.timeSinceLevelLoad;

				// здесь раньше была логика которая проверяла на клавишу стрельбы
				// эту логику перенём в функцию которую вызываю выше


				// coreInst = Instantiate (core, bulletOrigin.transform.position, Quaternion.identity);
				coreInst = PhotonNetwork.Instantiate (core.gameObject.name, bulletOrigin.transform.position, Quaternion.identity, 0);
				

				coreInst.GetComponent<CoreAttack> ().insideGun = true;
				coreInst.GetComponent<CoreAttack> ().gun = gameObject;
				//coreInst.name = "Core";
				Rigidbody2D coreInstrb = coreInst.GetComponent<Rigidbody2D> ();
				bullets.RemoveAt (0);


				if (gameObject.name.Contains ("Gun")) {
					coreInstrb.AddRelativeForce (gameObject.transform.right * strike, ForceMode2D.Impulse);
					float heightAttack = 0f;
					heightAttack = playerNumber == 1f ? 50f * duloGun.transform.localRotation.z : -50f * duloGun.transform.localRotation.z;

					coreInstrb.AddRelativeForce (gameObject.transform.up * heightAttack, ForceMode2D.Impulse);
					//rb2D.AddRelativeForce (transform.right * strike, ForceMode2D.Impulse);
					//rb2D.AddRelativeForce (transform.up * 10f, ForceMode2D.Impulse);

					gameObject.GetComponent<AudioSource> ().clip = clips [0];
					gameObject.GetComponent<AudioSource> ().Play ();
				} else if (gameObject.name.Contains ("Firework")) {
					coreInstrb.AddRelativeForce (gameObject.transform.right * Random.Range (1, strike + 1), ForceMode2D.Impulse);
					float heightAttack = playerNumber == 1f ? Random.Range (-1, -50) * duloGun.transform.localRotation.z : Random.Range (1, 50) * duloGun.transform.localRotation.z;
					coreInstrb.AddRelativeForce (gameObject.transform.up * heightAttack, ForceMode2D.Impulse);

					gameObject.GetComponent<AudioSource> ().clip = clips [1];
					gameObject.GetComponent<AudioSource> ().Play ();
				} else if(gameObject.name.Contains ("Spear")){

					/*
					coreInst.GetComponent<ConstantForce2D> ().enabled = true;
					if (other.gameObject.tag == "Player") {
						coreInst.GetComponent<ConstantForce2D>().relativeForce = new Vector2(15f, 0f);
					} else if (other.gameObject.tag == "PlayerEnemy") {
						coreInst.GetComponent<ConstantForce2D>().relativeForce = new Vector2(-15f, 0f);
					}
					*/

					coreInstrb.AddRelativeForce (gameObject.transform.right * strike, ForceMode2D.Impulse);
					float heightAttack = 0f;
					//heightAttack = playerNumber == 1f || playerNumber == 2f ? 50f * duloGun.transform.localRotation.z : -50f * duloGun.transform.localRotation.z;
					heightAttack = Mathf.Abs(50f * duloGun.transform.localRotation.z);

					coreInstrb.AddRelativeForce (gameObject.transform.up * heightAttack, ForceMode2D.Impulse);
					//rb2D.AddRelativeForce (transform.right * strike, ForceMode2D.Impulse);
					//rb2D.AddRelativeForce (transform.up * 10f, ForceMode2D.Impulse);

					gameObject.GetComponent<AudioSource> ().clip = clips [0];
					gameObject.GetComponent<AudioSource> ().Play ();

					gameObject.GetComponent<AudioSource> ().clip = clips [2];
					gameObject.GetComponent<AudioSource> ().Play ();
				} else if(gameObject.name.Contains ("Rod")){

					coreInst.GetComponent<CoreAttack> ().gun = gameObject;
					GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;

					coreInst.GetComponent<TrailRenderer> ().enabled = true;
					//coreInst.GetComponent<TrailRenderer>(). = new Vector2(15f, 0f);

					coreInstrb.AddRelativeForce (gameObject.transform.right * strike, ForceMode2D.Impulse);
					float heightAttack = 0f;
					heightAttack = playerNumber == 1f ? 50f * duloGun.transform.localRotation.z : -50f * duloGun.transform.localRotation.z;

					coreInstrb.AddRelativeForce (gameObject.transform.up * heightAttack, ForceMode2D.Impulse);

					gameObject.GetComponent<AudioSource> ().clip = clips [3];
					gameObject.GetComponent<AudioSource> ().Play ();
				}

				Invoke ("AddCore", 2f);
			
			}
		}
	}

	public void OnEvent(byte eventCode, object content, int senderId){
		if (eventCode == 33) {
			try {
				object[] data = (object[])content;
				string coreName = (string)data[0];
				// GameObject core = (GameObject)data[0];
				GameObject core = GameObject.Find(coreName);
				float posX = (float)data[1];
				core.GetComponent<CoreAttack> ().gameSettings = GameObject.FindWithTag("GameSettings").GetComponent<SetBackground>();
				core.transform.position = new Vector2(posX, pointOfCore.transform.position.y);
				core.GetComponent<CoreAttack> ().insideGun = false;	
			} catch (System.InvalidCastException e) {
				Debug.Log("InvalidCastException поймал 2");
			}
		}
	}

	public void AddCore(){
		float randomPositionX = Random.Range(-24, 24f);
		GameObject coreNew = PhotonNetwork.Instantiate (core.gameObject.name, new Vector2(randomPositionX, pointOfCore.transform.position.y), Quaternion.identity, 0);
		// PhotonNetwork.RaiseEvent(33, new object[] { coreNew, randomPositionX }, true, new RaiseEventOptions { Receivers = ReceiverGroup.All });
		PhotonNetwork.RaiseEvent(33, new object[] { coreNew.name, randomPositionX }, true, new RaiseEventOptions { Receivers = ReceiverGroup.All });
		
		/*
		закомментировал потому что нельзя называться coreInst то что не для стрельбы а только для подбора
		coreInst = Instantiate (core.gameObject, new Vector2(pointOfCore.transform.position.x, pointOfCore.transform.position.y), Quaternion.identity, gameObject.transform);
		coreInst.transform.localPosition = new Vector2(0f, 0f);
		*/

		/*
		float randomPositionX = Random.Range(-24, 24f);
		GameObject coreNew = PhotonNetwork.Instantiate (core.gameObject.name, new Vector2(randomPositionX, pointOfCore.transform.position.y), Quaternion.identity, 0);

		coreNew.GetComponent<CoreAttack> ().gameSettings = GameObject.FindWithTag("GameSettings").GetComponent<SetBackground>();
		coreNew.transform.position = new Vector2(randomPositionX, pointOfCore.transform.position.y);
		coreNew.GetComponent<CoreAttack> ().insideGun = false;
		*/
	}

	void Start () {
		
		PhotonNetwork.OnEventCall += OnEvent;
		photonView = PhotonView.Get(this);
		
		//isLeftTeam = gameObject.GetComponent<SpriteRenderer> ().flipX ? -20f : 20f;
		coreInst = core;
		//rb2D = core.GetComponent<Rigidbody2D>();


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

}
