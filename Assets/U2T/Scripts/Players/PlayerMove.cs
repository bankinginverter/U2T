using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U2T.Keyboard;

public class PlayerMove : MonoBehaviour
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
        keyboardController.OnKeyDown += () =>
        {
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.position += new Vector3(0f,0f,3f);
                //MoveVertical(speedCurrent);
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

    private void Initialized()
    {
        GameObject clone = new GameObject("KeyboardController");
        keyboardController = clone.AddComponent<KeyboardController>();
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
