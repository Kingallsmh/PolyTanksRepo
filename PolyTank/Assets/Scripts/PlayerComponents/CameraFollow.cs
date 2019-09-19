using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetToFollow;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        if (targetToFollow)
        {
            transform.position = targetToFollow.transform.position + offset;
        }
    }
}
