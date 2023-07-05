using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIMove : MonoBehaviour,IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [Header("부모트랜스폼"),SerializeField]
    RectTransform parentTr;

    Vector3 originpos;

    RectTransform rectTransform;
   // CanvasGroup canvasGroup;
    [SerializeField] Canvas canvas;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        rectTransform = GetComponent<RectTransform>();
      

        originpos = eventData.position;
        parentTr.transform.SetAsLastSibling();

    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        parentTr.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        
    }
}
