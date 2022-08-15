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
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {

                GameObject msgBox = Instantiate(Resources.Load("DialogBoxLocal") as GameObject);
                msgBox.transform.parent = parent.transform;
                msgBox.transform.GetChild(0).GetComponent<Text>().text = "  " + chatInputField.text;
                currentText = chatInputField.text;
                view.RPC("ISend", RpcTarget.Others, currentText);

                //Debug.Log("Other");
                //GameObject msgBoxOther = Instantiate(Resources.Load("DialogBoxOther") as GameObject);
                //msgBoxOther.transform.parent = parent.transform;
                //msgBoxOther.transform.GetChild(0).GetComponent<Text>().text = chatInputField.text + "  ";
                //currentText = chatInputField.text;
                //view.RPC("ISend", RpcTarget.All, currentText);
                //send = false;

                //updateText.text += chatInputField.text;
                //currentText = updateText.text + "\n";
                //view.RPC("ISend", RpcTarget.All, currentText);
                //chatInputField.text = "";

                send = false;
            }
            //if (send)
            //{
            //    GameObject msgBoxOther = Instantiate(Resources.Load("DialogBoxOther") as GameObject);
            //    msgBoxOther.transform.parent = parent.transform;
            //    msgBoxOther.transform.GetChild(0).GetComponent<Text>().text = chatInputField.text + "  ";
            //    currentText = chatInputField.text;
            //    view.RPC("ISend", RpcTarget.All, currentText);
            //}
        }
    }

    [PunRPC]
    void ISend(string message)
    {
        message = chatInputField.text;
        GameObject msgBoxOther = Instantiate(Resources.Load("DialogBoxOther") as GameObject);
        msgBoxOther.transform.parent = parent.transform;
        msgBoxOther.transform.GetChild(0).GetComponent<Text>().text = message + "  ";
    }
}
