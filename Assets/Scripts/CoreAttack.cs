using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreAttack : MonoBehaviour {

	public GameObject gun;
	public bool insideGun;
	public GameObject playerNumberOne;
	public GameObject playerNumberTwo;
	public GameObject playerNumberThree;
	public GameObject playerNumberFour;
	public List<GameObject> killedCharacters;
	public GameObject allOfHeroTeam;
	public GameObject allOfEnemyTeam;
	public GameObject explosion;
	public string currentCommand;
	public List<Sprite> coresSprites;
	public List<string> commands = new List<string>(){
		"pirates",
		"clowns",
		"knights",
		"rods"
	};

	public SetBackground gameSettings;

	public PhotonView photonView;
		

	public void OnEvent(byte eventCode, object content, int senderId) {
		if (eventCode == 36) {
			try {
				object[] data = (object[])content;
				
				int commandIndex = (int)data[0];
				currentCommand = gameSettings.shuffledCommands[commandIndex];
		
				if(currentCommand.Contains("pirates")){
					GetComponent<SpriteRenderer>().sprite = coresSprites[0];
				} else if(currentCommand.Contains("clowns")){
					GetComponent<SpriteRenderer>().sprite = coresSprites[1];
				} else if(currentCommand.Contains("knights")){
					GetComponent<SpriteRenderer>().sprite = coresSprites[2];
				} else if(currentCommand.Contains("rods")){
					GetComponent<SpriteRenderer>().sprite = coresSprites[3];
				}

				PhotonNetwork.RaiseEvent(37, new object[] { commandIndex }, true, new RaiseEventOptions { Receivers = ReceiverGroup.All });
			
			} catch (System.InvalidCastException e) {
				Debug.Log("InvalidCastException поймал 3");
			}
		} else if (eventCode == 37) {
			try {
				object[] data = (object[])content;
				
				int commandIndex = (int)data[0];
			
				Debug.Log("CoreAttack37: " + commandIndex + " : " + PlayerPrefs.GetInt("PlayerIndex").ToString());
			} catch (System.InvalidCastException e) {
				Debug.Log("InvalidCastException поймал 3.2");
			}
		}
	}

	void Start(){
		PhotonNetwork.OnEventCall += OnEvent;
	 	photonView = PhotonView.Get(this);

		if(gameObject.name.Contains("Enemy")){
			playerNumberOne = GameObject.FindGameObjectsWithTag("PlayerEnemy")[0];
			playerNumberTwo = GameObject.FindGameObjectsWithTag("PlayerEnemy")[1];
			playerNumberThree = GameObject.FindGameObjectsWithTag("Player")[0];
			playerNumberFour = GameObject.FindGameObjectsWithTag("Player")[1];
		} else if(!gameObject.name.Contains("Enemy")){
			playerNumberOne = GameObject.FindGameObjectsWithTag("Player")[0];
			playerNumberTwo = GameObject.FindGameObjectsWithTag("Player")[1];
			playerNumberThree = GameObject.FindGameObjectsWithTag("PlayerEnemy")[0];
			playerNumberFour = GameObject.FindGameObjectsWithTag("PlayerEnemy")[1];
		}
		allOfHeroTeam = GameObject.FindWithTag("AllOfHeroTeam");
		allOfEnemyTeam = GameObject.FindWithTag("AllOfEnemyTeam");

		// currentCommand = commands[Random.Range(0, commands.Count)];
		// currentCommand = gameSettings.shuffledCommands[Random.Range(0, gameSettings.shuffledCommands.Count)];
		// currentCommand = SetBackground.shuffledCommands[Random.Range(0, 2)];
		
		//if(PhotonNetwork.isMasterClient){
			
			// int randomCommand = Random.Range(0, 2);
			// currentCommand = gameSettings.shuffledCommands[randomCommand];
			// if(currentCommand.Contains("pirates")){
			// 	GetComponent<SpriteRenderer>().sprite = coresSprites[0];
			// } else if(currentCommand.Contains("clowns")){
			// 	GetComponent<SpriteRenderer>().sprite = coresSprites[1];
			// } else if(currentCommand.Contains("knights")){
			// 	GetComponent<SpriteRenderer>().sprite = coresSprites[2];
			// } else if(currentCommand.Contains("rods")){
			// 	GetComponent<SpriteRenderer>().sprite = coresSprites[3];
			// }
			// PhotonNetwork.RaiseEvent(33, new object[] { randomCommand }, true, new RaiseEventOptions { Receivers = ReceiverGroup.Others });
			
			//PhotonNetwork.RaiseEvent(36, new object[] { Random.Range(0, 2) }, true, new RaiseEventOptions { Receivers = ReceiverGroup.All });
		
		//}

	}

	void OnCollisionEnter2D (Collision2D other) {

		bool isGun = other.gameObject.name.Contains ("Gun") || other.gameObject.name == "Firework" || other.gameObject.name.Contains ("Rod") || other.gameObject.name.Contains ("Spear");
		
		if (other.gameObject.tag.Contains ("Player")) {
			try {
				photonView.TransferOwnership(other.gameObject.GetComponent<PhotonView>().owner);
			} catch(System.Exception e) {
				Debug.Log("Ошибка передачи владельца ядра");
			}
			/*
			взрыв от выстрела ядром
			if(playerNumberOne == "Screamer" || playerNumberOne == "Character"){
				GameObject explosionInst = Instantiate (explosion, new Vector2(other.contacts[0].point.x, other.contacts[0].point.y), Quaternion.identity);
				Destroy (explosionInst, 0.2f);
			}
			*/

			//print (other.gameObject.GetComponent<MoveHero> ().killed);
			if (gameObject.name.Contains("Enemy")) {
				if (other.gameObject.tag == "Player") {
					other.gameObject.GetComponent<MoveHero> ().killed = true;
					/*
					killedCharacters.Add (other.gameObject);
					*/
					//if(other.gameObject.GetComponent){
					//other.gameObject.GetComponent<MoveHero> ().killed = true
					//}
					//Destroy (other.gameObject, 10f);

					//if (playerNumberOne.GetComponent<MoveHero> ().killed && playerNumberTwo.GetComponent<MoveHero> ().killed) {
					if (playerNumberThree.GetComponent<MoveHero> ().killed && playerNumberFour.GetComponent<MoveHero> ().killed) {
						//Invoke ("GameOver", 1f);
						GameOver();
					}
					//if (killedCharacters.FindAll(FindComputer)) {
					//Invoke ("GameOver", 1f);
					//}
					/*
					if (GameObject.FindGameObjectsWithTag("Player").Length == 0 || GameObject.FindGameObjectsWithTag ("PlayerEnemy").Length == 0) {
						Invoke ("GameOver", 1f);
					}
					*/
					/*
					if (GameObject.FindWithTag ("Player") == null || GameObject.FindWithTag ("PlayerEnemy") == null) {
						Invoke ("GameOver", 1f);
					}
					*/
					/*
					if(allOfHeroTeam.transform.childCount == 0 || allOfEnemyTeam.transform.childCount == 0){
						Invoke ("GameOver", 1f);
					}
					*/

				}
			} else if (!gameObject.name.Contains("Enemy")) {

				if (other.gameObject.tag == "PlayerEnemy") {
					other.gameObject.GetComponent<MoveHero> ().killed = true;
					//other.gameObject.killed = true;
					//killedCharacters.Add (other.gameObject);
					//Destroy (other.gameObject, 10f);

					//if (playerNumberOne.GetComponent<MoveHero> ().killed && playerNumberTwo.GetComponent<MoveHero> ().killed) {
					if (playerNumberThree.GetComponent<MoveHero> ().killed && playerNumberFour.GetComponent<MoveHero> ().killed) {

						//Invoke ("GameOver", 1f);
						GameOver();
					}
					/*
					if (GameObject.FindWithTag ("Player") == null || GameObject.FindWithTag ("PlayerEnemy") == null) {
						Invoke ("GameOver", 1f);
					}
					*/
					/*
					if (GameObject.FindGameObjectsWithTag("Player").Length == 0 || GameObject.FindGameObjectsWithTag ("PlayerEnemy").Length == 0) {
						Invoke ("GameOver", 1f);
					}
					/*
					/*
					if(allOfHeroTeam.transform.childCount == 0 || allOfEnemyTeam.transform.childCount == 0){
						Invoke ("GameOver", 1f);
					}
					*/
				}
			}
		} else if (isGun && !insideGun && currentCommand.Contains(other.gameObject.GetComponent<ShootCore>().command)) {
			if(!other.gameObject.name.Contains("Rod")){
				other.gameObject.GetComponent<ShootCore> ().bullets.Add ("newbullet");

				Debug.Log("Удаляем ядро по всей сети");
				// Destroy (gameObject, 0.5f);
				
				// if(PhotonNetwork.player.ID == photonView.owner.ID){
				// 	PhotonNetwork.Destroy(gameObject);
				// }

				// if(PlayerPrefs.GetInt("PlayerIndex") == 1){
				// 	PhotonNetwork.Destroy(gameObject);
				// }

				// if(PlayerPrefs.GetInt("PlayerIndex") == 1){
				// 	PhotonNetwork.Destroy(gameObject);
				// } else if(PlayerPrefs.GetInt("PlayerIndex") != 1 && photonView.ownerId == PhotonNetwork.player.ID){
				// 	PhotonNetwork.Destroy(gameObject);
				// }

				try {
					if(photonView.ownerId == PhotonNetwork.player.ID && gameObject != null){
						PhotonNetwork.Destroy(gameObject);
					}
				} catch(System.Exception e) {
					Debug.Log("ошибка удаления 1");
				}

			} else if(other.gameObject.name.Contains("Rod") && other.gameObject.GetComponent<ShootCore> ().bullets.Count <= 0){
				photonView.TransferOwnership(other.gameObject.GetComponent<PhotonView>().owner);

				other.gameObject.GetComponent<ShootCore> ().bullets.Add ("newbullet");
				
				// Destroy (gameObject, 0.5f);
				
				// if(PhotonNetwork.player.ID == photonView.owner.ID){
				// 	PhotonNetwork.Destroy(gameObject);
				// }

				// if(PlayerPrefs.GetInt("PlayerIndex") == 1){
				// 	PhotonNetwork.Destroy(gameObject);
				// }

				// if(PlayerPrefs.GetInt("PlayerIndex") == 1){
				// 	PhotonNetwork.Destroy(gameObject);
				// } else if(PlayerPrefs.GetInt("PlayerIndex") != 1 && photonView.ownerId == PhotonNetwork.player.ID){
				// 	PhotonNetwork.Destroy(gameObject);
				// }

				try {
					if(photonView.ownerId == PhotonNetwork.player.ID && gameObject != null){
						PhotonNetwork.Destroy(gameObject);
					}
				} catch(System.Exception e) {
					Debug.Log("ошибка удаления 2");
				}

			}
		} else if ((other.gameObject.tag.Contains ("Platform") || other.gameObject.name.Contains ("Bounds")) && insideGun) {
			if (!other.gameObject.name.Contains ("Rod")) {
				gun.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
			}
			
			// Destroy (gameObject);
				
			// if(PhotonNetwork.player.ID == photonView.owner.ID){
			// 	PhotonNetwork.Destroy(gameObject);
			// }

			// if(PlayerPrefs.GetInt("PlayerIndex") == 1){
			// 	PhotonNetwork.Destroy(gameObject);
			// } else if(PlayerPrefs.GetInt("PlayerIndex") != 1){
			// 	photonView.TransferOwnership(PhotonNetwork.player.ID);
			// 	PhotonNetwork.Destroy(gameObject);
			// }

			// if(PlayerPrefs.GetInt("PlayerIndex") == 1){
			// 	PhotonNetwork.Destroy(gameObject);
			// } else if(PlayerPrefs.GetInt("PlayerIndex") != 1 && photonView.ownerId == PhotonNetwork.player.ID){
			// 	PhotonNetwork.Destroy(gameObject);
			// }

			try {
				if(photonView.ownerId == PhotonNetwork.player.ID && gameObject != null){
					PhotonNetwork.Destroy(gameObject);
				}
			} catch (System.Exception e) {
				Debug.Log("ошибка удаления 3");
			}

		}
	}

	/*
	private static bool FindComputer(GameObject go)
	{

		if (go.name == "Zek" || go.name == "Character"))
		{
			return go;
		}
	}
	*/
	public void GameOver(){
		print ("GameOver");
		
		// Application.LoadLevel (Application.loadedLevel);
		PhotonNetwork.LoadLevel(Application.loadedLevel);

	}

	public void OnBecameInvisible(){
		Destroy (gameObject);
	}

}
