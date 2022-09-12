using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class BackendFirebase
{
    public string _databaseUrl = "https://photharammetaverse-default-rtdb.asia-southeast1.firebasedatabase.app/";
    private RequestHelper currentRequest;
    UserPlayer user = new UserPlayer();


    public void saveDataToDatabase(string username, string password)
    {
        user.UserName = username;

        RestClient.Put<UserPlayer>(_databaseUrl + "/" + user.UserName + ".json", new UserPlayer
        {
            UserName = username,
            Password = password,
        });
    }

    public void ReadData(string username)
    {
        RestClient.Get<UserPlayer>(_databaseUrl + "/"+ username + ".json").Then(response =>
        {
            user = response;
            Debug.Log(user.UserName);
        });
    }
}

public class UserPlayer
{
    public string UserName;
    public string Password;
}