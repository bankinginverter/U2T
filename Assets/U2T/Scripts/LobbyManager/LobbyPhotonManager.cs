using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using U2T.Foundation;

public class LobbyPhotonManager : MonoBehaviourPunCallbacks
{
    public static LobbyPhotonManager Instance;

    private RoomOptions _roomOptions;
    private bool _isPhotonConnected = false;

    private void Awake()
    {
        Instance = this;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void JoinOrCreateRoom(string roomName)
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, _roomOptions, TypedLobby.Default);
    }

    public override void OnConnected()
    {
        Debug.Log("Isconnected");
        _isPhotonConnected = true;
        AppStateManager.Instance.ChangeAppState(Enumulator.GameState.PHOTON_CONNECTED);
        UIManagers.instance.DisableUIPopUp("PhotonConnectingPopup");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("PhotonGamePlay1");
    }

    public bool IsPhotonConnected()
    {
        return _isPhotonConnected;
    }

}
