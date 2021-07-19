using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SetBackground : MonoBehaviour {

	public GameObject sand;
	public List<GameObject> levelList;
	public List<string> commands;
	public List<GameObject> players;
	public List<Sprite> commandsSprites;
	public List<GameObject> commandsCores;
	public List<GameObject> commandsGuns;

	void Start () {
		//int levelNumber = Random.Range (0, levelList.Count);
		//levelList [levelNumber].SetActive (true);

		//закоментировал выше и вставил ниже так потому что персонажам и оружиям применяется сила не на уровне 0
		int levelNumber = 0;
		levelList [levelNumber].SetActive (true);

		if(levelNumber == 0){
			// позже надо раскоментировать чтобы включить зыбучие пески
			//InvokeRepeating("Sands", 2f, 1f);	
		} else if(levelNumber == 1){

		} else if(levelNumber == 2){

		}

		//логика распределения команд
		commands = new List<string>(){
			"pirates",
			"clowns",
			"knights",
			"rods"
		};
		List<string> shuffledCommands = commands.OrderBy(x => Random.value ).ToList();
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


				} else if (command.Contains ("clowns")) {
					players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [2], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
					players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
					players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Firework";

					players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [2];
					players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [3];
					players [0].GetComponent<MoveHero> ().gun.core = commandsCores [2];

				} else if (command.Contains ("knights")) {
					players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [4], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
					players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
					players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Spear";

					players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [4];
					players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [5];

					players [0].GetComponent<MoveHero> ().gun.core = commandsCores [4];

				} else if (command.Contains ("rods")) {
					players [0].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [6], new Vector2 (-14.32f, -12.21f), Quaternion.identity).GetComponent<ShootCore> ();
					players [1].GetComponent<MoveHero> ().gun = players [0].GetComponent<MoveHero> ().gun;
					players [0].GetComponent<MoveHero> ().gun.gameObject.name = "Rod";

					players [0].GetComponent<SpriteRenderer> ().sprite = commandsSprites [6];
					players [1].GetComponent<SpriteRenderer> ().sprite = commandsSprites [7];
					players [0].GetComponent<MoveHero> ().gun.core = commandsCores [6];

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

				} else if (command.Contains ("clowns")) {
					players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [3], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
					players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
					players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Firework";

					players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [2];
					players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [3];
					players [2].GetComponent<MoveHero> ().gun.core = commandsCores [3];

				} else if (command.Contains ("knights")) {
					players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [5], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
					players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
					players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Spear";

					players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [4];
					players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [5];


					players [2].GetComponent<MoveHero> ().gun.core = commandsCores [5];

				} else if (command.Contains ("rods")) {
					players [2].GetComponent<MoveHero> ().gun = Instantiate (commandsGuns [7], new Vector2 (15.19f, -12.05f), Quaternion.identity).GetComponent<ShootCore> ();
					players [3].GetComponent<MoveHero> ().gun = players [2].GetComponent<MoveHero> ().gun;
					players [2].GetComponent<MoveHero> ().gun.gameObject.name = "Rod";

					players [2].GetComponent<SpriteRenderer> ().sprite = commandsSprites [6];
					players [3].GetComponent<SpriteRenderer> ().sprite = commandsSprites [7];
					players [2].GetComponent<MoveHero> ().gun.core = commandsCores [7];

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
	void Sands(){
		Instantiate (sand, new Vector2(Random.Range(-17.5f, 17.6f), sand.transform.position.y), Quaternion.identity);

	}

}
