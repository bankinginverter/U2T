using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

namespace U2T.Foundation
{
    public class AppStateManager : MonoBehaviour
    {
        //BackendManager db;
        Save save;
        //BsonDocument dbCompare;

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
            LOG_OUT,
            SELECT_CHARACTER,
            GAMEPLAY_INIT,
            GAMEPLAY_START
        }

        public GameState gameState;

        private void Awake()
        {
            //db = new BackendManager();
            save = new Save();
        }

        private void Start()
        {
            OnStateChange?.Invoke();
        }

        public void ChangeAppState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.PHOTON_CONNECTING:
                    if (PhotonNetwork.IsConnected)
                    {
                        Debug.Log("AppState : GameState.PHOTON_CONNECTING");
                        ChangeAppState(GameState.PHOTON_CONNECTED);
                    }
                    break;
                case GameState.PHOTON_CONNECTED:
                    Debug.Log("AppState : GameState.PHOTON_CONNECTED");
                    ChangeAppState(GameState.FETCHING_DATA);
                    break;
                case GameState.FETCHING_DATA:
                    Debug.Log("AppState : GameState.FETCHINGDATA");
                    //db.Initialize();
                    //dbCompare = db.FilterData("username", save.GetUserName());
                    //if (db.FilterData("username", save.GetUserName()) == null)
                    //{
                    //    Debug.Log("1");
                    //    ChangeAppState(GameState.LOGING_IN);
                    //}
                    //if ((save.GetUserName() == dbCompare.GetValue("username")) && (save.GetPassword() == dbCompare.GetValue("password")))
                    //{
                    //    Debug.Log("2");
                    //    ChangeAppState(GameState.SELECT_CHARACTER);
                    //}
                    ChangeAppState(GameState.SELECT_CHARACTER);
                    break;
                case GameState.REGISTER:
                    Debug.Log("AppState : GameState.REGISTER");
                    UIManagers.instance.EnbleUIPopUp("RegisterPopup");
                    GameObject.Find("RegisterPopup").GetComponent<Register>().OnGotoLogin += () =>
                    {
                        UIManagers.instance.DisableUIPopUp("RegisterPopup");
                        ChangeAppState(GameState.LOGING_IN);
                    };
                    GameObject.Find("RegisterPopup").GetComponent<Register>().OnRegisted += (username,password) =>
                    {
                        UIManagers.instance.EnbleUIPopUp("AuthenPopup");
                        GameObject.Find("RegisterPopup").GetComponent<SendEmail>().SendingGmail(username);
                        GameObject.Find("AuthenPopup").GetComponent<Authentication>().OnVerified += () =>
                        {
                            //db.AddData(username, password);
                            save.SaveUserName(username);
                            //save.SavePassword(db.EncodePasswordToHAS256(password));
                            UIManagers.instance.DisableUIPopUp("AuthenPopup");
                            UIManagers.instance.DisableUIPopUp("RegisterPopup");
                            ChangeAppState(GameState.LOGING_IN);
                        };
                    };
                    break;
                case GameState.LOGING_IN:
                    Debug.Log("AppState : GameState.LOGIN");
                    UIManagers.instance.EnbleUIPopUp("LoginPopup");

                    //if (save.GetUserName() == dbCompare.GetValue("username") && save.GetPassword() == dbCompare.GetValue("password"))
                    //{
                    //    ChangeAppState(GameState.LOGGED_IN);
                    //    UIManagers.instance.DisableUIPopUp("LoginPopup");
                    //}

                    GameObject.Find("LoginPopup").GetComponent<Login>().OnGotoRegister += () =>
                    {
                        UIManagers.instance.DisableUIPopUp("LoginPopup");
                        ChangeAppState(GameState.REGISTER);
                    };

                    GameObject.Find("LoginPopup").GetComponent<Login>().OnLoggedin += (username,password) =>
                    {
                        //if (username == dbCompare.GetValue("username") && db.EncodePasswordToHAS256(password) == dbCompare.GetValue("password"))
                        //{
                        //    UIManagers.instance.DisableUIPopUp("LoginPopup");
                        //    ChangeAppState(GameState.LOGGED_IN);
                        //}
                    };
                    break;
                case GameState.LOGGED_IN:
                    Debug.Log("AppState : GameState.LOGGED_IN");
                    break;
                case GameState.LOG_OUT:
                    save.SaveUserName("");
                    save.SavePassword("");
                    ChangeAppState(GameState.LOGING_IN);
                    break;
                case GameState.SELECT_CHARACTER:
                    Debug.Log("AppState : GameState.SELECT_CHARACTER");
                    UIManagers.instance.EnbleUIPopUp("SelectCharacterPopup");
                    GameObject.Find("SelectCharacterPopup").GetComponent<SelectCharactorScreen>().OnEnter += () =>
                    {
                        ChangeAppState(GameState.GAMEPLAY_INIT);
                        GameObject.Find("SceneManager").GetComponent<LobbyPhotonManager>().JoinOrCreateRoom("B");
                    };
                    break;
                case GameState.GAMEPLAY_INIT:
                    Debug.Log("AppState : GameState.GAME_INIT");
                    ChangeAppState(GameState.GAMEPLAY_START);
                    break;
                case GameState.GAMEPLAY_START:
                    Debug.Log("AppState : GameState.GAME_START");
                    break;
                default:
                    break;
            }
        }
    }
}
