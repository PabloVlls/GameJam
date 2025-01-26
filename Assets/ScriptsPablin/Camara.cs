using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform follow;
    public float distance;

    void LateUpdate()
    {
        transform.position = follow.position;
    }
}
