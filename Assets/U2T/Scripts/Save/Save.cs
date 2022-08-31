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
}
