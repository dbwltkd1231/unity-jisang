using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObj : MonoBehaviour
{
    [SerializeField]
    Material[] hidematerials;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            for(int i=0;i< hidematerials.Length;i++)
            {
                hidematerials[i].SetFloat("_Transp", 0.5f);
            }
        }
      
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < hidematerials.Length; i++)
            {
                hidematerials[i].SetFloat("_Transp", 1f);
            }
        }
    }
}
//인벤토리-상점-케릭터 연동