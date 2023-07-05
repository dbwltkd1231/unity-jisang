using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Npc_Master : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    GameObject craft;
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        craft.SetActive(true);
        craft.GetComponent<Craft>().init();

    }


}
