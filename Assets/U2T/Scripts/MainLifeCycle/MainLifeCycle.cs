using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U2T.Keyboard;

public class MainLifeCycle : MonoBehaviour
{
    PlayerTransform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.Find("PlayerLocal").GetComponent<PlayerTransform>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        playerTransform.TransformPlayer();
    }

    private void Initialize()
    {

    }
}
