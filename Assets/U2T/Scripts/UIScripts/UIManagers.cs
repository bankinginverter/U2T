using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagers : MonoBehaviour
{
    public static UIManagers Instance;

    private List<GameObject> UI = new List<GameObject>();
    private GameObject canvas;

    private void Awake()
    {
        Instance = this;
        canvas = GameObject.Find("Canvas");
    }

    public void EnbleUIPopUp(string nameUIPopup)
    {
        GameObject gameObjectUI = Instantiate(Resources.Load<GameObject>(nameUIPopup),canvas.transform) as GameObject;
        gameObjectUI.name = nameUIPopup;
        UI.Add(gameObjectUI);
    }

    public void DisableUIPopUp(string nameUIPopup)
    {
        GameObject gameObjectUI = GameObject.Find(nameUIPopup).gameObject;
        Destroy(gameObjectUI);
        UI.Clear();
    }

    public void DisableAllUIPopup()
    {
        foreach (var item in UI)
        {
            if (item.name != "GeneralMenuPopup")
            {
                Destroy(item);
            }
        }
        UI.Clear();
    }
}
