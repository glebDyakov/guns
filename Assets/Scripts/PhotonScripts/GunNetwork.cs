using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon;

public class GunNetwork : PunBehaviour {
    
    public GameObject buttonLoadArena;
    public UnityEngine.UI.Button buttonJoinedArena;
    public UnityEngine.UI.Text connectionStatus;
    public UnityEngine.UI.Text playerStatus;
    public UnityEngine.UI.InputField roomNameField;
    public string gameVersion = "1";
    string playerName;
    string roomName;
    public GameObject playerRoom;
    public GameObject rooms;
    public GameObject players;
    public UnityEngine.UI.Text playersCount;
    public GameObject emptyPrefab;
    public Coroutine checkLobby;
    TypedLobby typedLobby;
    // public string ROOM_CODE = "C0";

    void Awake() {
        PhotonNetwork.automaticallySyncScene = true;
    }

    public IEnumerator CheckLobby() {
        while(true){
            if(PhotonNetwork.insideLobby){
                print("зашёл");
                OnReceivedRoomListUpdate();
                // PhotonNetwork.GetCustomRoomList(new TypedLobby("lobbyname", LobbyType.Default), ROOM_CODE + " = '" + "strroomcode" + "'");
                StopCoroutine(checkLobby);
                yield return null;
            } else if(!PhotonNetwork.insideLobby) {
                print("не зашёл");
                PhotonNetwork.JoinLobby	();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public override void OnConnectedToMaster(){
        checkLobby = StartCoroutine(CheckLobby());
        PhotonNetwork.JoinLobby	();
        print("PhotonNetwork.insideLobby: " + PhotonNetwork.insideLobby);
    }

    void Start() {
        
        PlayerPrefs.DeleteAll ();

        ConnectToPhoton();

        // PhotonNetwork.JoinLobby	();
        // print("PhotonNetwork.insideLobby: " + PhotonNetwork.insideLobby);

        // typedLobby = new TypedLobby(roomName, LobbyType.Default);
        // PhotonNetwork.JoinLobby	(typedLobby);
        
        // checkLobby = StartCoroutine(CheckLobby());

        
        // if(PhotonNetwork.GetRoomList ().Length >= 1){
        //     for(int roomIndex = 0; roomIndex < PhotonNetwork.countOfRooms; roomIndex++){
        //         GameObject roomInst = Instantiate(emptyPrefab, new Vector2(0f, 0f), Quaternion.identity);
        //         roomInst.GetComponent<UnityEngine.UI.Text>().text = PhotonNetwork.GetRoomList()[roomIndex].Name + ": " + PhotonNetwork.GetRoomList()[roomIndex].PlayerCount.ToString() + "/4";
        //         roomInst.transform.parent = rooms.transform;
        //         roomInst.transform.localScale = new Vector2(1f, 1f);
        //         roomInst.GetComponent<RectTransform>().sizeDelta = new Vector2(487f, roomInst.GetComponent<RectTransform>().sizeDelta.y);
        //         print("roomName: " + PhotonNetwork.GetRoomList()[roomIndex].Name);
        //     }
        // } else if(PhotonNetwork.GetRoomList ().Length <= 0){
        //     GameObject roomInst = Instantiate(emptyPrefab, new Vector2(0f, 0f), Quaternion.identity);
        //     roomInst.GetComponent<UnityEngine.UI.Text>().text = "Список комнат пуст...";
        //     roomInst.transform.parent = rooms.transform;
        //     roomInst.transform.localScale = new Vector2(1f, 1f);
        // }

    }
    
    public void SetPlayerName (string name) {
        playerName = name;
    }

    public void SetRoomName (string name) {
        roomName = name;
    }

    void  ConnectToPhoton (){
        connectionStatus.text = "Подключение ...";
        PhotonNetwork.gameVersion = gameVersion; 
        PhotonNetwork.ConnectUsingSettings(gameVersion);
    }

    public void JoinRoom() {
        try {
            if (PhotonNetwork.connected){
                // SetPlayerName(new List<string>(){"Mike", "Cris", "Smith"}[Random.Range(0, 3)]);
                SetPlayerName(SystemInfo.deviceName);
                PhotonNetwork.player.NickName = playerName;
                Debug.Log("PhotonNetwork.IsConnected! | Trying to Create/Join Room " + roomNameField.text);
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.maxPlayers = 2;
                // roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { { ROOM_CODE, "strRoomCode" } };
                // roomOptions.CustomRoomPropertiesForLobby = new string[]{ ROOM_CODE };
                TypedLobby typedLobby = new TypedLobby(roomName, LobbyType.Default);
                // TypedLobby typedLobby = new TypedLobby("lobbyname", LobbyType.Default);
                SetRoomName(roomNameField.text);
                PhotonNetwork.CreateRoom(roomName, roomOptions, typedLobby);
            }
        } catch(System.Exception e) {
            Debug.Log(e);
        }
    }

    public void LoadArena() {
        if (PhotonNetwork.room.playerCount >= 2) {
            PhotonNetwork.LoadLevel("main");
        } else {
            playerStatus.text = "Minimum 4 Players required to Load Arena!";
            Debug.Log("Minimum 4 Players required to Load Arena!");
        }
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer) {
        playerRoom.SetActive(true);
        playersCount.text = PhotonNetwork.room.playerCount.ToString() + "/4";
        if(PhotonNetwork.room.playerCount >= 2){
            buttonJoinedArena.interactable = true;
        }
        for(int playerIndex = 0; playerIndex < players.transform.childCount; playerIndex++){
            Destroy(players.transform.GetChild(playerIndex).gameObject);
        }
        for(int playerIndex = 0; playerIndex < PhotonNetwork.room.playerCount; playerIndex++){
            GameObject playerInst = Instantiate(emptyPrefab, new Vector2(0f, 0f), Quaternion.identity);
            playerInst.GetComponent<UnityEngine.UI.Text>().text = PhotonNetwork.playerList[playerIndex].name;
            playerInst.transform.parent = players.transform;
            playerInst.transform.localScale = new Vector2(1f, 1f);
            playerInst.GetComponent<RectTransform>().sizeDelta = new Vector2(228f, playerInst.GetComponent<RectTransform>().sizeDelta.y);
        }
    }

    public override void OnPhotonPlayerDisconnected (PhotonPlayer otherPlayer){
        if(PhotonNetwork.room.playerCount >= 2){
            playersCount.GetComponent<UnityEngine.UI.Text>().text = PhotonNetwork.room.playerCount.ToString() + "/4";
        } else if(PhotonNetwork.room.playerCount <= 1){
            playersCount.GetComponent<UnityEngine.UI.Text>().text = "1/4";
        }
        for(int playerIndex = 0; playerIndex < players.transform.childCount; playerIndex++){
            if(players.transform.GetChild(playerIndex).gameObject.GetComponent<UnityEngine.UI.Text>().text.Contains(otherPlayer.name)){
                Destroy(players.transform.GetChild(playerIndex).gameObject);
                break;
            }
        }
    }

    public override void OnJoinedRoom (){
        Debug.Log("Вы присоединились к комнате и неважно создатель комнаты вы или нет");
        playerRoom.SetActive(true);
        playersCount.text = PhotonNetwork.room.playerCount.ToString() + "/4";
        if(PhotonNetwork.room.playerCount >= 2){
            buttonJoinedArena.interactable = true;
        }
        
        for(int playerIndex = 0; playerIndex < PhotonNetwork.room.playerCount; playerIndex++){
            GameObject playerInst = Instantiate(emptyPrefab, new Vector2(0f, 0f), Quaternion.identity);
            playerInst.GetComponent<UnityEngine.UI.Text>().text = PhotonNetwork.playerList[playerIndex].name;
            playerInst.transform.parent = players.transform;
            playerInst.transform.localScale = new Vector2(1f, 1f);
            playerInst.GetComponent<RectTransform>().sizeDelta = new Vector2(228f, playerInst.GetComponent<RectTransform>().sizeDelta.y);
        }

        PlayerPrefs.SetInt("PlayerIndex", PhotonNetwork.room.playerCount);

    }

    public override void OnCreatedRoom (){
        Debug.Log("Вы создали комнату");
        // playerRoom.SetActive(true);
        // playersCount.text = PhotonNetwork.room.playerCount.ToString() + "/4";
        // if(PhotonNetwork.room.playerCount >= 2){
        //     buttonJoinedArena.interactable = true;
        // }
        
        // for(int playerIndex = 0; playerIndex < PhotonNetwork.room.playerCount; playerIndex++){
        //     GameObject playerInst = Instantiate(emptyPrefab, new Vector2(0f, 0f), Quaternion.identity);
        //     playerInst.GetComponent<UnityEngine.UI.Text>().text = PhotonNetwork.playerList[playerIndex].name;
        //     playerInst.transform.parent = players.transform;
        //     playerInst.transform.localScale = new Vector2(1f, 1f);
        //     playerInst.GetComponent<RectTransform>().sizeDelta = new Vector2(228f, playerInst.GetComponent<RectTransform>().sizeDelta.y);
        // }
    }
    
    public override void OnLeftRoom (){
        Debug.Log("Вы вышли из комнаты");
        for(int playerIndex = 0; playerIndex < players.transform.childCount; playerIndex++){
            Destroy(players.transform.GetChild(playerIndex).gameObject);
        }
        playersCount.GetComponent<UnityEngine.UI.Text>().text = "1/4";
        PhotonNetwork.JoinLobby	();
        checkLobby = StartCoroutine(CheckLobby());
    }

    public override void OnPhotonCreateRoomFailed (object[] messages){
        Debug.Log("Нельзя создать комнату несколько раз");
    }

    public override void OnPhotonJoinRoomFailed (object[] messages){
        Debug.Log("Нельзя присоединиться в комнату несколько раз 1 игроку подряд");
    }

    public void ExitFromRoom(){
        PhotonNetwork.LeaveRoom();
        playerRoom.SetActive(false);
    }

    public override void OnReceivedRoomListUpdate() {
        Debug.Log("получаем новые комнаты: " + PhotonNetwork.GetRoomList ().Length.ToString());
        for(int roomIndex = 0; roomIndex < rooms.transform.childCount; roomIndex++){
            Destroy(rooms.transform.GetChild(roomIndex).gameObject);
        }
        if(PhotonNetwork.GetRoomList ().Length >= 1){
            for(int roomIndex = 0; roomIndex < PhotonNetwork.countOfRooms; roomIndex++){
                GameObject roomInst = Instantiate(emptyPrefab, new Vector2(0f, 0f), Quaternion.identity);
                
                // roomInst.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
                // roomInst.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                // roomInst.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                
                roomInst.GetComponent<UnityEngine.UI.Button>().enabled = true;
                roomInst.GetComponent<UnityEngine.UI.Text>().text = PhotonNetwork.GetRoomList()[roomIndex].Name + ": " + PhotonNetwork.GetRoomList()[roomIndex].PlayerCount.ToString() + "/4";
                roomInst.transform.parent = rooms.transform;
                roomInst.transform.localScale = new Vector2(1f, 1f);
                
                // roomInst.GetComponent<RectTransform>().sizeDelta = new Vector2(487f, roomInst.GetComponent<RectTransform>().sizeDelta.y);
                roomInst.GetComponent<RectTransform>().sizeDelta = new Vector2(rooms.GetComponent<RectTransform>().sizeDelta.x / 2, roomInst.GetComponent<RectTransform>().sizeDelta.y);
                
                print("roomName: " + PhotonNetwork.GetRoomList()[roomIndex].Name);
            }
        } else if(PhotonNetwork.GetRoomList ().Length <= 0){
            GameObject roomInst = Instantiate(emptyPrefab, new Vector2(0f, 0f), Quaternion.identity);
            
            // roomInst.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
            // roomInst.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            // roomInst.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

            roomInst.GetComponent<UnityEngine.UI.Text>().text = "Список комнат пуст...";
            roomInst.transform.parent = rooms.transform;
            
            roomInst.GetComponent<RectTransform>().sizeDelta = new Vector2(rooms.GetComponent<RectTransform>().sizeDelta.x / 2, roomInst.GetComponent<RectTransform>().sizeDelta.y);
            
            roomInst.transform.localScale = new Vector2(1f, 1f);
        }
    } 

    // public override void OnJoinedRoom () {
    //     ​if (PhotonNetwork.isMasterClient) ​{
    //         ​buttonLoadArena.SetActive(true);
    //         ​buttonJoinRoom.SetActive(false);
    //         ​playerStatus.text = "You are Lobby Leader";
    //     ​} else {
    //         ​playerStatus.text = "Connected to Lobby";
    //     ​}
    // }

    // public override void OnConnected() {
    //     base.OnConnected();
    //     connectionStatus.text = "Connected to Photon!";
    //     connectionStatus.color = Color.green;
    //     roomJoinUI.SetActive(true);
    //     buttonLoadArena.SetActive(false);
    // }

    // public ​override void OnDisconnected(DisconnectCause cause) {
    //     ​isConnecting = false;
    //     ​controlPanel.SetActive(true);
    //     ​Debug.LogError("Disconnected. Please check your Internet connection.");
    // }

}
