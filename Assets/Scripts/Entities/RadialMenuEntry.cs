using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, InteractableObject
{
    public int _id;

    [SerializeField] private TextMeshProUGUI label;

    public Action<RadialMenuEntry> CallbackEnter;
    public Action<RadialMenuEntry> CallbackClick;

    public UnityEvent OnPointerEnterEvent;
    public UnityEvent OnPointerExitEvent;

    [SerializeField] private Image icon;

    public void SetLabel(string newLabelText)
    {
        if (label == null)
        {
            return;
        }

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
        if (icon == null)
        {
            return;
        }

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
        OnPointerEnterEvent?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CallbackEnter?.Invoke(null);
        OnPointerExitEvent?.Invoke();
    }

    private void OnMouseOver()
    {
        CallbackEnter?.Invoke(this);
        OnPointerEnterEvent?.Invoke();
    }

    private void OnMouseExit()
    {
        CallbackEnter?.Invoke(null);
        OnPointerExitEvent?.Invoke();
    }

    public void UseInteractable()
    {
        CallbackClick?.Invoke(this);
    }

    public void OverInteractable()
    {
        CallbackEnter?.Invoke(this);
        OnPointerEnterEvent?.Invoke();
    }

    public void LeavOverInteractable()
    {
        CallbackEnter?.Invoke(null);
        OnPointerExitEvent?.Invoke();
    }
}