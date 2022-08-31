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
        SimpleEmailSender.emailSettings.UserName = "bankinginverter@gmail.com";
        SimpleEmailSender.emailSettings.UserPass = "kfdpzapdlupukhpo";

        SimpleEmailSender.Send(email, "Authentication identify", "Please Copy this password " + GenOTP.instance.GetOTP() + " to the registerpage", "", SendCompletedCallback);
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
