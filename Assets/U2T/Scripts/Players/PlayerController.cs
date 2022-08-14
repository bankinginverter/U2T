using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U2T.Keyboard;

public class PlayerController : MonoBehaviour
{
    KeyboardController keyboardController;
    LobbyPhotonManager lobby;
    Rigidbody rb;

    [SerializeField] bool isLocal = true;
    private float speedCurrent = 5f;
    private float speed = 0f;

    private void Awake()
    {
        Initialized();
        OnPushKeyboard();
    }

    private void Initialized()
    {
        rb = GetComponent<Rigidbody>();
        keyboardController = GameObject.Find("KeyboardManager").GetComponent<KeyboardController>();
    }

    private void OnPushKeyboard()
    {
        keyboardController.OnKeyDown += () =>
        {
            if (Input.GetKey(KeyCode.W))
            {
                MoveVertical(speedCurrent);
            }
            if (Input.GetKey(KeyCode.S))
            {
                MoveVertical(-speedCurrent);
            }
            if (Input.GetKey(KeyCode.A))
            {
                MoveHorizontal(-speedCurrent);
            }
            if (Input.GetKey(KeyCode.D))
            {
                MoveHorizontal(speedCurrent);
            }
        };

        keyboardController.OnKeyUp += () =>
        {
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                speed = 0f;
            }
        };
    }

    private void MoveVertical(float speedH)
    {
        speed = speedH;
        rb.velocity = transform.forward * speed;
    }

    private void MoveHorizontal(float speedV)
    {
        speed = speedV;
        rb.velocity = transform.right * speed;
    }
}
