using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U2T.Keyboard;
using U2T.Foundation;

public class MainLifeCycle : MonoBehaviour
{
    PlayerAnimation playerAnimation;
    PlayerTransform playerTransform;
    AppStateManager appStateManager;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        appStateManager = GameObject.Find("SceneManager").GetComponent<AppStateManager>();
        appStateManager.OnStateChange += () =>
        {
            Debug.Log("IN");
            appStateManager.ChangeAppState(AppStateManager.GameState.PHOTON_CONNECTING);
        };
    }

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    private void Initialize()
    {

    }
}
