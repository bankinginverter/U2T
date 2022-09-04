using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enumulator
{
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
        GAMEPLAY_START,
        CHECKIN_POPUP
    }
}
