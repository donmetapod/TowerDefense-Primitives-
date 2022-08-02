using UnityEngine;
using UnityEngine.Events;

public class DeactivateObject : MonoBehaviour
{
    [SerializeField] private UnityEvent OnDeactivate;

    public void Deactivate()
    {
        OnDeactivate?.Invoke();
        gameObject.SetActive(false);
    }
}
