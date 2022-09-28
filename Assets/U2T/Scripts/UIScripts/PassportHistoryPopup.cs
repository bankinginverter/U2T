using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassportHistoryPopup : MonoBehaviour
{
    [SerializeField] private GameObject QRcode;

    public delegate void GeneralDelegate(string name);
    public delegate void ButtonDelegate();
    public delegate void ConfirmDelegate();

    public GeneralDelegate OnButtonLocation = null;
    public ButtonDelegate OnButtonSelect = null;
    public ConfirmDelegate OnConfirm = null;

    private Button _katomsatuButton;
    private Button _donomButton;
    private Button _photharamButton;
    private Button _riverButton;
    private Button _NextButton;
    private Button _BackButton;
    private Button _ConfirmButton;

    private void Awake()
    {
        _katomsatuButton = GameObject.Find("KA-TOM-SA-TU").GetComponent<Button>();
        _donomButton = GameObject.Find("DO NOM COFFEE").GetComponent<Button>();
        _photharamButton = GameObject.Find("PHOTHARAM GALLERY").GetComponent<Button>();
        _riverButton = GameObject.Find("MAE KLONG RIVER").GetComponent<Button>();
        _NextButton = GameObject.Find("NextButton").GetComponent<Button>();
        _BackButton = GameObject.Find("BackButton").GetComponent<Button>();
        _ConfirmButton = GameObject.Find("ConfirmButton").GetComponent<Button>();

        _katomsatuButton.onClick.AddListener(Katomsatu);
        _donomButton.onClick.AddListener(Donom);
        _photharamButton.onClick.AddListener(Photharam);
        _riverButton.onClick.AddListener(River);
        _NextButton.onClick.AddListener(Next);
        _BackButton.onClick.AddListener(Back);
        _ConfirmButton.onClick.AddListener(Confirm);
    }

    public void Next()
    {
        OnButtonSelect?.Invoke();
    }

    public void Back()
    {
        OnButtonSelect?.Invoke();
    }

    public void Confirm()
    {
        OnConfirm?.Invoke();
    }

    public void Katomsatu()
    {
        OnButtonLocation?.Invoke("KA-TOM-SA-TU");
    }

    public void Donom()
    {
        OnButtonLocation?.Invoke("DO NOM COFFEE");
    }

    public void Photharam()
    {
        OnButtonLocation?.Invoke("PHOTHARAM GALLERY");
    }

    public void River()
    {
        OnButtonLocation?.Invoke("MAE KLONG RIVER");
    }

    public void ShowSign(string nameSign)
    {
        GameObject.Find(nameSign).GetComponent<Image>().enabled = true;
    }

    public void DisableSign(string nameSign)
    {
        GameObject.Find(nameSign).GetComponent<Image>().enabled = false;
    }

    public void ShowButton(string nameButton)
    {
        GameObject.Find(nameButton).GetComponent<Button>().enabled = true;
    }

    public void DiableButton(string nameButton)
    {
        GameObject.Find(nameButton).GetComponent<Button>().enabled = false;
    }

    public void ShowQRCode()
    {
        QRcode.SetActive(true);
    }

    public void DisableQRCode()
    {
        QRcode.SetActive(false);
    }
}
