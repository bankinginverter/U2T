using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGamePlay : MonoBehaviour
{
    public delegate void GeneralDelegate();
    public GeneralDelegate OnLock = null;
    public GeneralDelegate OnUnLock = null;

    PlayerTransform _playerTransform;
    bool _stateMouse = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerTransform = GameObject.Find("PlayerLocal").GetComponent<PlayerTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_stateMouse)
            {
                Cursor.lockState = CursorLockMode.Locked;
                OnUnLock?.Invoke();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
                OnLock?.Invoke();
            }
            _stateMouse = !_stateMouse;
        }
        _playerTransform.TransformPlayer();
    }
}
