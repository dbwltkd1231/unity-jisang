using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SwordBoundaryCtrl : MonoBehaviour
{
    [SerializeField]
    GameObject[] swords;
    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<swords.Length;i++)
        {
            swords[i].transform.RotateAround(this.transform.position, Vector3.down / 4f, 40*Time.deltaTime);
        }
    }
}
