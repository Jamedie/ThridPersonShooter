using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleGun : Gun
{
    private float lastShot = 0f;

    public override void EquipGun(Transform gunAttachement)
    {
    }

    public override void Shoot(Vector3 mouseWorldPosition)
    {
        if (Time.time > currentGunStats.RateFire + lastShot)
        {
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            currentGunStats.FxMuzzleShot.Spawn(muzzlePosition.position);
            currentGunStats.FxGunShot.Spawn(spawnBulletPosition.position, spawnBulletPosition.rotation);
            currentGunStats.bulletProjectilePrefab.Spawn(spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            lastShot = Time.time;
        }
    }
}