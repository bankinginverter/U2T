using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U2T.Keyboard;

public class Player : MonoBehaviour
{
    KeyboardController keyboardController;
    LobbyPhotonManager lobby;
    Rigidbody rb;

    [SerializeField] bool isLocal = true;
    private float speed = 0f;
    private float speedCurrent = 5f;

    private void Awake()
    {
        if (isLocal)
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
        }
        else
        {
            if (lobby.ViewOnline())
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
            }
        }
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
