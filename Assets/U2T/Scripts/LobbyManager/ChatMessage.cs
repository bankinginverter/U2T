using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatMessage : MonoBehaviourPun
{
    [SerializeField] Transform parent;

    private PhotonView view;
    private InputField chatInputField;
    private Text updateText;

    private bool send = false;

    string currentText = "";

    private void Awake()
    {
        //instace = this;
        view = GetComponent<PhotonView>();
        //updateText = GameObject.Find("TextboxView").GetComponent<Text>();
        chatInputField = GameObject.Find("SendMessageBox").GetComponent<InputField>();
    }

    private void Update()
    {

        if (view.IsMine)
        {
            if (send || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (PhotonNetwork.LocalPlayer.IsLocal)
                {
                    GameObject msgBox = Instantiate(Resources.Load("DialogBoxLocal") as GameObject);
                    msgBox.transform.parent = parent.transform;
                    msgBox.transform.GetChild(0).GetComponent<Text>().text = "  " + chatInputField.text;
                    currentText = chatInputField.text;
                    view.RPC("ISend", RpcTarget.All, currentText);
                }
                else
                {
                    GameObject msgBox = Instantiate(Resources.Load("DialogBoxOther") as GameObject);
                    msgBox.transform.parent = parent.transform;
                    msgBox.transform.GetChild(0).GetComponent<Text>().text = chatInputField.text + "  ";
                    currentText = chatInputField.text;
                    view.RPC("ISend", RpcTarget.All, currentText);
                }
                //updateText.text += chatInputField.text;
                //currentText = updateText.text + "\n";
                //view.RPC("ISend", RpcTarget.All, currentText);
                //chatInputField.text = "";
                send = false;
            }
        }
    }

    public void Send()
    {
        send = true;
    }

    [PunRPC]
    void ISend(string message)
    {
        message = chatInputField.text;
    }
}
