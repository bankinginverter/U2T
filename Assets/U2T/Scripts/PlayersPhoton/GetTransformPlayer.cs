using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GetTransformPlayer : MonoBehaviour
{
    LobbyPhotonManager lobbyPhotonManager;
    PlayerTransform playerTransform;

    PhotonView view;

    public delegate void GetTransformDelegate();
    public GetTransformDelegate OnGetTransform = null;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        playerTransform = GameObject.Find("PlayerLocal").GetComponent<PlayerTransform>();
    }


    private void Update()
    {
        if (view.IsMine)
        {
            this.transform.position = playerTransform.GetTransform();
        }
    }
}
