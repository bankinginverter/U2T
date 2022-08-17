using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U2T;
using UnityEngine.UI;
public class Demo : MonoBehaviour
{
    [SerializeField] InputField _emailInput;
    [SerializeField] InputField _passwordInput;
    [SerializeField] InputField _comfirmPasswordInput;
    [SerializeField] Text _statusText;
    BackendManager db;


    private void Start()
    {
        db = new BackendManager();
        db.Initialize();
    }

    private void ShowStatus()
    {
        _statusText.text = "Registed";
    }

    public void Register()
    {
        db.AddData(_emailInput.text, _passwordInput.text);
        ShowStatus();
    }
}
