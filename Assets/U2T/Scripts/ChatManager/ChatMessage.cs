using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class ChatMessage : MonoBehaviourPun
{
    [SerializeField] GameObject _chatBox;
    [SerializeField] RectTransform parent;

    private PhotonView view;
    private InputField chatInputField;
    private Text updateText;
    private Button _sendButton;
    private int countMessage = 0;

    string currentText = "";

    private void Awake()
    {
        //_sendButton = GameObject.Find("SendButton").GetComponent<Button>();
        //_sendButton.onClick.AddListener(SendMessageToOtherPlayerWithButton);
        view = GetComponent<PhotonView>();
        //chatInputField = GameObject.Find("SendMessageBox").GetComponent<InputField>();
        chatInputField = _chatBox.GetComponentInChildren<InputField>();
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
            ExplaneParentScale();
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

    public void SendMessageToOtherPlayerWithButton()
    {
        ExplaneParentScale();
        GameObject msgBox = Instantiate(Resources.Load("DialogBoxLocal") as GameObject);
        msgBox.transform.parent = parent.transform;
        msgBox.transform.localScale = new Vector3(1f, 1f, 1f);
        msgBox.transform.GetChild(0).GetComponent<Text>().text = chatInputField.text + "  ";
        currentText = chatInputField.text;
        view.RPC("ISend", RpcTarget.Others, currentText);
        chatInputField.text = "";
        countMessage++;
    }

    private void ExplaneParentScale()
    {
        if (countMessage > 10)
        {
            parent.localPosition = new Vector2(0f, parent.localPosition.y + 32f);       
            parent.sizeDelta = new Vector2(0f, parent.rect.height + 30);
        }
    }

    [PunRPC]
    void ISend(string message)
    {
        ExplaneParentScale();
        GameObject msgBoxOther = Instantiate(Resources.Load("DialogBoxOther") as GameObject);
        msgBoxOther.transform.parent = parent.transform;
        msgBoxOther.transform.localScale = new Vector3(1f, 1f, 1f);
        msgBoxOther.transform.GetChild(0).GetComponent<Text>().text = "  " + message;
        countMessage++;
    }
}
