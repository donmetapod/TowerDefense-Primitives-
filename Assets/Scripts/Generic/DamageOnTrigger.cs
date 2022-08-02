using UnityEngine;
using UnityEngine.Events;

public class DamageOnTrigger : MonoBehaviour
{
    [SerializeField] private int _damagePower = 10;
    [SerializeField] private string _tagToCompare = "Enemy";
    [SerializeField] private UnityEvent OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagToCompare))
        {
            if (other.TryGetComponent(out Health health))
            {
                health.ReceiveDamage(_damagePower);
                OnTrigger?.Invoke();
            }else{
                Debug.LogWarning("Health Component not found");
            }
        }
    }
}
