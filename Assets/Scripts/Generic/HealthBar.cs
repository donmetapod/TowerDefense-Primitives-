using UnityEngine;
using UnityEngine.UI;

// Controls GameObject's health bar
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform _lookAtCamera;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;

    private void Awake()
    {
        if (Camera.main is not null)
        {
            _lookAtCamera = Camera.main.transform;
            _canvas.worldCamera = Camera.main;
        }
        _slider.maxValue = _health.CurrentHealth;
        _slider.value = _health.CurrentHealth;
    }

    public void UpdateSliderValue(int newValue)
    {
        _slider.value = newValue;
    }
    
    private void Update()
    {
        transform.LookAt(_lookAtCamera);
    }
}
