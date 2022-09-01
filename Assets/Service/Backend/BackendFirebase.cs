using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class BackendFirebase : MonoBehaviour
{

    private void Start()
    {
        PostToDataBase();
    }

    private void PostToDataBase()
    {
        Player p1 = new Player("banking", "12345678");
        RestClient.Post("https://u2twebgl-default-rtdb.asia-southeast1.firebasedatabase.app/.json",p1);
        //Debug.Log(RestClient.Get("https://u2twebgl-default-rtdb.asia-southeast1.firebasedatabase.app/.json"));
    }
}

public class Player
{
    string _username;
    string _password;

    public Player(string username, string password)
    {
        _username = username;
        _password = password;
    }
}