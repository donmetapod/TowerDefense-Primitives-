using System;
using UnityEngine;

// Utility class used to avoid passing weapon type parameters as strings
public class WeaponTypeSelector : MonoBehaviour
{
    [Serializable]
    public enum WeaponTypesEnum
    {
        Gun,
        Cannon,
        LaserTurret
    }
    public WeaponTypesEnum WeaponType;
}
