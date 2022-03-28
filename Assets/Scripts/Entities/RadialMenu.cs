using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RadialMenu : MonoBehaviour
{
    public enum ValidationMethod
    {
        pointerUp,
        click
    }

    public enum OrganisationType
    {
        Circle,
        SemiCircle,
        Ellipse,
        SemiEllipse
    }

    public bool isOpen = false;

    public Action<RadialMenuEntry> OnValidate;

    [SerializeField] private ValidationMethod currentValidationMethod;

    [SerializeField] private OrganisationType currentOrganisationType;

    [SerializeField] private GameObject entryPrefab;

    [SerializeField] private float radius = 100f;

    [SerializeField] private List<EntryModel> entryModelList;

    [SerializeField] private RadialMenuEntry currentSeletectedEntry;

    [SerializeField] private UnityEvent OnItemSelected;

    private List<RadialMenuEntry> _entriesList;

    private void Awake()
    {
        _entriesList = new List<RadialMenuEntry>();
    }

    private void AddEntry(Sprite icon, string label)
    {
        GameObject entry = Instantiate(entryPrefab, transform);

        RadialMenuEntry newRadialEntry = entry.GetComponent<RadialMenuEntry>();

        newRadialEntry.SetLabel(label);
        newRadialEntry.SetIcon(icon);
        newRadialEntry.CallbackEnter += SetCurrentSelectedEntry;
        newRadialEntry.CallbackClick += ValidateClickEntry;
        newRadialEntry._id = _entriesList.Count;

        _entriesList.Add(newRadialEntry);
        entry.name += "_" + _entriesList.Count.ToString();
    }

    private void SetCurrentSelectedEntry(RadialMenuEntry newCurrentSeletectedEntry)
    {
        if (newCurrentSeletectedEntry == null)
        {
            currentSeletectedEntry = null;
        }
        else
        {
            currentSeletectedEntry = newCurrentSeletectedEntry;
            Debug.Log("Select => " + currentSeletectedEntry.name);
        }
    }

    public void ValidateClickEntry(RadialMenuEntry newCurrentSeletectedEntry)
    {
        if (currentValidationMethod != ValidationMethod.click)
        {
            return;
        }

        Debug.Log("Validate with => " + currentSeletectedEntry.gameObject.name);
        OnValidate?.Invoke(currentSeletectedEntry);
        Close();
    }

    public void ValidatePointerUpEntry()
    {
        if (currentValidationMethod != ValidationMethod.pointerUp)
        {
            return;
        }

        if (currentSeletectedEntry != null)
        {
            Debug.Log("Validate with => " + currentSeletectedEntry.gameObject.name);
            OnValidate?.Invoke(currentSeletectedEntry);
        }
        Close();
    }

    public void ValidateAndClose()
    {
        if (currentSeletectedEntry != null)
        {
            Debug.Log("Validate with => " + currentSeletectedEntry.gameObject.name);
            OnValidate?.Invoke(currentSeletectedEntry);
        }
        Close();
    }

    public void Open()
    {
        Debug.Log("Open");
        for (int i = 0; i < entryModelList.Count; i++)
        {
            AddEntry(entryModelList[i].EntryIcon, entryModelList[i].EntryLabel);
        }
        ReArrange();

        isOpen = true;
    }

    public void Close()
    {
        for (int i = 0; i < _entriesList.Count; i++)
        {
            GameObject entry = _entriesList[i].gameObject;
            Destroy(entry);
        }
        _entriesList.Clear();

        isOpen = false;
    }

    public void Toggle()
    {
        if (_entriesList.Count == 0)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    private void ReArrange()
    {
        float radiensOfSeperation;
        switch (currentOrganisationType)
        {
            case OrganisationType.Circle:
                radiensOfSeperation = (Mathf.PI * 2) / _entriesList.Count;

                for (int i = 0; i < _entriesList.Count; i++)
                {
                    float x = -Mathf.Cos(radiensOfSeperation * i) * radius;
                    float y = Mathf.Sin(radiensOfSeperation * i) * radius;

                    _entriesList[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0);
                }
                break;

            case OrganisationType.SemiCircle:
                radiensOfSeperation = (Mathf.PI) / (_entriesList.Count - 1);

                for (int i = 0; i < _entriesList.Count; i++)
                {
                    float x = -Mathf.Cos(radiensOfSeperation * i) * radius;
                    float y = Mathf.Sin(radiensOfSeperation * i) * radius;

                    _entriesList[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0);
                }
                break;

            case OrganisationType.Ellipse:

                radiensOfSeperation = (Mathf.PI * 2) / (_entriesList.Count);

                for (int i = 0; i < _entriesList.Count; i++)
                {
                    float x = -Mathf.Cos(radiensOfSeperation * i) * radius;
                    float y = Mathf.Sin(radiensOfSeperation * i) * radius;

                    _entriesList[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y / 2, 0);
                }
                break;

            case OrganisationType.SemiEllipse:
                radiensOfSeperation = (Mathf.PI) / (_entriesList.Count - 1);

                for (int i = 0; i < _entriesList.Count; i++)
                {
                    float x = -Mathf.Cos(radiensOfSeperation * i) * radius;
                    float y = Mathf.Sin(radiensOfSeperation * i) * radius;

                    _entriesList[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y / 2, 0);
                }
                break;
        }
    }
}

[Serializable]
public struct EntryModel
{
    public Sprite EntryIcon;
    public string EntryLabel;
}