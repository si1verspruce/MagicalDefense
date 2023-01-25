using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnPointerDownToggle : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private UnityEvent onValueChanged;

    public void OnPointerDown(PointerEventData eventData)
    {
        onValueChanged?.Invoke();
    }
}
