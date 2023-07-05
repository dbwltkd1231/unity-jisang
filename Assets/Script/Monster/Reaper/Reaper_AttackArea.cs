using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper_AttackArea : MonoBehaviour
{
    public bool AttackSuccess;
    void Start()
    {
        AttackSuccess = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AttackSuccess = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AttackSuccess = false;
        }
    }
}
