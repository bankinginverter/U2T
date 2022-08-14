using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    private Vector3 playerTransform;

    public void TransformPlayer()
    {
        playerTransform = this.transform.position;
    }

    public Vector3 GetTransform()
    {
        return playerTransform;
    }

}
