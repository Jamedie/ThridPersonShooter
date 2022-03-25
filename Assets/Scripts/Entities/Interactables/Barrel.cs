using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour, InteractableObject
{
    public void LeavOverInteractable()
    {
        throw new System.NotImplementedException();
    }

    public void OverInteractable()
    {
        Debug.Log("Over");
    }

    public void UseInteractable()
    {
        Debug.Log("Use");
    }
}