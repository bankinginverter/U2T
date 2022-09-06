using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGamePlay : MonoBehaviour
{
    PlayerTransform _playerTransform;
    bool _stateMouse = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _playerTransform = GameObject.Find("PlayerLocal").GetComponent<PlayerTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_stateMouse)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            _stateMouse = !_stateMouse;
        }
        Debug.Log(_stateMouse);
        _playerTransform.TransformPlayer();
    }
}
