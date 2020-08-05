using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageResize : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //[SerializeField] private GameObject gmObj;
    private RectTransform rectTransform;
    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnPointerDown(PointerEventData eventData) {
        print("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        print("OnBeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData) {
        print("OnEndDrag");
    }

    public void OnDrag(PointerEventData eventData) {
        print("OnDrag");
        rectTransform.anchoredPosition += eventData.delta;
    }
}
