using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitObject : MonoBehaviour
{
    public delegate void PlayerDetectObjectDelegate(string name, string tag);
    public delegate void PlayerUnDetectObjectDelegate();
    public PlayerDetectObjectDelegate OnDetect = null;
    public PlayerUnDetectObjectDelegate UnDetect = null;

    private GameObject _currentObjectHit = null;
    private bool _playerHit = true;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Item")
        {
            Debug.Log(hit.collider.name);
            OnDetect?.Invoke(hit.collider.name,hit.collider.tag);
            _currentObjectHit = hit.gameObject;
            GameObject temp = GameObject.Find(hit.collider.name);
            Destroy(temp);
        }

        if (hit.collider.tag == "Gallery" && _playerHit)
        {
            _playerHit = false;
            Debug.Log(hit.collider.name);
            OnDetect?.Invoke(hit.collider.name, hit.collider.tag);
            _currentObjectHit = hit.gameObject;
        }

        if (hit.collider.tag == "360View" && _playerHit)
        {
            _playerHit = false;
            Debug.Log(hit.collider.name);
            OnDetect?.Invoke(hit.collider.name, hit.collider.tag);
            _currentObjectHit = hit.gameObject;
            Cursor.lockState = CursorLockMode.Confined;
        }
        if (_currentObjectHit != null)
        {
            Debug.Log(Vector3.Distance(_currentObjectHit.transform.position, this.gameObject.transform.position));
            if (Vector3.Distance(_currentObjectHit.transform.position, this.gameObject.transform.position) > 3f)
            {
                _playerHit = true;
                _currentObjectHit = null;
            }
        }
    }
}
