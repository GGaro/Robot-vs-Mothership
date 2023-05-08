using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToCamera : MonoBehaviour
{
    [SerializeField] GameObject Camera;
    void Update()
    {
        transform.LookAt(Camera.transform);
    }
}
