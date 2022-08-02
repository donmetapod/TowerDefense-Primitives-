using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Controls creation of weapons, selected weapon handling and positioning
public class PlayerController : MonoBehaviour
{
    [SerializeField] private WeaponData _gunData;
    [SerializeField] private WeaponData _cannonData;
    [SerializeField] private WeaponData _laserTurretData;
    [SerializeField] private GameObject _heldWeapon;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _maxRayDistance = 20;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private UnityEvent<WeaponTypeSelector.WeaponTypesEnum> OnWeaponPurchased;
    [SerializeField] private GameState _gameState;
    [SerializeField] private ResourceData _resourceData;
    private void Start()
    {
        StartCoroutine(HeldWeaponRoutine());
    }
    
    // Creates a weapon from UI button press
    public void CreateWeapon(WeaponTypeSelector weaponTypeSelector)
    {
        if(_heldWeapon != null)
            return;

        switch (weaponTypeSelector.WeaponType)
        {
            case WeaponTypeSelector.WeaponTypesEnum.Gun:
                _heldWeapon = Instantiate(_gunData.WeaponPrefab);
                break;
            case WeaponTypeSelector.WeaponTypesEnum.Cannon:
                _heldWeapon = Instantiate(_cannonData.WeaponPrefab);
                break;
            case WeaponTypeSelector.WeaponTypesEnum.LaserTurret:
                _heldWeapon = Instantiate(_laserTurretData.WeaponPrefab);
                break;
        }
        
        OnWeaponPurchased?.Invoke(weaponTypeSelector.WeaponType);
    }

    // Moves the selected weapon across the scene
    IEnumerator HeldWeaponRoutine()
    {
        while (_gameState.CurrentGameState == GameState.GameStateEnum.Playing)
        {
            if (_heldWeapon != null)
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxRayDistance, _groundLayerMask))
                {
                    _heldWeapon.transform.position = hitInfo.point;
                    // Set weapon on a WeaponSlot hit by ray
                    if (Input.GetMouseButton(0)
                    && hitInfo.collider.CompareTag("WeaponSlot")
                    && hitInfo.transform.childCount == 0)
                    {
                        _heldWeapon.transform.position = hitInfo.transform.position;
                        _heldWeapon.transform.SetParent(hitInfo.transform);
                        _heldWeapon.GetComponent<WeaponAttack>().StartWeaponAttack();
                        _heldWeapon = null;
                    }
                }

                if (Input.GetMouseButton(1))
                {
                    Destroy(_heldWeapon);
                    _heldWeapon = null;
                    _resourceData.ReturnWeaponCostToResources();
                }
            }
            yield return null;
        }
    }
}
