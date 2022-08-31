using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Authentication : MonoBehaviour
{
    [SerializeField] InputField _otpInput;
    [SerializeField] Text _statusText;

    public delegate void VerifyDelegate();
    public VerifyDelegate OnVerified = null;

    private void ShowStatus(string status)
    {
        _statusText.text = status;
    }

    private bool OTPIsNull()
    {
        return  _otpInput.text == "";
    }

    private bool OTPInCorrect()
    {
        return _otpInput.text != GenOTP.instance.GetOTP();
    }

    public void OnVerify()
    {
        if (OTPIsNull())
        {
            Debug.Log("please enter your OTP");
            ShowStatus("please enter your OTP");
            return;
        }
        if (OTPInCorrect())
        {
            Debug.Log("OTP unmatch");
            ShowStatus("OTP unmatch");
            return;
        }
        Verified();
    }

    private void Verified()
    {
        OnVerified?.Invoke();
    }
}
