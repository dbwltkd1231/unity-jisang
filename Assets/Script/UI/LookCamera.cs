using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCamera : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
