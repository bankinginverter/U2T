using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGamePlay : MonoBehaviour
{
    PlayerTransform _playerTransform;

    private void Awake()
    {
        _playerTransform = GameObject.Find("PlayerLocal").GetComponent<PlayerTransform>();
    }

    private void Update()
    {
        _playerTransform.TransformPlayer();
    }
}
