using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharactorScreen : MonoBehaviour
{
    public Button leftBtn;
    public Button rightBtn;
    public Button enterBtn;

    public delegate void GeneralDelegate();
    public GeneralDelegate OnLeftBtn = null;
    public GeneralDelegate OnRightBtn = null;
    public GeneralDelegate OnEnter = null;
    public GeneralDelegate OnSelectBtn = null;

    private void Awake()
    {
        leftBtn.onClick.AddListener(Left);
        rightBtn.onClick.AddListener(Right);
        enterBtn.onClick.AddListener(Enter);
    }

    public void Left()
    {
        if (OnLeftBtn != null)
        {
            OnLeftBtn();
        }
    }

    public void Right()
    {
        if (OnRightBtn != null)
        {
            OnRightBtn();
        }
    }

    public void Enter()
    {
        OnEnter?.Invoke();
    }

    public void SelectPlayer()
    {
        if (OnSelectBtn != null)
        {
            OnSelectBtn();
        }
    }
}
