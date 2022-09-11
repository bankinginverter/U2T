using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U2T.Keyboard;

public class PlayerViewer : MonoBehaviour
{
    [SerializeField] Transform playerClientTransfrom;
    [SerializeField] float fps = 0f;
    [SerializeField] float tps = 0f;

    KeyboardController keyboardController;
    private float xAxisRotation;
    private bool swtichView = true;
    private bool _isActive = true;

    private void Awake()
    {
        Initialized();
        keyboardController.OnKeyDown += () =>
        {
            if (Input.GetKeyDown(KeyCode.V) && _isActive)
            {
                swtichView = !swtichView;
                if (swtichView)
                {
                    ChangeCameraViewFPS();
                }
                else
                {
                    ChangeCameraViewTPS();
                }
            }
        };
    }

    private void Update()
    {
        if (_isActive)
        {
            float mouseX = Input.GetAxis("Mouse X") * 200f * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * 200f * Time.deltaTime;
            xAxisRotation += mouseY;
            xAxisRotation = Mathf.Clamp(xAxisRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xAxisRotation * -1, 0f, 0f);
            playerClientTransfrom.Rotate(0f, mouseX, 0f);
        }
    }

    private void Initialized()
    {
        keyboardController = GameObject.Find("KeyboardManager").GetComponent<KeyboardController>();
    }

    private void ChangeCameraViewFPS()
    {
        this.transform.localPosition = new Vector3(0f, 3.05f, fps);
    }

    private void ChangeCameraViewTPS()
    {
        this.transform.localPosition = new Vector3(0f, 3.05f, tps);
    }

    public void SetActiveViewer(bool isActive)
    {
        _isActive = isActive;
    }
}
