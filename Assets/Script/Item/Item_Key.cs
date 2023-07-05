using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Key : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent GetKey;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GetKey.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
