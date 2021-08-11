using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(PhotonAnimatorView))]
public class SetBackground : MonoBehaviour {

	public GameObject sand;
	public List<GameObject> levelList;
	public List<string> commands;
	public List<GameObject> players;
	public List<Sprite> commandsSprites;
	public List<GameObject> commandsCores;
	public List<GameObject> commandsGuns;
	
	public string[] shuffledCommands;
	public string[] gunCases = new string[2];
	// public List<string> shuffledCommands;
	
	public List<GameObject> pointOfCores;

	PhotonView photonView;

	[PunRPC]
	void SetDefaultSettings(List<string> shuffledCommands){
		int levelNumber = 0;
		levelList [levelNumber].SetActive (true);
		commands = new List<string>(){
			"pirates",
			"clowns",
			"knights",
			"rods"
		};
		int commandIndex = 0;
		foreach(var command in shuffledCommands){
			commandIndex++;
			if (commandIndex == 1) {
				players [0].tag = "Player";
				players [1].tag = "Player";
				if (command.Contains ("pirates")) {
					players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [0], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
					players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
					players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Gun";

					players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [0];
					players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [1];


					players [0].GetComponent<MoveHero> ().gun.core = commandsCores [0];

					players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "pirates";

				} else if (command.Contains ("clowns")) {
					players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [2], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
					players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
					players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Firework";

					players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [2];
					players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [3];
					players [0].GetComponent<MoveHero> ().gun.core = commandsCores [2];

					players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "clowns";

				} else if (command.Contains ("knights")) {
					players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [4], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
					players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
					players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Spear";

					players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [4];
					players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [5];

					players [0].GetComponent<MoveHero> ().gun.core = commandsCores [4];

					players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "knights";

				} else if (command.Contains ("rods")) {
					players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [6], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
					players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
					players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Rod";

					players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [6];
					players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [7];
					players [0].GetComponent<MoveHero> ().gun.core = commandsCores [6];

					players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "rods";

				}
				players [0].GetComponent<MoveHero> ().gun.pointOfCore = GameObject.FindGameObjectsWithTag ("ControlPoint") [0];
				players [0].GetComponent<MoveHero> ().gun.strike = 20;
			} else if (commandIndex == 2) {
				if (command.Contains ("pirates")) {
					players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [1], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
					players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
					players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Gun";

					players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [0];
					players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [1];


					players [2].GetComponent<MoveHero> ().gun.core = commandsCores [0];

					players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "pirates";

				} else if (command.Contains ("clowns")) {
					players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [3], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
					players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
					players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Firework";

					players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [2];
					players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [3];
					players [2].GetComponent<MoveHero> ().gun.core = commandsCores [3];
					
					players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "clowns";

				} else if (command.Contains ("knights")) {
					players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [5], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
					players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
					players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Spear";

					players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [4];
					players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [5];


					players [2].GetComponent<MoveHero> ().gun.core = commandsCores [5];

					players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "knights";

				} else if (command.Contains ("rods")) {
					players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [7], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
					players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
					players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Rod";

					players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [6];
					players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [7];
					players [2].GetComponent<MoveHero> ().gun.core = commandsCores [7];

					players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "rods";

				}

				players [2].tag = "PlayerEnemy";
				players [3].tag = "PlayerEnemy";

				players [2].GetComponent<MoveHero> ().gun.pointOfCore = GameObject.FindGameObjectsWithTag ("ControlPoint") [1];
				players [2].GetComponent<MoveHero> ().gun.strike = -20;
			}
			if(commandIndex >= 2){
				break;
			}
		}
	}

