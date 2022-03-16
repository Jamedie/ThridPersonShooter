using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BulletStats", menuName = "BulletStats")]
public class BulletStats : ScriptableObject
{
    public Transform BulletTransform;
    public float BulletSpeed;
    public Transform FxBulletImpact;
    public Transform FxBullet;
}