using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

[System.Serializable]
public class DefaultRoom
{
    public string Name;
    public int sceneIndex;
    public int maxPlayer;
}

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<DefaultRoom> defaultRooms;

    //public GameObject roomUI;
    //public GameObject loadText;

    public AudioClip JoinClip;
    public AudioClip LeaveClip;
    private AudioSource source;

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Server.");
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Connected to lobby");
        //roomUI.SetActive(true);
        //loadText.SetActive(false);
    }

    public void InitializeRoom(int defaultRoomIndex)
    {
        DefaultRoom roomSettings = defaultRooms[defaultRoomIndex];

        //LOAD SCENE
        PhotonNetwork.LoadLevel(roomSettings.sceneIndex);

        //CREATE ROOM
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.maxPlayer;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom(roomSettings.Name, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined a Room");
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the room");
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        base.OnPlayerEnteredRoom(newPlayer);

        if (this.JoinClip != null)
        {
            if (this.source == null) this.source = FindObjectOfType<AudioSource>();
            this.source.PlayOneShot(this.JoinClip);
        }
    }

    public void LeaveTheRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (this.LeaveClip != null)
        {
            if (this.source == null) this.source = FindObjectOfType<AudioSource>();
            this.source.PlayOneShot(this.LeaveClip);
        }
    }
}
