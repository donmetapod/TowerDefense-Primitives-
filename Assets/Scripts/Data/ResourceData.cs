using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu (fileName = "WeaponsData", menuName = "ScriptableObjects/CreateResourcesData")]
public class ResourceData : ScriptableObject
{
    public int InitialGoldAmount = 100;
    public int IncreaseGoldAmount = 5;
    public int IncreaseGoldDelay = 1;
    [SerializeField] private int _gold = 100;
    public int LastWeaponSelectedCost = 0;
    public UnityEvent<int> OnGoldAmountChange;

    public int Gold
    {
        get => _gold;
        set => _gold = value;
    }
    
    [Serializable]
    public struct WeaponConfig
    {
        public string WeaponName;
        public int WeaponCost;
    }

    public WeaponConfig[] WeaponsCosts;
    
    public void ReturnWeaponCostToResources()
    {
        Gold += LastWeaponSelectedCost;
        OnGoldAmountChange?.Invoke(Gold);
    }
    
    public void SubtractWeaponCost(WeaponTypeSelector.WeaponTypesEnum weaponType)
    {
        switch (weaponType)
        {
            case WeaponTypeSelector.WeaponTypesEnum.Gun:
                LastWeaponSelectedCost = WeaponsCosts[0].WeaponCost; // 0 is Gun
                break;
            case WeaponTypeSelector.WeaponTypesEnum.Cannon:
                LastWeaponSelectedCost = WeaponsCosts[1].WeaponCost; // 1 is Cannon
                break;
            case WeaponTypeSelector.WeaponTypesEnum.LaserTurret:
                LastWeaponSelectedCost = WeaponsCosts[2].WeaponCost; // 2 is LaserTurret
                break;
        }

        Gold -= LastWeaponSelectedCost;
        OnGoldAmountChange?.Invoke(Gold);
    }
}
