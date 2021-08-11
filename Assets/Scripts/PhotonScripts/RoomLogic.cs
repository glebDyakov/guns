using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomLogic : MonoBehaviour {

    string playerName;

    public void SetPlayerName (string name) {
        playerName = name;
    }

    void JoinToRoom(){
        if (PhotonNetwork.connected){
            SetPlayerName(SystemInfo.deviceName);
            PhotonNetwork.player.NickName = playerName;
            string roomName = GetComponent<Text>().text.Split(new string[] { ":" }, StringSplitOptions.None)[0];
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    void Start() {
        Button btn = GetComponent<Button>();
		if(btn.enabled){
            btn.onClick.AddListener(JoinToRoom);
        }
    }
}
