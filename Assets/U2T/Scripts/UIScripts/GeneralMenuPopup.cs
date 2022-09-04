using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralMenuPopup : MonoBehaviour
{
    public delegate void GeneralDelegate();

    public GeneralDelegate OnPassported = null;
    public GeneralDelegate OnExit = null;

    private Button _passportButton;
    private Button _exitButton;

    private bool isActiveComponent;

    private void Awake()
    {
        _passportButton = GameObject.Find("PassportButton").GetComponent<Button>();
        _exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        _passportButton.onClick.AddListener(OnPassportButton);
        _exitButton.onClick.AddListener(OnExitButton);
    }

    public void OnPassportButton()
    {
        OnPassported?.Invoke();
    }

    public void OnExitButton()
    {
        OnExit?.Invoke();
    }

    public void SetExitButtonEnable(bool isActive)
    {
        _exitButton.GetComponent<Image>().enabled = isActive;
        _exitButton.GetComponent<Button>().interactable = isActive;
    }

    public void SetExitButtonDisable(bool isActive)
    {
        _exitButton.GetComponent<Image>().enabled = isActive;
        _exitButton.GetComponent<Button>().interactable = isActive;
    }
}
