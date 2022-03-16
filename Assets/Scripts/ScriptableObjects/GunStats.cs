using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GunStats", menuName = "GunStats")]
public class GunStats : ScriptableObject
{
    public Transform GunTransform;
    public Transform FxGunShot;
    public BulletStats GunBulletStats;
}