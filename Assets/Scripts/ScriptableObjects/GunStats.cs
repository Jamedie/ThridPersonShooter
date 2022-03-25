using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunStats", menuName = "GunStats")]
public class GunStats : ScriptableObject
{
    public Transform GunTransform;

    public Transform FxMuzzleShot;
    public Transform FxGunShot;
    public BulletStats GunBulletStats;
    public FiringMode GunFiringMode;
    public WeaponType GunWeaponType;
    public Transform bulletProjectilePrefab;

    public float RateFire;
}

public enum FiringMode
{
    SemiAutomatic,
    Burst,
    FullyAutomatic
}

public enum WeaponType
{
    Pistol,
    Rifle,
}