using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U2T.Keyboard;

public class PlayerController : MonoBehaviour
{
    KeyboardController keyboardController;
    FocusUI focusUI;
    LobbyPhotonManager lobby;
    Rigidbody rb;
    CharacterController _characterController;

    private float _speedCurrent = 5f;
    private float _speed = 0f;
    private float _gravity = -20f;

    private void Awake()
    {
        Initialized();
        OnPushKeyboard();
    }

    private void Initialized()
    {
        rb = GetComponent<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        keyboardController = GameObject.Find("KeyboardManager").GetComponent<KeyboardController>();
        focusUI = GameObject.Find("FocusUI").GetComponent<FocusUI>();
        focusUI.EventSystemOn();
    }

    private void OnPushKeyboard()
    {
        keyboardController.OnKeyDown += () =>
        {
            focusUI.FocusOnGUI();
            if (focusUI.Focus())
            {
                if (Input.GetKey(KeyCode.W))
                {
                    MoveVertical(_speedCurrent);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    MoveVertical(-_speedCurrent);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    MoveHorizontal(-_speedCurrent);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    MoveHorizontal(_speedCurrent);
                }
            }
        };

        keyboardController.OnKeyUp += () =>
        {
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                _speed = 0f;
            }
        };
    }

    private void MoveVertical(float speedH)
    {
        _speed = speedH;
        Vector3 _move = transform.TransformDirection(Vector3.forward * _speed);
        _move.y += _gravity;
        _characterController.Move(_move * Time.deltaTime);
    }

    private void MoveHorizontal(float speedV)
    {
        _speed = speedV;
        Vector3 _move = transform.TransformDirection(Vector3.right * _speed);
        _move.y += _gravity;
        _characterController.Move(_move * Time.deltaTime);
    }
}
