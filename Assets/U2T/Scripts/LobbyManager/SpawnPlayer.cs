using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    Save save;

    [SerializeField] GameObject _spawnPoint;

    private void Awake()
    {
        save = new Save();
    }

    void Start()
    {
        PhotonNetwork.Instantiate(save.GetCharacterID(),_spawnPoint.transform.position, Quaternion.identity);
        //PhotonNetwork.Instantiate("Test", _spawnPoint.transform.position, Quaternion.identity);
        //CharacterManagerInScene.Instance.OnOffCharacter(save.GetCharacterID());
    }

}
