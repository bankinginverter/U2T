using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagers : MonoBehaviour
{
    public static UIManagers instance;

    private GameObject canvas;

    private void Awake()
    {
        instance = this;
        canvas = GameObject.Find("Canvas");
    }

    public void EnbleUIPopUp(string nameUIPopup)
    {
        GameObject gameObjectUI = Instantiate(Resources.Load<GameObject>(nameUIPopup),canvas.transform) as GameObject;
    }

    public void DisableUIPopUp(string nameUIPopup)
    {
        GameObject gameObjectUI = GameObject.Find(nameUIPopup).gameObject;
        gameObjectUI.SetActive(false);
    }
}
