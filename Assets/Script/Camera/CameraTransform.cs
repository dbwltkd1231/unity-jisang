using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    Vector3 CameraPos;
    public bool InShop;
 
  
    private void FixedUpdate()
    {
        if(InShop==false)
        {
            transform.position = player.transform.position + CameraPos;
        }
     
        else
        {
            transform.position = player.transform.position + new Vector3(3, CameraPos.y, CameraPos.z);
        }
        
    }
}
