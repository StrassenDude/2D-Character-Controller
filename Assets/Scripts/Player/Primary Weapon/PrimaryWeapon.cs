using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrimaryWeapon : ScriptableObject
{
    public abstract void Fire(Transform weaponTransform);
}
