using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenOTP : MonoBehaviour
{
    public static GenOTP instance;
    string _otp = "";

    private void Awake()
    {
        instance = this;
        GenerateOTP();
    }

    private void GenerateOTP()
    {
        int index1 = Random.Range(0, 9);
        int index2 = Random.Range(0, 9);
        int index3 = Random.Range(0, 9);
        int index4 = Random.Range(0, 9);
        int index5 = Random.Range(0, 9);
        int index6 = Random.Range(0, 9);

        _otp = index1.ToString() + index2.ToString() + index3.ToString() + index4.ToString() + index5.ToString() + index6.ToString();
    }

    public string GetOTP()
    {
        return _otp;
    }
}
