using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum FiringMode
    {
        SemiAutomatic,
        Burst,
        FullyAutomatic
    }

    public virtual void EquipGun(Transform gunAttachement)
    {
    }

    public virtual void Shoot(Vector3 aimDir)
    {
    }
}