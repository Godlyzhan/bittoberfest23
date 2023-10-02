using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "CreateWeapon/WeaponStats", order = 1)]
public class WeaponStats : ScriptableObject
{
    public Weapons.WeaponsTypes WeaponType;

    public float FireRate;
    public float BulletSpeed;
    public float ImpactForce;
    public float Damage;
    public float Cooldown;
    public GameObject prefab;
    public AnimatorOverrideController AnimatorOverrideController;
}
