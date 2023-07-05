using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Npc_merchan : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject Shop;
    [SerializeField]
    RectTransform ScrollContents;



    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Shop.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -64.5f);
        Shop.SetActive(true);
        float x = ScrollContents.anchoredPosition.x;
        ScrollContents.anchoredPosition = new Vector3(x, -30, 0);
        GameManager.Instance.IsUi = true;
    }

  
}
