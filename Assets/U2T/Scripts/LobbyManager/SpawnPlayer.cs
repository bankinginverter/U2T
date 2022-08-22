using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject _spawnPoint;
    //[SerializeField] SelectCharacter selectCharacter;

    void Start()
    {
        PhotonNetwork.Instantiate(PlayerPrefs.GetString("character"),_spawnPoint.transform.position, Quaternion.identity);
    }

}