	void Start() {
		
		PhotonNetwork.OnEventCall += OnEvent;
		
		photonView = PhotonView.Get(this);
		
		if(PlayerPrefs.GetInt("PlayerIndex") == 1){
			
		/*
			int levelNumber = Random.Range (0, levelList.Count);
			levelList [levelNumber].SetActive (true);
			закоментировал выше и вставил ниже так потому что персонажам и оружиям применяется сила не на уровне 0
		*/

			// int levelNumber = 0;
			// levelList [levelNumber].SetActive (true);

			// if(levelNumber == 0){
				
			/*
				позже надо раскоментировать чтобы включить зыбучие пески
				InvokeRepeating("Sands", 2f, 1f);	
			*/
			
			// } else if(levelNumber == 1){

			// } else if(levelNumber == 2){

			// }

			//логика распределения команд
			commands = new List<string>(){
				"pirates",
				"clowns",
				"knights",
				"rods"
			};
			shuffledCommands = commands.OrderBy(x => Random.value).ToArray<string>();
			int commandIndex = 0;
			foreach(var command in shuffledCommands){
				commandIndex++;
				if (commandIndex == 1) {
					players [0].tag = "Player";
					players [1].tag = "Player";
					if (command.Contains ("pirates")) {
						gunCases.SetValue(PhotonNetwork.Instantiate (commandsGuns [0].name, new Vector2 (-14.32f, -12.21f), Quaternion.identity, 0).GetComponent<ShootCore> ().name, 0);
						// players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [0], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
						
						// players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
						// players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Gun";

						// players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [0];
						// players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [1];


						// players [0].GetComponent<MoveHero> ().gun.core = commandsCores [0];

						// players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "pirates";

					} else if (command.Contains ("clowns")) {
						gunCases.SetValue(PhotonNetwork.Instantiate (commandsGuns [2].name, new Vector2 (-14.32f, -12.21f), Quaternion.identity, 0).GetComponent<ShootCore> ().name, 0);
						// players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [2], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
						
						// players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
						// players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Firework";

						// players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [2];
						// players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [3];
						// players [0].GetComponent<MoveHero> ().gun.core = commandsCores [2];

						// players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "clowns";

					} else if (command.Contains ("knights")) {
						gunCases.SetValue(PhotonNetwork.Instantiate (commandsGuns [4].name, new Vector2 (-14.32f, -12.21f), Quaternion.identity, 0).GetComponent<ShootCore> ().name, 0);
						// players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [4], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
						
						// players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
						// players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Spear";

						// players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [4];
						// players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [5];

						// players [0].GetComponent<MoveHero> ().gun.core = commandsCores [4];

						// players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "knights";

					} else if (command.Contains ("rods")) {
						gunCases.SetValue(PhotonNetwork.Instantiate (commandsGuns [6].name, new Vector2 (-14.32f, -12.21f), Quaternion.identity, 0).GetComponent<ShootCore> ().name, 0);
						// players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [6], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
						
						// players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
						// players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Rod";

						// players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [6];
						// players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [7];
						// players [0].GetComponent<MoveHero> ().gun.core = commandsCores [6];

						// players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "rods";

					}
					players [0].GetComponent<MoveHero> ().gun.pointOfCore = GameObject.FindGameObjectsWithTag ("ControlPoint") [0];
					players [0].GetComponent<MoveHero> ().gun.strike = 20;
				} else if (commandIndex == 2) {
					if (command.Contains ("pirates")) {
						gunCases.SetValue(PhotonNetwork.Instantiate (commandsGuns [1].name, new Vector2 (15.19f, -12.05f), Quaternion.identity, 0).GetComponent<ShootCore> ().name, 1);
						// players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [1], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
						
						// players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
						// players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Gun";

						// players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [0];
						// players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [1];


						// players [2].GetComponent<MoveHero> ().gun.core = commandsCores [0];

						// players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "pirates";

					} else if (command.Contains ("clowns")) {
						gunCases.SetValue(PhotonNetwork.Instantiate (commandsGuns [3].name, new Vector2 (15.19f, -12.05f), Quaternion.identity, 0).GetComponent<ShootCore> ().name, 1);
						// players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [3], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
						
						// players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
						// players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Firework";

						// players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [2];
						// players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [3];
						// players [2].GetComponent<MoveHero> ().gun.core = commandsCores [3];
						
						// players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "clowns";

					} else if (command.Contains ("knights")) {
						gunCases.SetValue(PhotonNetwork.Instantiate (commandsGuns [5].name, new Vector2 (15.19f, -12.05f), Quaternion.identity, 0).GetComponent<ShootCore> ().name, 1);
						// players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [5], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
						
						// players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
						// players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Spear";

						// players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [4];
						// players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [5];


						// players [2].GetComponent<MoveHero> ().gun.core = commandsCores [5];

						// players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "knights";

					} else if (command.Contains ("rods")) {
						gunCases.SetValue(PhotonNetwork.Instantiate (commandsGuns [7].name, new Vector2 (15.19f, -12.05f), Quaternion.identity, 0).GetComponent<ShootCore> ().name, 1);
						// players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [7], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
						
						// players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
						// players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Rod";

						// players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [6];
						// players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [7];
						// players [2].GetComponent<MoveHero> ().gun.core = commandsCores [7];

						// players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "rods";

					}

					players [2].tag = "PlayerEnemy";
					players [3].tag = "PlayerEnemy";

					players [2].GetComponent<MoveHero> ().gun.pointOfCore = GameObject.FindGameObjectsWithTag ("ControlPoint") [1];
					players [2].GetComponent<MoveHero> ().gun.strike = -20;
				}
				if(commandIndex >= 2){
					break;
				}
			}

			/*
			print("PhotonNetwork.playerList: " + PhotonNetwork.playerList.Length.ToString());
			photonView.RPC("SetDefaultSettings", PhotonTargets.All, shuffledCommands);
			*/
			PhotonNetwork.RaiseEvent(33, new object[] { shuffledCommands, gunCases }, true, new RaiseEventOptions { Receivers = ReceiverGroup.All });
			/*
			PhotonNetwork.RaiseEvent(0, new object[] { shuffledCommands }, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable);

			photonView.RPC("SetDefaultSettings", PhotonNetwork.playerList[0], shuffledCommands);
			photonView.RPC("SetDefaultSettings", PhotonNetwork.playerList[1], shuffledCommands);
			photonView.RPC("SetDefaultSettings", PhotonNetwork.playerList[2], shuffledCommands);
			photonView.RPC("SetDefaultSettings", PhotonNetwork.playerList[3], shuffledCommands);

			GameObject coreNewFirst = Instantiate (players [0].GetComponent<MoveHero> ().gun.core, players[0].GetComponent<MoveHero> ().gun.pointOfCore), Quaternion.identity);
			coreNewFirst.GetComponent<CoreAttack> ().gameSettings = GameObject.FindWithTag("GameSettings").GetComponent<SetBackground>();
			coreNewFirst.GetComponent<CoreAttack> ().insideGun = false;

			GameObject coreNewSecond = Instantiate (players [2].GetComponent<MoveHero> ().gun.core, players[2].GetComponent<MoveHero> ().gun.pointOfCore), Quaternion.identity);
			coreNewSecond.GetComponent<CoreAttack> ().gameSettings = GameObject.FindWithTag("GameSettings").GetComponent<SetBackground>();
			coreNewSecond.GetComponent<CoreAttack> ().insideGun = false;

			players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().AddCore();
			*/
		}	
	}

