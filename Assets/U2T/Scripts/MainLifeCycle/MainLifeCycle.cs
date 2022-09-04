using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U2T.Keyboard;
using U2T.Foundation;
using UnityEngine.SceneManagement;

public class MainLifeCycle : MonoBehaviour
{
    PlayerAnimation playerAnimation;
    PlayerTransform playerTransform;
    AppStateManager appStateManager;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //playerTransform = GameObject.Find("PlayerLocal").GetComponent<PlayerTransform>();
        appStateManager = GameObject.Find("SceneManager").GetComponent<AppStateManager>();
        appStateManager.OnStateChange += () =>
        {
            Debug.Log("IN");
            appStateManager.ChangeAppState(Enumulator.GameState.PHOTON_CONNECTING);
        };
    }

    private void Start()
    {

    }

    private void Update()
    {
        //playerTransform.TransformPlayer();
    }

    private void Initialize()
    {

    }
}
