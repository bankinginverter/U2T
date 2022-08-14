using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace U2T.Keyboard
{
    public class KeyboardController : MonoBehaviour
    {
        public delegate void KeyDelegate();
        public KeyDelegate OnKeyUp = null;
        public KeyDelegate OnKeyDown = null;

        private void Update()
        {
            InputKeyboard();
        }

        public void InputKeyboard()
        {
            OnKeyUp?.Invoke();
            OnKeyDown?.Invoke();
        }
    }
}
