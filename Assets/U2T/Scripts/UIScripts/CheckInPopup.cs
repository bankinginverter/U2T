using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInPopup : MonoBehaviour
{
    public delegate void CheckInPopupDelegate(string name);
    public CheckInPopupDelegate OnCheckIn = null;

    private Button _checkInButton;
    private Text _textLabel;
    private string _nameIcon;

    private void Start()
    {
        _textLabel = GameObject.Find("TextLabel").GetComponent<Text>();
        _checkInButton.onClick.AddListener(OnCheckedIn);
    }

    public void SetTextLabel(string text)
    {
        _textLabel.text = text;
    }

    public void ShowIcon(string nameIcon)
    {
        _nameIcon = nameIcon;
        GameObject.Find(nameIcon).GetComponent<Image>().enabled = true;
    }

    public void DisableIcon(string nameIcon)
    {
        GameObject.Find(nameIcon).GetComponent<Image>().enabled = false;
    }

    public void OnCheckedIn()
    {
        OnCheckIn?.Invoke(_nameIcon);
    }
}
