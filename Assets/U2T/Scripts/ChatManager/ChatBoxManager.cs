using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBoxManager : MonoBehaviour
{
    public static ChatBoxManager Instance;

    [SerializeField] GameObject ChatBox;

    private void Start()
    {
        Instance = this;
    }

}
