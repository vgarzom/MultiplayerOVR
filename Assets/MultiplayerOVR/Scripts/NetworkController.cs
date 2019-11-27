using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public string _room = "multiplayerOVR2";

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 20;

    TypedLobby lobby = new TypedLobby("MyLobby", LobbyType.Default);

    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void startConnection() {
        if (PhotonNetwork.IsConnected)
        {
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
        PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, lobby);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Just connected to master!");
        PhotonNetwork.JoinLobby(lobby);
    }

    override public void OnJoinedLobby()
    {
        Debug.Log("Just Joined to Lobby!");

        JoinRoom();
    }

    override public void OnJoinedRoom()
    {
        Debug.Log("just New user joined to room...");
        GameObject player = PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);

        //GameObject shadow = PhotonNetwork.Instantiate("ShadowPlayer", Vector3.zero, Quaternion.identity, 0);
        //shadow.GetComponent<ShadowPlayer>().SetId(PhotonNetwork.CurrentRoom.PlayerCount.ToString());
        //player.GetComponent<NetworkedPlayer>().SetShadow(shadow.GetComponent<ShadowPlayer>());

        Debug.Log("Joined to room");
    }
}
