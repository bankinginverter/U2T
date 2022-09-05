using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitObject : MonoBehaviour
{
    public delegate void PlayerHitObjectDelegate(string name, string tag);
    public PlayerHitObjectDelegate OnHit = null;

    private GameObject _currentObjectHit = null;
    private bool _playerHit = true;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Item")
        {
            Debug.Log(hit.collider.name);
            OnHit?.Invoke(hit.collider.name,hit.collider.tag);
            GameObject temp = GameObject.Find(hit.collider.name);
            Destroy(temp);
        }

        if (hit.collider.tag == "Gallery" && _playerHit)
        {
            _playerHit = false;
            Debug.Log(hit.collider.name);
            OnHit?.Invoke(hit.collider.name, hit.collider.tag);
            _currentObjectHit = hit.gameObject;
        }

        if (hit.collider.tag == "360View" && _playerHit)
        {
            _playerHit = false;
            Debug.Log(hit.collider.name);
            OnHit?.Invoke(hit.collider.name, hit.collider.tag);
            _currentObjectHit = hit.gameObject;
        }

        if (_currentObjectHit != null)
        {
            Debug.Log(Vector3.Distance(_currentObjectHit.transform.position, this.gameObject.transform.position));
            if (Vector3.Distance(_currentObjectHit.transform.position, this.gameObject.transform.position) > 5f)
            {
                _playerHit = true;
                _currentObjectHit = null;
            }
        }
    }
}
