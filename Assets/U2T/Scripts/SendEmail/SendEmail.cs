using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SendEmail : MonoBehaviour
{
    bool triggerResultEmail = false;
    bool resultEmailSucess;

    public void SendingGmail(string email)
    {
        SimpleEmailSender.emailSettings.STMPClient = "smtp.gmail.com";
        SimpleEmailSender.emailSettings.SMTPPort = 587;
        SimpleEmailSender.emailSettings.UserName = "PhotharamMetaverseRatchaburi@gmail.com";
        SimpleEmailSender.emailSettings.UserPass = "rdzllzwkengqeksj";

        SimpleEmailSender.Send(email, "Authentication identify", "Please Copy this password " + GetOTP() + " to the registerpage", "", SendCompletedCallback);
    }

    private string GetOTP()
    {
        int index1 = Random.Range(0, 9);
        int index2 = Random.Range(0, 9);
        int index3 = Random.Range(0, 9);
        int index4 = Random.Range(0, 9);
        int index5 = Random.Range(0, 9);
        int index6 = Random.Range(0, 9);

        return index1.ToString() + index2.ToString() + index3.ToString() + index4.ToString() + index5.ToString() + index6.ToString();
    }

    private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        if (e.Cancelled || e.Error != null)
        {
            print("Email not sent: " + e.Error.ToString());

            resultEmailSucess = false;
            triggerResultEmail = true;
        }
        else
        {
            print("Email successfully sent.");

            resultEmailSucess = true;
            triggerResultEmail = true;
        }
    }
}
