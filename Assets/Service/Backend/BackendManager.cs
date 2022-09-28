using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using UnityEngine;
using Proyecto26;
using RSG;

public class BackendManager : MonoBehaviour
{
    public string _databaseUrl;
    User user;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _databaseUrl = "https://photharammetaverse-default-rtdb.asia-southeast1.firebasedatabase.app/";
        user = new User();
    }

    public void saveDataToDatabase(string username, string password)
    {
        user = new User();
        user.UserName = username;
        string[] splitWord = user.UserName.Split('@');

        RestClient.Put<User>(_databaseUrl + "/" + splitWord[0] + ".json", new User
        {
            UserName = username,
            Password = password,
        });
    }

    public async Task <string> ReadDataUsername(string username)
    {
        user = new User();
        var a = RestClient.Get<User>(_databaseUrl + "/" + username + ".json").Then(response =>
        {
            user = response;
        });
        await Task.Delay(1000);
        return user.UserName;
    }

    public async Task<string> ReadDataPassword(string username)
    {
        user = new User();
        RestClient.Get<User>(_databaseUrl + "/" + username + ".json").Then(response =>
        {
            user = response;
        });
        await Task.Delay(1000);
        return user.Password;
    }

    public string EncodePasswordToHAS256(string password)
    {
        var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

        var stringbuilder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            stringbuilder.Append(bytes[i].ToString());
        }
        return stringbuilder.ToString();
    }
}

public class User
{
    public string UserName;
    public string Password;
}
