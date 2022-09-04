using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitObject : MonoBehaviour
{
    public delegate void PlayerHitObjectDelegate(string name);
    public PlayerHitObjectDelegate OnHit = null;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Item")
        {
            Debug.Log(hit.collider.name);
            OnHit?.Invoke(hit.collider.name);
            GameObject temp = GameObject.Find(hit.collider.name);
            Destroy(temp);
        }
    }
}
