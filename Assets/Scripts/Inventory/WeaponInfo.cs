using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class WeaponInfo : ScriptableObject
{
    public GameObject weaponPrefab = null;
    public float weaponCooldown = 0f;
    public int weaponDamage = 0;
    public float weaponRange = 0f;
}
