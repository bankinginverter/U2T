using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U2T;
using UnityEngine.UI;
public class Register : MonoBehaviour
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

    private void ShowStatus(string status)
    {
        _statusText.text = status;
    }

    private bool EmailIsNull()
    {
        return _emailInput.text == "";
    }

    private bool PasswordIsNull()
    {
        return (_passwordInput.text == "") || (_comfirmPasswordInput.text == "");
    }

    private bool PasswordInCorrected()
    {
        return _passwordInput.text != _comfirmPasswordInput.text;
    }

    public void OnRegister()
    {
        if (EmailIsNull())
        {
            Debug.Log("please enter your email");
            ShowStatus("please enter your email");
            return;
        }
        if (PasswordIsNull())
        {
            Debug.Log("please enter your password");
            ShowStatus("please enter your password");
            return;
        }
        if (PasswordInCorrected())
        {
            Debug.Log("password or confirm password unmatch");
            ShowStatus("password or confirm password unmatch");
            return;
        }
        db.AddData(_emailInput.text, _passwordInput.text);
        ShowStatus("Registed");
    }

}
