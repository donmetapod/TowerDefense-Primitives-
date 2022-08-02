using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This scripts adds weapons to a list and damages them when ExplodeRoutine ends
public class Bomb : MonoBehaviour
{
    [SerializeField] private List<Transform> _damageList = new List<Transform>();
    [SerializeField] private int _damagePower = 50;
    [SerializeField] private UnityEvent OnDamageDelt;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _delayBeforeExplosion = 1;
    [SerializeField] private float _blinkDuration = 0.1f;
    [SerializeField] private float _blinkDelayDecreaseRate = 0.1f;
    [SerializeField] private string _tagToCompare = "Weapon";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagToCompare))
        {
            _damageList.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tagToCompare))
        {
            _damageList.Remove(other.transform);
        }
    }

    private void Start()
    {
        StartCoroutine(ExplodeRoutine());
    }
    
    IEnumerator ExplodeRoutine()
    {
        // Blink behaviour
        float blinkDelay = 1;
        while (blinkDelay > 0)
        {
            yield return new WaitForSeconds(blinkDelay);
            _renderer.material.SetColor("_EmissionColor", Color.white);
            yield return new WaitForSeconds(_blinkDuration);
            _renderer.material.SetColor("_EmissionColor", Color.red);
            blinkDelay -= _blinkDelayDecreaseRate;
        }
        _renderer.material.SetColor("_EmissionColor", Color.white);
        yield return new WaitForSeconds(_delayBeforeExplosion);
        
        DamageNearWeapons();
    }

    //Make damage to weapons
    void DamageNearWeapons()
    {
        foreach (var weapon in _damageList)
        {
            if(weapon == null)
                continue;
            
            if (weapon.TryGetComponent(out Health health))
            {
                health.ReceiveDamage(_damagePower);
            }
        }
        
        OnDamageDelt?.Invoke();
    }

}
