using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ChatMessage : MonoBehaviourPun
{
    [SerializeField] RectTransform parent;

    private PhotonView view;
    private InputField chatInputField;
    private Text updateText;
    private int countMessage = 0;

    string currentText = "";

    private void Awake()
    {
        //instace = this;
        //a = GetComponent<RectTransform>();
        //parent.sizeDelta = new Vector2(10f,100f);
        view = GetComponent<PhotonView>();
        chatInputField = GameObject.Find("SendMessageBox").GetComponent<InputField>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            SendMessageToOtherPlayer();
        }
        else
        {
            SendMessageToOtherPlayer();
        }
    }

    private void SendMessageToOtherPlayer()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && chatInputField.text != "")
        {
            GameObject msgBox = Instantiate(Resources.Load("DialogBoxLocal") as GameObject);
            msgBox.transform.parent = parent.transform;
            msgBox.transform.localScale = new Vector3(1f,1f,1f);
            msgBox.transform.GetChild(0).GetComponent<Text>().text = chatInputField.text + "  ";
            currentText = chatInputField.text;
            view.RPC("ISend", RpcTarget.Others, currentText);
            chatInputField.text = "";
            countMessage++;
        }
    }

    private void ExplaneParentScale()
    {

    }

    [PunRPC]
    void ISend(string message)
    {
        GameObject msgBoxOther = Instantiate(Resources.Load("DialogBoxOther") as GameObject);
        msgBoxOther.transform.parent = parent.transform;
        msgBoxOther.transform.localScale = new Vector3(1f, 1f, 1f);
        msgBoxOther.transform.GetChild(0).GetComponent<Text>().text = "  " + message;
        countMessage++;
    }
}