	public void OnEvent(byte eventCode, object content, int senderId) {
		if (eventCode == 33) {
			try {
				object[] data = (object[])content;
				
				string[] receivedShuffledCommands = (string[])data[0];
				string[] receivedGunsCases = (string[])data[1];
				
				string receivedFirstGunCase = receivedGunsCases[0];
				string receivedSecondGunCase = receivedGunsCases[1];

				// List<string> receivedShuffledCommands = (List<string>)data[0];

				Debug.Log("пришло событие SetBackground: " + System.String.Join(" ", receivedShuffledCommands));

				int levelNumber = 0;
				levelList [levelNumber].SetActive (true);
				
				int commandIndex = 0;
				shuffledCommands = receivedShuffledCommands;
				foreach(var command in receivedShuffledCommands){
					commandIndex++;
					if (commandIndex == 1) {
						players [0].tag = "Player";
						players [1].tag = "Player";
						if (command.Contains ("pirates")) {
							players [0].GetComponent<MoveHero> ().gun = GameObject.Find(receivedFirstGunCase).GetComponent<ShootCore>();
							// players [0].GetComponent<MoveHero> ().gun = PhotonNetwork.Instantiate (commandsGuns [0].name, new Vector2 (-14.32f, -12.21f), Quaternion.identity, 0).GetComponent<ShootCore> ();
							// players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [0], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
							players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
							players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Gun";
							players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [0];
							players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [1];


							players [0].GetComponent<MoveHero> ().gun.core = commandsCores [0];

							players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "pirates";
							
							players [0].GetComponent<MoveHero> ().gun.core.GetComponent<CoreAttack> ().gameSettings = GameObject.FindWithTag("GameSettings").GetComponent<SetBackground>();
						
							players [0].GetComponent<MoveHero> ().photonViewOfGun = players [0].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [0].GetComponent<MoveHero> ().photonViewOfDuloGun = players [0].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();
							players [1].GetComponent<MoveHero> ().photonViewOfGun = players [0].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [1].GetComponent<MoveHero> ().photonViewOfDuloGun = players [0].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();
						
						} else if (command.Contains ("clowns")) {
							players [0].GetComponent<MoveHero> ().gun = GameObject.Find(receivedFirstGunCase).GetComponent<ShootCore>();
							// players [0].GetComponent<MoveHero> ().gun = PhotonNetwork.Instantiate (commandsGuns [2].name, new Vector2 (-14.32f, -12.21f), Quaternion.identity, 0).GetComponent<ShootCore> ();
							// players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [2], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
							players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
							players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Firework";
							players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [2];
							players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [3];
							players [0].GetComponent<MoveHero> ().gun.core = commandsCores [2];

							players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "clowns";
						
							players [0].GetComponent<MoveHero> ().gun.core.GetComponent<CoreAttack> ().gameSettings = GameObject.FindWithTag("GameSettings").GetComponent<SetBackground>();

							players [0].GetComponent<MoveHero> ().photonViewOfGun = players [0].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [0].GetComponent<MoveHero> ().photonViewOfDuloGun = players [0].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();
							players [1].GetComponent<MoveHero> ().photonViewOfGun = players [0].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [1].GetComponent<MoveHero> ().photonViewOfDuloGun = players [0].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();

						} else if (command.Contains ("knights")) {
							players [0].GetComponent<MoveHero> ().gun = GameObject.Find(receivedFirstGunCase).GetComponent<ShootCore>();
							// players [0].GetComponent<MoveHero> ().gun = PhotonNetwork.Instantiate (commandsGuns [4].name, new Vector2 (-14.32f, -12.21f), Quaternion.identity, 0).GetComponent<ShootCore> ();
							// players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [4], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
							players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
							players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Spear";
							players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [4];
							players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [5];

							players [0].GetComponent<MoveHero> ().gun.core = commandsCores [4];

							players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "knights";

							players [0].GetComponent<MoveHero> ().gun.core.GetComponent<CoreAttack> ().gameSettings = GameObject.FindWithTag("GameSettings").GetComponent<SetBackground>();

							players [0].GetComponent<MoveHero> ().photonViewOfGun = players [0].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [0].GetComponent<MoveHero> ().photonViewOfDuloGun = players [0].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();
							players [1].GetComponent<MoveHero> ().photonViewOfGun = players [0].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [1].GetComponent<MoveHero> ().photonViewOfDuloGun = players [0].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();

						} else if (command.Contains ("rods")) {
							players [0].GetComponent<MoveHero> ().gun = GameObject.Find(receivedFirstGunCase).GetComponent<ShootCore>();
							// players [0].GetComponent<MoveHero> ().gun = PhotonNetwork.Instantiate (commandsGuns [6].name, new Vector2 (-14.32f, -12.21f), Quaternion.identity, 0).GetComponent<ShootCore> ();
							// players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [6], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
							players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
							players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Rod";
							players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [6];
							players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [7];
							players [0].GetComponent<MoveHero> ().gun.core = commandsCores [6];

							players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "rods";

							players [0].GetComponent<MoveHero> ().gun.core.GetComponent<CoreAttack> ().gameSettings = GameObject.FindWithTag("GameSettings").GetComponent<SetBackground>();

							players [0].GetComponent<MoveHero> ().photonViewOfGun = players [0].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [0].GetComponent<MoveHero> ().photonViewOfDuloGun = players [0].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();
							players [1].GetComponent<MoveHero> ().photonViewOfGun = players [0].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [1].GetComponent<MoveHero> ().photonViewOfDuloGun = players [0].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();

						}
						players [0].GetComponent<MoveHero> ().gun.pointOfCore = GameObject.FindGameObjectsWithTag ("ControlPoint") [0];
						players [0].GetComponent<MoveHero> ().gun.strike = 20;
					} else if (commandIndex == 2) {
						if (command.Contains ("pirates")) {
							players [2].GetComponent<MoveHero> ().gun = GameObject.Find(receivedSecondGunCase).GetComponent<ShootCore>();
							// players [2].GetComponent<MoveHero> ().gun = PhotonNetwork.Instantiate (commandsGuns [1].name, new Vector2 (15.19f, -12.05f), Quaternion.identity, 0).GetComponent<ShootCore> ();
							// players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [1], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
							players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
							players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Gun";
							players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [0];
							players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [1];


							players [2].GetComponent<MoveHero> ().gun.core = commandsCores [0];

							players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "pirates";

							players [2].GetComponent<MoveHero> ().gun.core.GetComponent<CoreAttack> ().gameSettings = GameObject.FindWithTag("GameSettings").GetComponent<SetBackground>();

							players [2].GetComponent<MoveHero> ().photonViewOfGun = players [2].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [2].GetComponent<MoveHero> ().photonViewOfDuloGun = players [2].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();
							players [3].GetComponent<MoveHero> ().photonViewOfGun = players [2].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [3].GetComponent<MoveHero> ().photonViewOfDuloGun = players [2].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();

						} else if (command.Contains ("clowns")) {
							players [2].GetComponent<MoveHero> ().gun = GameObject.Find(receivedSecondGunCase).GetComponent<ShootCore>();
							// players [2].GetComponent<MoveHero> ().gun = PhotonNetwork.Instantiate (commandsGuns [3].name, new Vector2 (15.19f, -12.05f), Quaternion.identity, 0).GetComponent<ShootCore> ();
							// players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [3], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
							players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
							players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Firework";
						
							players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [2];
							players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [3];
							players [2].GetComponent<MoveHero> ().gun.core = commandsCores [3];
							
							players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "clowns";

							players [2].GetComponent<MoveHero> ().gun.core.GetComponent<CoreAttack> ().gameSettings = GameObject.FindWithTag("GameSettings").GetComponent<SetBackground>();

							players [2].GetComponent<MoveHero> ().photonViewOfGun = players [2].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [2].GetComponent<MoveHero> ().photonViewOfDuloGun = players [2].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();
							players [3].GetComponent<MoveHero> ().photonViewOfGun = players [2].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [3].GetComponent<MoveHero> ().photonViewOfDuloGun = players [2].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();

						} else if (command.Contains ("knights")) {
							players [2].GetComponent<MoveHero> ().gun = GameObject.Find(receivedSecondGunCase).GetComponent<ShootCore>();
							// players [2].GetComponent<MoveHero> ().gun = PhotonNetwork.Instantiate (commandsGuns [5].name, new Vector2 (15.19f, -12.05f), Quaternion.identity, 0).GetComponent<ShootCore> ();
							// players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [5], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
							players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
							players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Spear";
							players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [4];
							players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [5];


							players [2].GetComponent<MoveHero> ().gun.core = commandsCores [5];

							players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "knights";

							players [2].GetComponent<MoveHero> ().gun.core.GetComponent<CoreAttack> ().gameSettings = GameObject.FindWithTag("GameSettings").GetComponent<SetBackground>();

							players [2].GetComponent<MoveHero> ().photonViewOfGun = players [2].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [2].GetComponent<MoveHero> ().photonViewOfDuloGun = players [2].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();
							players [3].GetComponent<MoveHero> ().photonViewOfGun = players [2].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [3].GetComponent<MoveHero> ().photonViewOfDuloGun = players [2].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();

						} else if (command.Contains ("rods")) {
							players [2].GetComponent<MoveHero> ().gun = GameObject.Find(receivedSecondGunCase).GetComponent<ShootCore>();
							// players [2].GetComponent<MoveHero> ().gun = PhotonNetwork.Instantiate (commandsGuns [7].name, new Vector2 (15.19f, -12.05f), Quaternion.identity, 0).GetComponent<ShootCore> ();
							// players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [7], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
							players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
							players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Rod";
							players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [6];
							players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [7];
							players [2].GetComponent<MoveHero> ().gun.core = commandsCores [7];

							players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().command = "rods";

							players [2].GetComponent<MoveHero> ().gun.core.GetComponent<CoreAttack> ().gameSettings = GameObject.FindWithTag("GameSettings").GetComponent<SetBackground>();

							players [2].GetComponent<MoveHero> ().photonViewOfGun = players [2].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [2].GetComponent<MoveHero> ().photonViewOfDuloGun = players [2].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();
							players [3].GetComponent<MoveHero> ().photonViewOfGun = players [2].GetComponent<MoveHero> ().gun.gameObject.GetComponent<PhotonView>();
							players [3].GetComponent<MoveHero> ().photonViewOfDuloGun = players [2].GetComponent<MoveHero> ().gun.gameObject.transform.GetChild(0).gameObject.GetComponent<PhotonView>();
						}

						players [2].tag = "PlayerEnemy";
						players [3].tag = "PlayerEnemy";

						players [2].GetComponent<MoveHero> ().gun.pointOfCore = GameObject.FindGameObjectsWithTag ("ControlPoint") [1];
						players [2].GetComponent<MoveHero> ().gun.strike = -20;
					}
					if(commandIndex >= 2){
						break;
					}
				}
				if(PhotonNetwork.isMasterClient){
					players [0].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().AddCore();
					players [2].GetComponent<MoveHero> ().gun.GetComponent<ShootCore>().AddCore();
				}
			} catch (System.InvalidCastException e) {
				Debug.Log("InvalidCastException поймал 1");
			}
		}
	}

	void Sands(){
		Instantiate (sand, new Vector2(UnityEngine.Random.Range(-17.5f, 17.6f), sand.transform.position.y), Quaternion.identity);
	}

}
