using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunStats currentGunStats;

    [Header("Weapons Settings")]
    [SerializeField] protected Transform muzzlePosition;
    [SerializeField] protected Transform spawnBulletPosition;

    public virtual void EquipGun(Transform gunAttachement)
    { }

    public virtual void Shoot(Vector3 mouseWorldPosition)
    { }
}