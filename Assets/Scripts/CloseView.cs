using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseView : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _View;

    public void OnPointerDown(PointerEventData eventData)
    {
        _View.SetActive(false);
    }
}
