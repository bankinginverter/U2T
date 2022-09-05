using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewKatomSatuPopup : MonoBehaviour
{
    public delegate void GeneralDelegate();

    public GeneralDelegate OnShowPopup = null;
    public GeneralDelegate OnExitPopup = null;
    public GeneralDelegate OnView360Button = null;

    private Button _closeButton;
    private Button _view360Button;

    private void Start()
    {
        _closeButton = GameObject.Find("CloseButton").GetComponent<Button>();
        _view360Button = GameObject.Find("BTN").GetComponent<Button>();
        _view360Button.onClick.AddListener(OnView360);
        _closeButton.onClick.AddListener(OnExit);
        OnShowPopup?.Invoke();
    }

    public void OnView360()
    {
        OnView360Button?.Invoke();
    }

    public void OnExit()
    {
        OnExitPopup?.Invoke();
    }
}
