using UnityEngine;

[CreateAssetMenu (fileName = "WeaponData", menuName = "ScriptableObjects/CreateWeaponData")]
public class WeaponData : ScriptableObject
{
    public GameObject WeaponPrefab;
    public GameObject CannonballPrefab;

    public float MaxRayDistance = 20;
    public int DamagePower = 10;
    public float ShotCooldown = 1;
}
