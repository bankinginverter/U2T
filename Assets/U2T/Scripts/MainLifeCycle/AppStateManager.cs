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
        public static AppStateManager Instance;
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
            Instance = this;
            save = new Save();
        }

        private void Start()
        {
            OnStateChange?.Invoke();
        }

        public void ChangeAppState(Enumulator.GameState gameState)
        {
            switch (gameState)
            {
                case Enumulator.GameState.PHOTON_CONNECTING:
                    Debug.Log("AppState : GameState.PHOTON_CONNECTING");
                    UIManagers.Instance.EnbleUIPopUp("PhotonConnectingPopup");
                    break;
                case Enumulator.GameState.PHOTON_CONNECTED:
                    Debug.Log("AppState : GameState.PHOTON_CONNECTED");
                    ChangeAppState(Enumulator.GameState.FETCHING_DATA);
                    break;
                case Enumulator.GameState.FETCHING_DATA:
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
                    ChangeAppState(Enumulator.GameState.SELECT_CHARACTER);
                    break;
                case Enumulator.GameState.REGISTER:
                    Debug.Log("AppState : GameState.REGISTER");
                    UIManagers.Instance.EnbleUIPopUp("RegisterPopup");
                    GameObject.Find("RegisterPopup").GetComponent<Register>().OnGotoLogin += () =>
                    {
                        UIManagers.Instance.DisableUIPopUp("RegisterPopup");
                        ChangeAppState(Enumulator.GameState.LOGING_IN);
                    };
                    GameObject.Find("RegisterPopup").GetComponent<Register>().OnRegisted += (username,password) =>
                    {
                        UIManagers.Instance.EnbleUIPopUp("AuthenPopup");
                        GameObject.Find("RegisterPopup").GetComponent<SendEmail>().SendingGmail(username);
                        GameObject.Find("AuthenPopup").GetComponent<Authentication>().OnVerified += () =>
                        {
                            //db.AddData(username, password);
                            save.SaveUserName(username);
                            //save.SavePassword(db.EncodePasswordToHAS256(password));
                            UIManagers.Instance.DisableUIPopUp("AuthenPopup");
                            UIManagers.Instance.DisableUIPopUp("RegisterPopup");
                            ChangeAppState(Enumulator.GameState.LOGING_IN);
                        };
                    };
                    break;
                case Enumulator.GameState.LOGING_IN:
                    Debug.Log("AppState : GameState.LOGIN");
                    UIManagers.Instance.EnbleUIPopUp("LoginPopup");

                    //if (save.GetUserName() == dbCompare.GetValue("username") && save.GetPassword() == dbCompare.GetValue("password"))
                    //{
                    //    ChangeAppState(GameState.LOGGED_IN);
                    //    UIManagers.instance.DisableUIPopUp("LoginPopup");
                    //}

                    GameObject.Find("LoginPopup").GetComponent<Login>().OnGotoRegister += () =>
                    {
                        UIManagers.Instance.DisableUIPopUp("LoginPopup");
                        ChangeAppState(Enumulator.GameState.REGISTER);
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
                case Enumulator.GameState.LOGGED_IN:
                    Debug.Log("AppState : GameState.LOGGED_IN");
                    break;
                case Enumulator.GameState.LOG_OUT:
                    save.SaveUserName("");
                    save.SavePassword("");
                    ChangeAppState(Enumulator.GameState.LOGING_IN);
                    break;
                case Enumulator.GameState.SELECT_CHARACTER:
                    Debug.Log("AppState : GameState.SELECT_CHARACTER");
                    UIManagers.Instance.EnbleUIPopUp("SelectCharacterPopup");
                    GameObject.Find("SelectCharacterPopup").GetComponent<SelectCharactorScreen>().OnEnter += () =>
                    {
                        ChangeAppState(Enumulator.GameState.GAMEPLAY_INIT);
                        GameObject.Find("SceneManager").GetComponent<LobbyPhotonManager>().JoinOrCreateRoom("B");
                    };
                    break;
                case Enumulator.GameState.GAMEPLAY_INIT:
                    Debug.Log("AppState : GameState.GAME_INIT");
                    break;
                case Enumulator.GameState.GAMEPLAY_START:
                    Debug.Log("AppState : GameState.GAME_START");
                    UIManagers.Instance.EnbleUIPopUp("GeneralMenuPopup");
                    GameObject.Find("GeneralMenuPopup").GetComponent<GeneralMenuPopup>().OnPassported += () =>
                    {
                        UIManagers.Instance.EnbleUIPopUp("PassportHistoryPopup");
                        if (save.GetCheckIn1() == "pass")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Reward1");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Disable1");
                        }
                        if (save.GetCheckIn2() == "pass")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Reward2");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Disable2");
                        }
                        if (save.GetCheckIn3() == "pass")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Reward3");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Disable3");
                        }
                        if (save.GetCheckIn4() == "pass")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Reward4");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Disable4");
                        }
                        if (save.GetCheckIn1() == "reward")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Normal1");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Reward1");
                        }
                        if (save.GetCheckIn2() == "reward")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Normal2");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Reward2");
                        }
                        if (save.GetCheckIn3() == "reward")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Normal3");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Reward3");
                        }
                        if (save.GetCheckIn4() == "reward")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Normal4");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Reward4");
                        }
                        GameObject.Find("GeneralMenuPopup").GetComponent<GeneralMenuPopup>().SetExitButtonEnable(true);
                    };
                    GameObject.Find("GeneralMenuPopup").GetComponent<GeneralMenuPopup>().OnExit += () =>
                    {
                        UIManagers.Instance.DisableUIPopUp("PassportHistoryPopup");
                        GameObject.Find("GeneralMenuPopup").GetComponent<GeneralMenuPopup>().SetExitButtonEnable(false);
                    };
                    GameObject.Find("PlayerLocal").GetComponent<PlayerHitObject>().OnHit += (name) =>
                    {
                        UIManagers.Instance.EnbleUIPopUp("CheckInPopup");
                        GameObject.Find("CheckInPopup").GetComponent<CheckInPopup>().ShowIcon(name);
                        GameObject.Find("CheckInPopup").GetComponent<CheckInPopup>().SetTextLabel(name);
                    };
                    //GameObject.Find("CheckInPopup").GetComponent<CheckInPopup>().OnCheckIn += (name) =>
                    //{
                    //    GameObject.Find("CheckInPopup").GetComponent<CheckInPopup>().DisableIcon(name);
                    //    UIManagers.Instance.DisableUIPopUp("CheckInPopup");
                    //};
                    break;
                default:
                    break;
            }
        }
    }
}
