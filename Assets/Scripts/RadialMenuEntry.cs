using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI label;

    public Action<RadialMenuEntry> CallbackEnter;
    public Action<RadialMenuEntry> CallbackClick;

    [SerializeField] private Image icon;

    public void SetLabel(string newLabelText)
    {
        if (!string.IsNullOrEmpty(newLabelText))
        {
            label.gameObject.SetActive(true);
            label.text = newLabelText;
        }
        else
        {
            label.gameObject.SetActive(false);
        }
    }

    public void SetIcon(Sprite newIcon)
    {
        icon.sprite = newIcon;
    }

    public Sprite GetIcon()
    {
        return icon.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CallbackClick?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CallbackEnter?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CallbackEnter?.Invoke(null);
    }
}