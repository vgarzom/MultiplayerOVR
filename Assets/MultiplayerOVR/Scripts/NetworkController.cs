using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{
    public string _room = "multiplayerOVR2";
    public GameObject infoPanel;
    public Text infoText;

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 20;

    TypedLobby lobby = new TypedLobby("MyLobby", LobbyType.Default);

    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void startConnection() {
        infoText.text = "Iniciando conexión al servidor";
        if (PhotonNetwork.IsConnected)
        {
            JoinRoom();
        }
        else
        {
            Debug.Log("Trying to connect to photon server...");
            PhotonNetwork.GameVersion = "2.0";
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
        infoText.text = "Conexión al servidor completa. Conectando al lobby...";
        PhotonNetwork.JoinLobby(lobby);
    }

    override public void OnJoinedLobby()
    {
        Debug.Log("Just Joined to Lobby!");
        infoText.text = "Conectando al salón";

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

        infoText.text = "Conexión completa";
        StartCoroutine(HidePanel());
    }

    IEnumerator HidePanel()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        infoPanel.SetActive(false);
    }
}
