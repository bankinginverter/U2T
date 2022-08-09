using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U2T.Keyboard;

public class MainLifeCycle : MonoBehaviour
{
    KeyboardController keyboardController;

    private void Awake()
    {

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        keyboardController.InputKeyboard();
    }
}
