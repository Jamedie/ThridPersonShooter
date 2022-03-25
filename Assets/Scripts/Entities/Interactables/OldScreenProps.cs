using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OldScreenProps : MonoBehaviour, InteractableObject
{
    public UnityEvent OnOverEvent;
    public UnityEvent OnLeavOverEvent;

    public UnityEvent OnUseEvent;

    [SerializeField] private GameObject RadialMenuPrefab;
    [SerializeField] private MeshRenderer planeMeshRenderer;
    [SerializeField] private Vector3 OffSetInstanciation;
    [SerializeField] private Material[] resultMaterials;

    private RadialMenu radialMenu;

    private void Awake()
    {
        if (radialMenu == null)
        {
            radialMenu = Instantiate(RadialMenuPrefab, transform.position, transform.rotation, transform).GetComponent<RadialMenu>();
            radialMenu.transform.localPosition += OffSetInstanciation;
            radialMenu.OnValidate += ValidateMenu;
            radialMenu.Close();
        }
    }

    public void LeavOverInteractable()
    {
        OnLeavOverEvent?.Invoke();
    }

    public void OverInteractable()
    {
        OnOverEvent?.Invoke();
    }

    public void UseInteractable()
    {
        OnUseEvent?.Invoke();
        InstanciateRadialMenu();
    }

    private void InstanciateRadialMenu()
    {
        if (radialMenu == null)
        {
            radialMenu = Instantiate(RadialMenuPrefab, transform.position, transform.rotation, transform).GetComponent<RadialMenu>();
            radialMenu.transform.localPosition += OffSetInstanciation;
            radialMenu.OnValidate += ValidateMenu;
            radialMenu.Open();
        }
        else
        {
            radialMenu.Toggle();
        }
    }

    private void ValidateMenu(RadialMenuEntry validateEntry)
    {
        if (validateEntry != null)
        {
            Debug.Log("Entry validate !");
            planeMeshRenderer.material = resultMaterials[validateEntry._id];
        }
    }
}