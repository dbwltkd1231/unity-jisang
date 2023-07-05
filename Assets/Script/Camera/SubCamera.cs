using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCamera : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position = new Vector3(PlayerManager.Instance.transform.position.x, 0, PlayerManager.Instance.transform.position.z);
    }
}
