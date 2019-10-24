using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{
    string _room = "multiplayerOVR";

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected) {
            JoinRoom();
        }
        else
        {
            Debug.Log("Trying to connect to photon server...");
            PhotonNetwork.GameVersion = "1.0";
            PhotonNetwork.ConnectUsingSettings();
        }

    }

    void JoinRoom() {
        RoomOptions roomOptions = new RoomOptions() { MaxPlayers = maxPlayersPerRoom };
        PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Just connected to master!");
        JoinRoom();
    }

    override public void OnJoinedLobby()
    {
        Debug.Log("Just Joined to Lobby!");
    }

    override public void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
        Debug.Log("Joined to room");
    }
}
