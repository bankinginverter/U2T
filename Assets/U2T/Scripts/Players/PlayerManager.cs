using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private List<GameObject> _player = new List<GameObject>();

    public GameObject GetPlayerFromList(int index)
    {
        return _player[index];
    }

    public void AddPlayerToList(GameObject player)
    {
        _player.Add(player);
    }

    public void RemovePlayerFromList(GameObject player)
    {
        _player.Remove(player);
    }

    public void RemoveAllPlayerFromList()
    {
        _player.Clear();
    }

    public void InActivePlayer()
    {
        _player[0].GetComponent<PlayerController>().SetActivePlayer(false);
    }

    public void InActivePlayerViewer()
    {
        _player[0].GetComponentInChildren<PlayerViewer>().SetActiveViewer(false);
    }

    public void ActivePlayer()
    {
        _player[0].GetComponent<PlayerController>().SetActivePlayer(true);
    }

    public void ActivePlayerViewer()
    {
        _player[0].GetComponentInChildren<PlayerViewer>().SetActiveViewer(true);
    }

}
