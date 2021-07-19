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

	void Start(){
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

	}

	void OnCollisionEnter2D (Collision2D other) {

		bool isGun = other.gameObject.name.Contains ("Gun") || other.gameObject.name == "Firework" || other.gameObject.name.Contains ("Rod") || other.gameObject.name.Contains ("Spear");
		
		if (other.gameObject.tag.Contains ("Player")) {
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
		} else if (isGun && !insideGun) {
			if(!other.gameObject.name.Contains("Rod")){
				other.gameObject.GetComponent<ShootCore> ().bullets.Add ("newbullet");
				Destroy (gameObject, 0.5f);
			} else if(other.gameObject.name.Contains("Rod") && other.gameObject.GetComponent<ShootCore> ().bullets.Count <= 0){
				other.gameObject.GetComponent<ShootCore> ().bullets.Add ("newbullet");
				Destroy (gameObject, 0.5f);
			}
		} else if ((other.gameObject.tag.Contains ("Platform") || other.gameObject.name.Contains ("Bounds")) && insideGun) {
			if (!other.gameObject.name.Contains ("Rod")) {
				gun.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
			}
			Destroy (gameObject);
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
		Application.LoadLevel (Application.loadedLevel);
	}

	public void OnBecameInvisible(){
		Destroy (gameObject);
	}

}
