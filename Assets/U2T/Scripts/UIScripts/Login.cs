using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] InputField _emailInput;
    [SerializeField] InputField _passwordInput;
    [SerializeField] Text _statusText;

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
        return _passwordInput.text == "";
    }

    private bool PasswordInCorrected()
    {
        return _passwordInput.text != "";
    }

    public void OnLogin()
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
        ShowStatus("Registed");
    }
}
