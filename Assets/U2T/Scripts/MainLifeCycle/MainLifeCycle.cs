using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U2T.Keyboard;
using U2T.Foundation;

public class MainLifeCycle : MonoBehaviour
{
    PlayerTransform playerTransform;
    AppStateManager appStateManager;

    private void Awake()
    {
        playerTransform = GameObject.Find("PlayerLocal").GetComponent<PlayerTransform>();
        appStateManager.OnStateChange?.Invoke();
        appStateManager.OnStateChange += () =>
        {
            appStateManager.ChangeAppState(AppStateManager.GameState.PHOTON_CONNECTING);
        };
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        playerTransform.TransformPlayer();
    }

    private void Initialize()
    {

    }
}
