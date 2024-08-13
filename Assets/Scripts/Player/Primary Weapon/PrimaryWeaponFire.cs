using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Primary Weapon/Fire")]
public class PrimaryWeaponFire : PrimaryWeapon
{
    public float damage;
    public GameObject bullet;

    private GameObject _weaponInst;
    public override void Fire(Transform weaponTransform)
    {
        _weaponInst = Instantiate(bullet, weaponTransform.position, weaponTransform.rotation);
    }
}
