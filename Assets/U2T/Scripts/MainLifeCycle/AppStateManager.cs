using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace U2T.Foundation
{
    public class AppStateManager
    {
        public delegate void AppStateDelegate();
        public AppStateDelegate OnStateChange = null;

        public enum GameState
        {
            PHOTON_CONNECTING,
            PHOTON_CONNECTED,
            FETCHING_DATA,
            REGISTER,
            LOGING_IN,
            LOGGED_IN,
            GAMEPLAY_START
        }

        public GameState gameState;

        public void ChangeAppState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.PHOTON_CONNECTING:
                    if (PhotonNetwork.IsConnected)
                    {
                        ChangeAppState(GameState.PHOTON_CONNECTED);
                    }
                    break;
                case GameState.PHOTON_CONNECTED:
                    ChangeAppState(GameState.FETCHING_DATA);
                    break;
                case GameState.FETCHING_DATA:
                    BackendManager db = new BackendManager();
                    db.Initialize();
                    break;
                case GameState.REGISTER:
                    break;
                case GameState.LOGING_IN:
                    break;
                case GameState.LOGGED_IN:
                    break;
                case GameState.GAMEPLAY_START:
                    break;
                default:
                    break;
            }
        }
    }
}
