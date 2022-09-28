using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        string _dbCompareUsername;
        string _dbComparePassword;
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

        public async void ChangeAppState(Enumulator.GameState gameState)
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
                    //ChangeAppState(Enumulator.GameState.SELECT_CHARACTER);
                    break;
                case Enumulator.GameState.FETCHING_DATA:
                    Debug.Log("AppState : GameState.FETCHINGDATA");
                    //db.Initialize();
                    //_dbCompareUsername = await GameObject.Find("BackendManager").GetComponent<BackendManager>().ReadDataUsername(save.GetUserName());
                    //_dbComparePassword = await GameObject.Find("BackendManager").GetComponent<BackendManager>().ReadDataPassword(save.GetPassword());
                    //Debug.Log("=>>2" + save.GetUserName());
                    //if (_dbCompareUsername == null)
                    //{
                    //    Debug.Log("1");
                    //    ChangeAppState(Enumulator.GameState.LOGING_IN);
                    //}
                    //if ((save.GetUserName() + "@gmail.com" == _dbCompareUsername))
                    //{
                    //    Debug.Log("2");
                    //    ChangeAppState(Enumulator.GameState.SELECT_CHARACTER);
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
                        string[] splitWord = username.Split('@');
                        GameObject.Find("AuthenPopup").GetComponent<Authentication>().OnVerified += () =>
                        {
                            save.SavePassword(GameObject.Find("BackendManager").GetComponent<BackendManager>().EncodePasswordToHAS256(password));
                            GameObject.Find("BackendManager").GetComponent<BackendManager>().saveDataToDatabase(username, save.GetPassword());
                            save.SaveUserName(splitWord[0]);
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

                    GameObject.Find("LoginPopup").GetComponent<Login>().OnLoggedin += async (username, password) =>
                    {
                        string[] splitWord = username.Split('@');
                        string u = await GameObject.Find("BackendManager").GetComponent<BackendManager>().ReadDataUsername(splitWord[0]);
                        Debug.Log("Wait==>U:" + u);
                        string _encodePassword = GameObject.Find("BackendManager").GetComponent<BackendManager>().EncodePasswordToHAS256(password);
                        string p = await GameObject.Find("BackendManager").GetComponent<BackendManager>().ReadDataPassword(splitWord[0]);
                        Debug.Log("Wait==>P:" + p);
                        if (username == u && _encodePassword == p)
                        {
                            save.SaveUserName(splitWord[0]);
                            save.SavePassword(p);
                            UIManagers.Instance.DisableUIPopUp("LoginPopup");
                            ChangeAppState(Enumulator.GameState.LOGGED_IN);
                        }
                    };
                    break;
                case Enumulator.GameState.LOGGED_IN:
                    Debug.Log("AppState : GameState.LOGGED_IN");
                    ChangeAppState(Enumulator.GameState.SELECT_CHARACTER);
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
                    PlayerManager _playerManager = new PlayerManager();
                    _playerManager.AddPlayerToList(GameObject.Find("PlayerLocal").gameObject);
                    GameObject temp = null;
                    //UIManagers.Instance.EnbleUIPopUp("GeneralMenuPopup");
                    GameObject.Find("GeneralMenuPopup").GetComponent<GeneralMenuPopup>().OnPassported += () =>
                    {
                        temp = Instantiate(Resources.Load<GameObject>("C1Present"), new Vector3(-1.54f, 1.900001f, 118.9f), Quaternion.EulerRotation(0f, -180f, 0f)) as GameObject;
                        UIManagers.Instance.EnbleUIPopUp("PassportHistoryPopup");
                        if (save.GetCheckIn1() == "pass")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Disable1");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Reward1");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowButton("KA-TOM-SA-TU");
                        }
                        if (save.GetCheckIn2() == "pass")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Disable2");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Reward2");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowButton("DO NOM COFFEE");
                        }
                        if (save.GetCheckIn3() == "pass")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Disable3");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Reward3");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowButton("PHOTHARAM GALLERY");
                        }
                        if (save.GetCheckIn4() == "pass")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Disable4");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Reward4");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowButton("MAE KLONG RIVER");
                        }
                        if (save.GetCheckIn1() == "reward")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Disable1");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Reward1");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Normal1");
                        }
                        if (save.GetCheckIn2() == "reward")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Disable2");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Reward2");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Normal2");
                        }
                        if (save.GetCheckIn3() == "reward")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Disable3");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Reward3");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Normal3");
                        }
                        if (save.GetCheckIn4() == "reward")
                        {
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Disable4");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().DisableSign("Reward4");
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowSign("Normal4");
                        }
                        if (CheckInManager.Instance.GetCounting() == 4)
                        {
                            save.SaveCheckInSuccess("Complete");
                        }

                        GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().OnButtonLocation += (name) =>
                        {
                            if (name == "KA-TOM-SA-TU")
                            {
                                save.SaveCheckIn1("reward");
                            }
                            if (name == "DO NOM COFFEE")
                            {
                                save.SaveCheckIn2("reward");
                            }
                            if (name == "PHOTHARAM GALLERY")
                            {
                                save.SaveCheckIn3("reward");
                            }
                            if (name == "MAE KLONG RIVER")
                            {
                                save.SaveCheckIn4("reward");
                            }
                            GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().ShowQRCode();
                        };
                        GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().OnButtonSelect += () =>
                        {
                            Destroy(temp);
                            Destroy(GameObject.Find("C1Presnet(Clone)"));
                        };
                        GameObject.Find("PassportHistoryPopup").GetComponent<PassportHistoryPopup>().OnConfirm += () =>
                        {
                            CharacterManagerInScene.Instance.OnOffCharacter(save.GetCharacterID());
                        };
                        GameObject.Find("GeneralMenuPopup").GetComponent<GeneralMenuPopup>().SetExitButtonEnable(true);
                    };

                    GameObject.Find("GeneralMenuPopup").GetComponent<GeneralMenuPopup>().OnExit += () =>
                    {
                        //Destroy(temp);
                        //Destroy(SelectCharacterCotroller._gameObjectCharacter);
                        //CharacterList.Instance.ClearList();
                        UIManagers.Instance.DisableUIPopUp("PassportHistoryPopup");
                        GameObject.Find("GeneralMenuPopup").GetComponent<GeneralMenuPopup>().SetExitButtonEnable(false);
                    };
                    GameObject.Find("PlayerLocal").GetComponent<PlayerHitObject>().OnDetect += (name,tag) =>
                    {
                        Debug.Log(_playerManager.GetPlayerFromList(0));
                        _playerManager.InActivePlayer();
                        _playerManager.InActivePlayerViewer();
                        GameObject.Find("MainGamePlay").GetComponent<MainGamePlay>().enabled = false;
                        if (tag == "Item")
                        {
                            UIManagers.Instance.EnbleUIPopUp("CheckInPopup");
                            GameObject.Find("CheckInPopup").GetComponent<CheckInPopup>().OnShowCheckInPopup += () =>
                            {
                                GameObject.Find("CheckInPopup").GetComponent<CheckInPopup>().ShowIcon(name);
                                GameObject.Find("CheckInPopup").GetComponent<CheckInPopup>().SetTextLabel(name);
                                CheckInManager.Instance.Counting();
                                if (name == "Katom" && save.GetCheckIn1() != "reward")
                                {
                                    save.SaveCheckIn1("pass");
                                }
                                if (name == "Donom" && save.GetCheckIn2() != "reward")
                                {
                                    save.SaveCheckIn2("pass");
                                }
                                if (name == "Gallery" && save.GetCheckIn3() != "reward")
                                {
                                    save.SaveCheckIn3("pass");
                                }
                                if (name == "River" && save.GetCheckIn4() != "reward")
                                {
                                    save.SaveCheckIn4("pass");
                                }
                            };
                            GameObject.Find("CheckInPopup").GetComponent<CheckInPopup>().OnCheckIn += (nameIconforDestory) =>
                            {
                                UIManagers.Instance.DisableUIPopUp("CheckInPopup");
                                _playerManager.ActivePlayer();
                                _playerManager.ActivePlayerViewer();
                                GameObject.Find("MainGamePlay").GetComponent<MainGamePlay>().enabled = true;
                                Cursor.lockState = CursorLockMode.Locked;
                            };
                        }
                        if (tag == "Gallery")
                        {
                            if (name == "GalleryDonom")
                            {
                                UIManagers.Instance.EnbleUIPopUp("GalleryDonomPopup");
                                GameObject.Find("GalleryDonomPopup").GetComponent<GalleryDonomPopup>().OnExitPopup += () =>
                                {
                                    UIManagers.Instance.DisableUIPopUp("GalleryDonomPopup");
                                    _playerManager.ActivePlayer();
                                    _playerManager.ActivePlayerViewer();
                                    GameObject.Find("MainGamePlay").GetComponent<MainGamePlay>().enabled = true;
                                    Cursor.lockState = CursorLockMode.Locked;
                                };
                            }
                            if (name == "GalleryPhotharam")
                            {
                                UIManagers.Instance.EnbleUIPopUp("GalleryPhotharamPopup");
                                GameObject.Find("GalleryPhotharamPopup").GetComponent<GalleryPhotharamPopup>().OnExitPopup += () =>
                                {
                                    UIManagers.Instance.DisableUIPopUp("GalleryPhotharamPopup");
                                    _playerManager.ActivePlayer();
                                    _playerManager.ActivePlayerViewer();
                                    GameObject.Find("MainGamePlay").GetComponent<MainGamePlay>().enabled = true;
                                    Cursor.lockState = CursorLockMode.Locked;
                                };
                            }
                            if (name == "GalleryRiver")
                            {
                                UIManagers.Instance.EnbleUIPopUp("GalleryRiverPopup");
                                GameObject.Find("GalleryRiverPopup").GetComponent<GalleryRiverPopup>().OnExitPopup += () =>
                                {
                                    UIManagers.Instance.DisableUIPopUp("GalleryRiverPopup");
                                    _playerManager.ActivePlayer();
                                    _playerManager.ActivePlayerViewer();
                                    GameObject.Find("MainGamePlay").GetComponent<MainGamePlay>().enabled = true;
                                    Cursor.lockState = CursorLockMode.Locked;
                                };
                            }
                        }
                        if (tag == "360View")
                        {
                            if (name == "360ViewDonom")
                            {
                                UIManagers.Instance.EnbleUIPopUp("360ViewDonomPopup");
                                GameObject.Find("360ViewDonomPopup").GetComponent<ViewDonomPopup>().OnView360Button += () => 
                                {
                                    GameObject.Find("PlayerLocal").transform.position = GameObject.Find("DonomWarp").transform.position;
                                    _playerManager.ActivePlayerViewer();
                                    GameObject.Find("MainGamePlay").GetComponent<MainGamePlay>().enabled = true;
                                };
                                GameObject.Find("360ViewDonomPopup").GetComponent<ViewDonomPopup>().OnExitPopup += () =>
                                {
                                    UIManagers.Instance.DisableUIPopUp("360ViewDonomPopup");
                                    _playerManager.ActivePlayer();
                                    GameObject.Find("PlayerLocal").transform.position = GameObject.Find("SpawnPlayer").transform.position;
                                    Cursor.lockState = CursorLockMode.Locked;
                                };
                            }
                            if (name == "360ViewPhotharam")
                            {
                                UIManagers.Instance.EnbleUIPopUp("360ViewPhotharamPopup");
                                GameObject.Find("360ViewPhotharamPopup").GetComponent<ViewPhotharamPopup>().OnView360Button += () =>
                                {
                                    GameObject.Find("PlayerLocal").transform.position = GameObject.Find("PhotharamWarp").transform.position;
                                    _playerManager.ActivePlayerViewer();
                                    GameObject.Find("MainGamePlay").GetComponent<MainGamePlay>().enabled = true;
                                };
                                GameObject.Find("360ViewPhotharamPopup").GetComponent<ViewPhotharamPopup>().OnExitPopup += () =>
                                {
                                    UIManagers.Instance.DisableUIPopUp("360ViewPhotharamPopup");
                                    _playerManager.ActivePlayer();
                                    GameObject.Find("PlayerLocal").transform.position = GameObject.Find("SpawnPlayer").transform.position;
                                    Cursor.lockState = CursorLockMode.Locked;
                                };
                            }
                            if (name == "360ViewRiver")
                            {
                                UIManagers.Instance.EnbleUIPopUp("360ViewRiverPopup");
                                GameObject.Find("360ViewRiverPopup").GetComponent<ViewRiverPopup>().OnView360Button += () =>
                                {
                                    GameObject.Find("PlayerLocal").transform.position = GameObject.Find("RiverWarp").transform.position;
                                    _playerManager.ActivePlayerViewer();
                                    GameObject.Find("MainGamePlay").GetComponent<MainGamePlay>().enabled = true;
                                };
                                GameObject.Find("360ViewRiverPopup").GetComponent<ViewRiverPopup>().OnExitPopup += () =>
                                {
                                    UIManagers.Instance.DisableUIPopUp("360ViewRiverPopup");
                                    _playerManager.ActivePlayer();
                                    GameObject.Find("PlayerLocal").transform.position = GameObject.Find("SpawnPlayer").transform.position;
                                    Cursor.lockState = CursorLockMode.Locked;
                                };
                            }
                            if (name == "360ViewKatomSatu")
                            {
                                UIManagers.Instance.EnbleUIPopUp("360ViewKatomSatuPopup");
                                GameObject.Find("360ViewKatomSatuPopup").GetComponent<ViewKatomSatuPopup>().OnView360Button += () =>
                                {
                                    GameObject.Find("PlayerLocal").transform.position = GameObject.Find("KatomSatuWarp").transform.position;
                                    _playerManager.ActivePlayerViewer();
                                    GameObject.Find("MainGamePlay").GetComponent<MainGamePlay>().enabled = true;
                                };
                                GameObject.Find("360ViewKatomSatuPopup").GetComponent<ViewKatomSatuPopup>().OnExitPopup += () =>
                                {
                                    UIManagers.Instance.DisableUIPopUp("360ViewKatomSatuPopup");
                                    _playerManager.ActivePlayer();
                                    GameObject.Find("PlayerLocal").transform.position = GameObject.Find("SpawnPlayer").transform.position;
                                    Cursor.lockState = CursorLockMode.Locked;
                                };
                            }
                            if (name == "360ViewTrain")
                            {
                                UIManagers.Instance.EnbleUIPopUp("360ViewTrainPopup");
                                GameObject.Find("360ViewTrainPopup").GetComponent<ViewTrainPopup>().OnView360Button += () =>
                                {
                                    GameObject.Find("PlayerLocal").transform.position = GameObject.Find("TrainWarp").transform.position;
                                    _playerManager.ActivePlayerViewer();
                                    GameObject.Find("MainGamePlay").GetComponent<MainGamePlay>().enabled = true;
                                };
                                GameObject.Find("360ViewTrainPopup").GetComponent<ViewTrainPopup>().OnExitPopup += () =>
                                {
                                    UIManagers.Instance.DisableUIPopUp("360ViewTrainPopup");
                                    _playerManager.ActivePlayer();
                                    GameObject.Find("PlayerLocal").transform.position = GameObject.Find("SpawnPlayer").transform.position;
                                    Cursor.lockState = CursorLockMode.Locked;
                                };
                            }
                        }
                    };
                    GameObject.Find("MainGamePlay").GetComponent<MainGamePlay>().OnLock += () =>
                    {
                        _playerManager.InActivePlayer();
                        _playerManager.InActivePlayerViewer();
                    };
                    GameObject.Find("MainGamePlay").GetComponent<MainGamePlay>().OnUnLock += () =>
                    {
                        _playerManager.ActivePlayer();
                        _playerManager.ActivePlayerViewer();
                    };
                    break;
                //case Enumulator.GameState.CHECKIN_POPUP:
                //    Debug.Log("AppState : GameState.CHECKIN_POPUP");
                //    UIManagers.Instance.EnbleUIPopUp("CheckInPopup");
                //    GameObject.Find("CheckInPopup").GetComponent<CheckInPopup>().OnShowCheckInPopup += () =>
                //    {
                //        GameObject.Find("CheckInPopup").GetComponent<CheckInPopup>().ShowIcon("River");
                //        GameObject.Find("CheckInPopup").GetComponent<CheckInPopup>().SetTextLabel("River");
                //    };
                //    GameObject.Find("CheckInPopup").GetComponent<CheckInPopup>().OnCheckIn += (name) =>
                //    {
                //        UIManagers.Instance.DisableUIPopUp("CheckInPopup");
                //        ChangeAppState(Enumulator.GameState.GAMEPLAY_START);
                //    };
                //    break;
                default:
                    break;
            }
        }
    }
}
