using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    private Vector3 playerTransform;
    private Quaternion playerRotation;

    public void TransformPlayer()
    {
        playerTransform = this.transform.position;
        playerRotation = this.transform.rotation;
    }

    public Vector3 GetTransform()
    {
        return playerTransform;
    }

    public Quaternion GetRotation()
    {
        return playerRotation;
    }

}
