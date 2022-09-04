using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInPopup : MonoBehaviour
{
    public delegate void GeneralDelegate();
    public delegate void CheckInPopupDelegate(string name);
    public CheckInPopupDelegate OnCheckIn = null;
    public GeneralDelegate OnShowCheckInPopup = null;

    private Button _checkInButton;
    [SerializeField] private Image _imageIcon1;
    [SerializeField] private Image _imageIcon2;
    [SerializeField] private Image _imageIcon3;
    [SerializeField] private Image _imageIcon4;
    private Text _textLabel;
    private string _nameIcon;

    private void Start()
    {
        OnShowCheckInPopup?.Invoke();
        _checkInButton = GameObject.Find("CheckInButton").GetComponent<Button>();
        _checkInButton.onClick.AddListener(OnCheckedIn);
    }

    public void SetTextLabel(string text)
    {
        _textLabel = GameObject.Find("TextLabel").GetComponent<Text>();
        _textLabel.text = text;
    }

    public void ShowIcon(string nameIcon)
    {
        _nameIcon = nameIcon;
        switch (nameIcon)
        {
            case "Katom":
                _imageIcon1.enabled = true;
                break;
            case "Donom":
                _imageIcon2.enabled = true;
                break;
            case "Gallery":
                _imageIcon3.enabled = true;
                break;
            case "River":
                _imageIcon4.enabled = true;
                break;
            default:
                break;
        }
    }

    public void DisableIcon(string nameIcon)
    {
        GameObject.Find(nameIcon).GetComponent<Image>().enabled = false;
    }

    public void OnCheckedIn()
    {
        Debug.Log("Push");
        OnCheckIn?.Invoke(_nameIcon);
    }
}
