using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryPhotharamPopup : MonoBehaviour
{
    public delegate void GeneralDelegate();

    public GeneralDelegate OnShowPopup = null;
    public GeneralDelegate OnExitPopup = null;

    private Button _closeButton;

    private void Start()
    {
        _closeButton = GameObject.Find("CloseButton").GetComponent<Button>();
        _closeButton.onClick.AddListener(OnExit);
        OnShowPopup?.Invoke();
    }

    public void OnExit()
    {
        OnExitPopup?.Invoke();
    }
}
