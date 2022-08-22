using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FocusUI : MonoBehaviour
{
    private EventSystem eventsystem;
    private bool allow = true;

    public void EventSystemOn()
    {
        eventsystem = EventSystem.current;
    }

    public void FocusOnGUI()
    {

        if (eventsystem == null)
        {
            eventsystem = EventSystem.current;
            if (eventsystem == null)
            {
                return;
            }
        }
        GameObject currentObject = eventsystem.currentSelectedGameObject;
        if (currentObject != null)
        {
            InputField inputField = currentObject.GetComponent<InputField>();
            if (inputField != null)
            {
                if (inputField.isFocused)
                {
                    allow = false;
                }
                else
                {
                    allow = true;
                }
            }
        }
    }

    public bool Focus()
    {
        return allow;
    }

}
