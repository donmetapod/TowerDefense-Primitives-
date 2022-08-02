using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Controls the weapon attack depending on weapon type
public class WeaponAttack : MonoBehaviour
{
    [SerializeField] private Transform _weaponBarrel;
    [SerializeField] private WeaponData _weaponData;
    
    enum ShootTypeEnum
    {
        Ray,
        Instantiate
    }
    [SerializeField] private ShootTypeEnum _shootType;
    [SerializeField] private Transform _cannonballSpawnPoint;
    [SerializeField] private UnityEvent OnShoot;
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private GameState _gameState;
    public void StartWeaponAttack()
    {
        StartCoroutine(FireRoutine()); 
    }
    
    // Shoot routine, behaves different depending on weapon type
    IEnumerator FireRoutine()
    {
        while (_gameState.CurrentGameState == GameState.GameStateEnum.Playing)
        {
            Ray ray = new Ray(_weaponBarrel.position, _weaponBarrel.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _weaponData.MaxRayDistance, _enemyLayerMask))
            {
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    if (_shootType == ShootTypeEnum.Instantiate)
                    {
                        // Instantiate cannonball
                        Instantiate(_weaponData.CannonballPrefab, _cannonballSpawnPoint.position, _cannonballSpawnPoint.rotation);
                    }
                    else
                    {
                        Health enemyHealth = hitInfo.collider.GetComponent<Health>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.ReceiveDamage(_weaponData.DamagePower);
                        }    
                    }
                    OnShoot?.Invoke();
                }
                Debug.DrawRay(ray.origin, ray.direction * _weaponData.MaxRayDistance, Color.red);
                yield return new WaitForSeconds(_weaponData.ShotCooldown);
            }
            else
            {
                yield return null;
                Debug.DrawRay(ray.origin, ray.direction * _weaponData.MaxRayDistance, Color.yellow);
            }
        }
    }
}
