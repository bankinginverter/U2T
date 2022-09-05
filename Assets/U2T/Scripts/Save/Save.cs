using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save
{
    public void SaveUserName(string username)
    {
        PlayerPrefs.SetString("username", username);
    }

    public void SavePassword(string password)
    {
        PlayerPrefs.SetString("password", password);
    }

    public void SaveCharacterID(string characterID)
    {
        PlayerPrefs.SetString("character", characterID);
    }

    public void SaveCheckIn1(string isActive)
    {
        PlayerPrefs.SetString("locations1", isActive);
    }

    public void SaveCheckIn2(string isActive)
    {
        PlayerPrefs.SetString("locations2", isActive);
    }

    public void SaveCheckIn3(string isActive)
    {
        PlayerPrefs.SetString("locations3", isActive);
    }

    public void SaveCheckIn4(string isActive)
    {
        PlayerPrefs.SetString("locations4", isActive);
    }

    public void SaveCheckInSuccess(string status)
    {
        PlayerPrefs.SetString("LocationsComplete", status);
    }

    public string GetUserName()
    {
        return PlayerPrefs.GetString("username");
    }

    public string GetPassword()
    {
        return PlayerPrefs.GetString("password");
    }

    public string GetCharacterID()
    {
        return PlayerPrefs.GetString("character");
    }

    public string GetCheckIn1()
    {
        return PlayerPrefs.GetString("locations1");
    }

    public string GetCheckIn2()
    {
        return PlayerPrefs.GetString("locations2");
    }

    public string GetCheckIn3()
    {
        return PlayerPrefs.GetString("locations3");
    }

    public string GetCheckIn4()
    {
        return PlayerPrefs.GetString("locations4");
    }

    public string GetCheckInSuccess()
    {
        return PlayerPrefs.GetString("LocationsComplete");
    }
}
